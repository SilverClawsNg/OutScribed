using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class Contact : Entity
    {
        public Ulid AccountId { get; private set; }

        public Account Account { get; private set; } = default!;

        public ContactType Type { get; private set; }

        public string Text { get; private set; } = string.Empty;

        public DateTime CreatedAt { get; private set; }

        private Contact() { }

        private Contact(string text, ContactType type)
        {
            CreatedAt = DateTime.UtcNow;
            Type = type;
            Text = text;
        }

        public static Contact Create(string text, ContactType type)
        {

            var invalidFields = Validator.GetInvalidFields(
               [
                     new("Contact Text", text, maxLength: 56),
                     new("Contact Type", type)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Contact(text, type);
        }
    }
}
