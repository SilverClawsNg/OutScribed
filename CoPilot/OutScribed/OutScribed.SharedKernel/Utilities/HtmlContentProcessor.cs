using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace OutScribed.SharedKernel.Utilities
{

 

    /// <summary>
    /// Provides methods for cleaning HTML content before saving it to a database.
    /// This extended version includes removal of dangerous attributes, scripts, styles,
    /// comments, and empty HTML tags, while also benefiting from HtmlAgilityPack's
    /// inherent HTML normalization.
    /// </summary>
    public static class HtmlContentProcessor
    {
        // Configuration options for this processor
        public static bool RemoveDangerousAttributes { get; set; } = true;
        public static bool RemoveScripts { get; set; } = true;    // Controls removal of <script> tags
        public static bool RemoveStyles { get; set; } = true;     // Controls removal of <style> tags
        public static bool RemoveComments { get; set; } = true;   // Controls removal of HTML comments
        public static bool RemoveEmptyHtmlTags { get; set; } = true; // Controls removal of empty HTML tags

        // Attributes commonly used for XSS attacks or unwanted behavior.
        // This list focuses on event handlers and attributes that can execute code.
        private static readonly HashSet<string> _dangerousAttributes = new(StringComparer.OrdinalIgnoreCase)
    {
        // Global Event Handlers (on*)
        "onabort", "onafterprint", "onbeforeprint", "onbeforeunload", "onblur", "oncanplay", "oncanplaythrough",
        "onchange", "onclick", "oncontextmenu", "oncopy", "oncuechange", "oncut", "ondblclick", "ondrag",
        "ondragend", "ondragenter", "ondragleave", "ondragover", "ondragstart", "ondrop", "ondurationchange",
        "onemptied", "onended", "onerror", "onfocus", "onhashchange", "oninput", "oninvalid", "onkeydown",
        "onkeypress", "onkeyup", "onload", "onloadeddata", "onloadedmetadata", "onloadstart", "onmessage",
        "onmousedown", "onmousemove", "onmouseout", "onmouseover", "onmouseup", "onmousewheel", "onoffline",
        "ononline", "onpagehide", "onpageshow", "onpaste", "onpause", "onplay", "onplaying", "onpopstate",
        "onprogress", "onratechange", "onreset", "onresize", "onscroll", "onseeked", "onseeking", "onselect",
        "onstalled", "onstorage", "onsubmit", "onsuspend", "ontimeupdate", "ontoggle", "onunload", "onvolumechange",
        "onwaiting", "onwheel",

        // Other potentially dangerous attributes
        "formaction", "formmethod", "formtarget", "formenctype", "formnovalidate", "contenteditable",
        "data", // For <object> tag
        "action", // For <form> tag, though usually safe, can be used maliciously with other techniques
        "srcset", "srcdoc", // Can be used for script injection in some contexts
        "ping", // Hyperlink auditing
        // The 'style' attribute is handled separately below due to its complexity.
        // Attributes like 'class' and 'id' are generally not dangerous themselves, but their interaction
        // with custom CSS or JavaScript can be. This class does not remove them by default.
    };

        // Regex for basic URL scheme validation in href/src.
        // Only schemes starting with http, https, mailto, or tel are considered safe.
        private static readonly Regex _safeUrlRegex = new Regex(@"^(https?|mailto|tel):", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        // Internal set of tags that should be preserved even if their InnerHtml is empty.
        // These are typically self-closing tags or tags that convey meaning through attributes.
        private static readonly HashSet<string> _tagsToPreserveEvenIfEmpty = new(StringComparer.OrdinalIgnoreCase)
    {
        "area", "base", "br", "col", "embed", "hr", "img", "input", "link", "meta", "param", "source", "track", "wbr",
    };

        /// <summary>
        /// Cleans HTML content before saving to a database, performing the following operations based on configuration:
        /// <list type="bullet">
        ///     <item><term>Remove Dangerous Attributes:</term><description>Strips attributes known for XSS vulnerabilities (e.g., 'onload') and sanitizes URL attributes ('href', 'src') for safe schemes. The 'style' attribute is also removed by default.</description></item>
        ///     <item><term>Remove Scripts and Styles:</term><description>Removes &lt;script&gt; and &lt;style&gt; tags and their content.</description></item>
        ///     <item><term>Remove Comments:</term><description>Strips HTML comments.</description></item>
        ///     <item><term>Remove Empty HTML Tags:</term><description>Removes tags that are structurally empty (contain no content or only whitespace), handling nested emptiness.</description></item>
        ///     <item><term>Normalize HTML:</term><description>HtmlAgilityPack inherently corrects malformed HTML and builds a consistent DOM. The output HTML reflects this corrected structure.</description></item>
        /// </list>
        /// This class **does not** implement a configurable whitelist for *all* HTML tags or attributes; it primarily focuses on removing *known unsafe* elements and attributes. For a more secure, whitelist-based sanitization, consider using the `HtmlSanitizer` class provided previously.</summary>
        /// <param name="htmlContent">The rich text HTML content to clean.</param>
        /// <returns>The cleaned HTML string. Returns String.Empty if input is null or whitespace.</returns>
        public static string Clean(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                return string.Empty;
            }

            HtmlDocument doc = new();
            // HtmlAgilityPack loads and parses the HTML, inherently correcting common malformed structures.
            // This process contributes to HTML normalization by building a consistent DOM.
            doc.LoadHtml(htmlContent);

            // Process nodes from deepest to shallowest. Iterating in reverse allows safe removal
            // of child nodes without invalidating the enumeration of their parents.
            foreach (var node in doc.DocumentNode.Descendants().Reverse().ToList())
            {
                if (node.NodeType == HtmlNodeType.Element)
                {
                    string tagName = node.Name.ToLowerInvariant(); // Normalize tag name for case-insensitive comparison

                    // --- 1. Remove Scripts and Styles ---
                    if (RemoveScripts && tagName == "script")
                    {
                        node.Remove();
                        continue; // Skip further processing for this node as it's removed
                    }
                    if (RemoveStyles && tagName == "style")
                    {
                        node.Remove();
                        continue; // Skip further processing for this node as it's removed
                    }

                    // --- 2. Sanitize Attributes (Remove Dangerous Attributes) ---
                    foreach (var attribute in node.Attributes.Reverse().ToList()) // Iterate attributes in reverse for safe removal
                    {
                        string attrName = attribute.Name.ToLowerInvariant(); // Normalize attribute name for comparison

                        // Remove attributes known to be dangerous (e.g., event handlers like 'onload')
                        if (RemoveDangerousAttributes && _dangerousAttributes.Contains(attrName))
                        {
                            attribute.Remove();
                            continue;
                        }

                        // Remove unsafe URL schemes (e.g., "javascript:") from href/src attributes
                        if ((attrName == "href" || attrName == "src") && !_safeUrlRegex.IsMatch(attribute.Value))
                        {
                            attribute.Remove();
                            continue;
                        }

                        // Remove the 'style' attribute entirely for security.
                        // Proper inline CSS sanitization is complex and typically requires a dedicated CSS parser,
                        // which is beyond the scope of this general cleanup class.
                        if (attrName == "style")
                        {
                            attribute.Remove();
                            continue;
                        }

                        // Add more attribute-specific validations here if necessary.
                    }
                }
                // --- 3. Remove Comments ---
                else if (node.NodeType == HtmlNodeType.Comment && RemoveComments)
                {
                    node.Remove();
                }
                // Other node types (like HtmlNodeType.Text, HtmlNodeType.DocumentType) are implicitly kept.
            }

            // --- 4. Remove Empty HTML Tags ---
            // This is performed in a separate, recursive pass. Removals in the first pass
            // might cause parent tags to become empty, requiring subsequent checks.
            if (RemoveEmptyHtmlTags)
            {
                RemoveRecursivelyEmptyHtmlTags(doc);
            }

            // --- 5. Normalize HTML (Output) ---
            // HtmlAgilityPack returns HTML based on its cleaned and corrected DOM structure.
            // This is the "normalized" output. While HtmlAgilityPack may preserve original tag/attribute
            // casing from the input HTML in its serialization, the internal processing within this class
            // (e.g., comparisons) is consistently case-insensitive.
            return doc.DocumentNode.InnerHtml;
        }

        /// <summary>
        /// Private helper method to recursively remove HTML tags that are truly empty
        /// (contain no content or only whitespace). This method continues in passes
        /// until no more empty tags are found, ensuring nested empty tags are handled.
        /// </summary>
        /// <param name="doc">The HtmlDocument to process.</param>
        private static void RemoveRecursivelyEmptyHtmlTags(HtmlDocument doc)
        {
            bool tagsRemovedInPass;
            do
            {
                tagsRemovedInPass = false;
                foreach (var node in doc.DocumentNode.Descendants().ToList()) // Use ToList() for safe iteration
                {
                    if (node.NodeType == HtmlNodeType.Element)
                    {
                        string tagName = node.Name.ToLowerInvariant();

                        // If the tag is explicitly listed to be preserved even if empty (e.g., <img>, <br>), skip it.
                        if (_tagsToPreserveEvenIfEmpty.Contains(tagName))
                        {
                            continue;
                        }

                        // A tag is considered empty if its inner content (text + child HTML) is null, empty, or whitespace.
                        if (string.IsNullOrWhiteSpace(node.InnerHtml))
                        {
                            node.Remove(); // Remove the empty tag
                            tagsRemovedInPass = true; // Mark that a removal occurred
                        }
                    }
                }
            } while (tagsRemovedInPass); // Continue passes as long as removals are happening
        }
    }
}
