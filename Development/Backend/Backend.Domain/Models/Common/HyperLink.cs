using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.Common
{
    public class Hyperlink : ValueObject
    {

        public string Url { get; init; }

        public string Text { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Hyperlink() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Hyperlink(string url, string text)
        {
            Url = url;
            Text = text;
        }

        /// <summary>
        /// Create a new post hyperlink
        /// </summary>
        /// <param name="url"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Result<Hyperlink> Create(string url, string text)
        {
            if (url == null || text == null)
                return new Error(
                    Code: StatusCodes.Status400BadRequest,
                    Title: $"Null Hyperlink",
                    Description: "Incomplete or no value was found for hyperlink."
                    );

            return new Hyperlink(url, text);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Url;

            yield return Text;
        }

    }

}
