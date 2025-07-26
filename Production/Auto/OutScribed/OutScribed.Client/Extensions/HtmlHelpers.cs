using System.Text.RegularExpressions;

namespace OutScribed.Client.Extensions
{
    public static class Helpers
    {
        public static string? Truncate(this string? input, int length)
        {

            if (input == null)
                return null;

            if (input.Length <= length)
            {
                return string.Concat(input, "...");
            }
            else
            {
                return string.Concat(input.AsSpan(0, length), "...");
            }
        }

        public static string GetTimeFromDate(DateTime Date)
        {

            if (Date == DateTime.MinValue)
            {
                return "0 seconds ago";
            }

            var timeMinus = DateTime.Now - Date.ToLocalTime();

            string timeSpan;

            if (timeMinus.TotalSeconds < 60)
            {
                var temp = ((int)(timeMinus.TotalSeconds));

                timeSpan = temp.ToString() + (temp < 2 ? " second ago" : " seconds ago");

            }
            else if (timeMinus.TotalMinutes < 60)
            {
                var temp = ((int)(timeMinus.TotalMinutes));

                timeSpan = temp.ToString() + (temp < 2 ? " minute ago" : " minutes ago");
            }
            else if (timeMinus.TotalHours < 24)
            {
                var temp = ((int)(timeMinus.TotalHours));

                timeSpan = temp.ToString() + (temp < 2 ? " hour ago" : " hours ago");
            }
            else if (timeMinus.TotalDays < 7)
            {
                var temp = ((int)(timeMinus.TotalDays));

                timeSpan = temp.ToString() + (temp < 2 ? " day ago" : " days ago");
            }
            else if (((timeMinus.TotalDays) / 7) < 4)
            {
                var temp = ((int)((timeMinus.TotalDays / 7)));

                timeSpan = (temp == 0 ? 1 : temp).ToString() + (temp < 2 ? " week ago" : " weeks ago");
            }
            else if (timeMinus.TotalDays < 365)
            {
                var temp = (int)((timeMinus.TotalDays / 30));

                timeSpan = (temp == 0 ? 1 : temp).ToString() + (temp < 2 ? " month ago" : " months ago");
            }
            else
            {
                var temp = ((int)((timeMinus.TotalDays / 365)));

                timeSpan = (temp == 0 ? 1 : temp).ToString() + (temp < 2 ? " year ago" : " years ago");
            }

            return timeSpan;
        }

        public static string TruncateEnd(this string input)
        {

            var index = input.LastIndexOf('_');

            if (index >= 0)
            {
                //var result = input[(index + 1)..].Replace("_", " na ");

                return input[..index].Replace("_", " na ");

            }

            return input;
        }

        public static string ConvertEnum(this string input)
        {

            var index = input.LastIndexOf('_');

            if (index >= 0)
            {

                return input[..index].Replace("8", " ");

            }

            return input;
        }

        public static string RemoveTags(this string input)
        {

            //return Regex.Replace(input, @"<[^>].+?>", string.Empty);

            return Regex.Replace(input, @"<[^>]*>", string.Empty);

        }

        public static string GetCounts(this string input)
        {

            string result = "0";

            var value = input.ToCharArray();

            if (value.Length == 1)
            {
                result = value[0].ToString();
            }
            else if (value.Length == 2)
            {
                result = value[0].ToString() + value[1].ToString();
            }
            else if (value.Length == 3)
            {
                result = value[0].ToString() + value[1].ToString() + value[2].ToString();
            }
            else if(value.Length == 4)
            {
                result = value[0].ToString() + '.' + value[1].ToString() + 'k';
            }
            else if (value.Length == 5)
            {
                result = value[0].ToString() + value[1].ToString() + '.' + value[2].ToString() + 'k';
            }
            else if (value.Length == 6)
            {
                result = value[0].ToString() + value[1].ToString() + value[2].ToString() + 'k';
            }
            else if (value.Length == 7)
            {
                result = value[0].ToString() + '.' + value[1].ToString() + 'm';
            }
            else if (value.Length == 8)
            {
                result = value[0].ToString() + value[1].ToString() + '.' + value[2].ToString() + 'm';
            }
            else if (value.Length == 9)
            {
                result = value[0].ToString() + value[1].ToString() + value[2].ToString() + 'm';
            }
            else if (value.Length > 9)
            {
                result = ">1b";
            }

            return result;
        }
    }

   
}
