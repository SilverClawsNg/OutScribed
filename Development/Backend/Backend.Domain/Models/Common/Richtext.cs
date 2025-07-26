using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;
using Backend.Domain.Misc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Backend.Domain.Models.Common
{

    public class RichText : ValueObject
    {

        public string Value { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private RichText() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private RichText(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Create a new post text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Result<RichText> Create(string value, string type, int maxLength)
        {

            if (string.IsNullOrWhiteSpace(value))
                return new Error(
                       Code: StatusCodes.Status400BadRequest,
                       Title: $"Null {type}",
                       Description: $"No value was found for {type.ToLower()}."
                       );

            if (value.Length > maxLength)
                return new Error(
                         Code: StatusCodes.Status400BadRequest,
                         Title: $"Invalid {type} Length",
                         Description: $"The length for {type.ToLower()}: '{value.GetShortTitle(30)}' is invalid"
                         );

            return new RichText(HttpUtility.HtmlEncode(value.ReplaceEmptyTags()));
        }

        public static Result<RichText> Create()
        {

            return new RichText();
        }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

    }

}
