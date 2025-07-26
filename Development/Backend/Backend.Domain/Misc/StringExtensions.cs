using System.Text;

namespace Backend.Domain.Misc
{
    public static class StringExtensions
    {
        public static string GetShortTitle(this string? html, int num)
        {
            if (string.IsNullOrEmpty(html))
            {
                return html!;
            }

            char[] charArray = new char[html.Length];

            int index = 0;

            bool isInside = false;

            for (int i = 0; i < html.Length; i++)
            {
                char left = html[i];

                if (left == '<')
                {
                    isInside = true;
                    continue;
                }

                if (left == '>')
                {
                    isInside = false;
                    continue;
                }

                if (!isInside)
                {
                    charArray[index] = left;
                    index++;
                }

            }

            var scrapedHtml = new string(charArray, 0, index);

            if (scrapedHtml.Length > num)
            {
                return string.Concat(scrapedHtml, "...");

            }
            else
            {
                return string.Concat(scrapedHtml.AsSpan(0, num), "...");

            }


        }
        public static string RemoveSpecialCharacters(this string str)
        {
            StringBuilder sb = new();

            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public static string ReplaceEmptyTags(this string str)
        {

            List<string> emptytags = ["<p></p>", "<p><br /></p>", "<p><br/></p>",
                 "<p><br></p>"];

            foreach (var emptytag in emptytags)
            {
                str = str.Replace(emptytag, null);
            }
            return str;
        }
    }
}
