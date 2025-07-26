using Backend.Domain.Abstracts;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.Common
{
    public class Label : ValueObject
    {

        public string Value { get; init; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Label() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Label(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Create a new label
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static Result<Label> Create(string? value, string type, int maxLength)
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
                       Description: $"The length for {type.ToLower()}: '{value}' is invalid"
                       );

            return new Label(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

    }

}
