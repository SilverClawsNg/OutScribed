using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.SharedKernel.Enums;

namespace OutScribed.Modules.Identity.Domain.Models
{
    public class Admin : Entity
    {
        public Ulid AccountId { get; private set; }
        public RoleType Type { get; private set; }
        public DateTime AssignedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        public bool IsActive { get; private set; }
        private Admin() { }

        private Admin(RoleType type)
        {
            AssignedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
            Type = type;
            IsActive = true;
        }

        public static Admin Create(RoleType type)
        {

            var invalidFields = Validator.GetInvalidFields(
               [
                     new("Admin Type", type)
               ]
             );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            return new Admin(type);
        }
    }
}
