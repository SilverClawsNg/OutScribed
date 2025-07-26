using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OutScribed.SharedKernel.Utilities
{
    public static class TagSlugHelper
    {
    
        /// <summary>
        /// Generates a slug from a single tag name.
        /// Trims whitespace, normalizes casing, removes diacritics, and strips invalid symbols.
        /// </summary>
        public static string Slugify(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Tag cannot be null or whitespace.", nameof(input));

            // Step 1: Trim whitespace and normalize casing
            string text = input.Trim().ToLowerInvariant();

            // Step 2: Remove diacritics (e.g. café → cafe)
            text = RemoveDiacritics(text);

            // Step 3: Replace whitespace or underscores with a single dash
            text = Regex.Replace(text, @"[\s_]+", "-");

            // Step 4: Remove all non-alphanumerics except dash
            text = Regex.Replace(text, @"[^a-z0-9\-]", "");

            // Step 5: Collapse multiple dashes
            text = Regex.Replace(text, @"\-{2,}", "-");

            // Step 6: Final trim
            return text.Trim('-');
        }

        public static string Clean(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Tag cannot be null or whitespace.", nameof(input));

            // Step 1: Trim and remove diacritics
            var cleaned = RemoveDiacritics(input.Trim());

            // Step 2: Remove special characters (except letters/numbers/spaces)
            cleaned = Regex.Replace(cleaned, @"[^a-zA-Z0-9\s]", "");

            // Step 3: Split into words and apply PascalCase
            var words = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var result = new StringBuilder();

            foreach (var word in words)
            {
                var lower = word.ToLowerInvariant();

                result.Append(char.ToUpperInvariant(lower[0]) + lower.Substring(1));
            }

            return result.ToString();
        }

        public static string Slugifies(string rawTags)
        {
            if (string.IsNullOrWhiteSpace(rawTags)) return string.Empty;

            var tags = rawTags
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(tag => Slugify(tag))
                .Where(slug => !string.IsNullOrWhiteSpace(slug))
                .Distinct()
                .ToList();

            return string.Join(",", tags);
        }

        public static string Cleans(string rawTags)
        {
            if (string.IsNullOrWhiteSpace(rawTags)) return string.Empty;

            var tags = rawTags
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(tag => Clean(tag))
                .Where(slug => !string.IsNullOrWhiteSpace(slug))
                .Distinct()
                .ToList();

            return string.Join(",", tags);
        }

        private static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            foreach (char c in normalized)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
