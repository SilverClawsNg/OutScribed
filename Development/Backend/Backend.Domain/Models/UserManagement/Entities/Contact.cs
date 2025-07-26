using Backend.Domain.Abstracts;
using Backend.Domain.Enums;
using Backend.Domain.Exceptions;
using Backend.Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace Backend.Domain.Models.UserManagement.Entities
{
    public class Contact : Entity
    {
        public Guid AccountId { get; private set; }

        public ContactTypes Type { get; private set; }

        public Label Text { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Contact() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private Contact(ContactTypes type, Label text)
            : base(Guid.NewGuid())
        {
            Type = type;
            Text = text;
        }

        /// <summary>
        /// Creates new contact
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Result<Contact> Create(ContactTypes? type, string text)
        {

            var valueResult = Label.Create(text, "Contact Text", 56);

            if (valueResult.IsFailure)
                return valueResult.Error;

            if (type == null)
                return new Error(Code: StatusCodes.Status400BadRequest,
                                     Title: "Null Contact Type",
                                      Description: "No contact type was found.");

            if (!Enum.IsDefined(typeof(ContactTypes), type))
                return new Error(Code: StatusCodes.Status400BadRequest,
                                   Title: "Invalid Contact Type",
                                    Description: $"The contact type with title: {type} is invalid.");

            return new Contact((ContactTypes)type, valueResult.Value);

        }

        /// <summary>
        /// Delegates update of an existing contact value
        /// </summary>
        /// <param name="value"></param>
        public Result<bool> Update(string value)
        {
            var valueResult = Label.Create(value, "Contact", 56);

            if (valueResult.IsFailure)
                return valueResult.Error;

            Text = valueResult.Value;

            return true;
        }
    }

}
