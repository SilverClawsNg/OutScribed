using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OutScribed.SharedKernel.Utilities
{
  
    public static class UrlSlugGenerator
    {
        /// <summary>
        /// Generates a custom URL slug by concatenating the blog's publication date (YYYY-MM-DD),
        /// the unique creator's username, and a slugified version of the title.
        /// Uses hyphens (-) as separators, avoiding forward slashes (/).
        /// </summary>
        /// <param name="publicationDate">The date the blog post was published.</param>
        /// <param name="username">The unique username of the blog post creator.</param>
        /// <param name="title">The title of the blog.</param>
        /// <returns>A string representing the custom URL slug (e.g., "2023-10-26-john-doe-my-awesome-blog-post").</returns>
        /// <exception cref="ArgumentException">Thrown if username or title is null or whitespace.</exception>
        public static string Generate(DateTime publicationDate, string username, string title)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or whitespace.", nameof(username));
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Blog title cannot be null or whitespace.", nameof(title));
            }

            // 1. Format the publication date as YYYY-MM-DD
            string dateSegment = publicationDate.ToString("yyyy-MM-dd");

            // 2. Slugify the Username (even if unique, ensure it's URL-safe)
            string usernameSlug = GenerateSlug(username);

            // 3. Slugify the Blog Title
            string titleSlug = GenerateSlug(title);

            // 4. Concatenate date, username, and title slugs with hyphens
            return $"{dateSegment}-{usernameSlug}-{titleSlug}";
        }

        /// <summary>
        /// Converts a string into a URL-friendly slug.
        /// (This private helper method remains the same as before.)
        /// </summary>
        /// <param name="phrase">The input string (e.g., a title or username).</param>
        /// <returns>A lowercase, hyphen-separated string with special characters removed.</returns>
        private static string GenerateSlug(string phrase)
        {

            string str = phrase.Normalize(NormalizationForm.FormD);

            StringBuilder slugBuilder = new();

            foreach (char c in str)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);

                if (Char.IsLetterOrDigit(c))
                {
                    slugBuilder.Append(Char.ToLowerInvariant(c));
                }
                else if (category == UnicodeCategory.SpaceSeparator ||
                         category == UnicodeCategory.DashPunctuation ||
                         category == UnicodeCategory.ConnectorPunctuation)
                {
                    if (slugBuilder.Length > 0 && slugBuilder[^1] != '-')
                    {
                        slugBuilder.Append('-');
                    }
                }
            }

            string slug = slugBuilder.ToString().Trim('-');

            slug = Regex.Replace(slug, @"--+", "-");

            return slug;
        }
    }
}
