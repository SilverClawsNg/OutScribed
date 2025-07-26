using OutScribed.SharedKernel.Abstract;
using OutScribed.SharedKernel.DomainFieldValidation;
using OutScribed.Modules.Identity.Domain.Exceptions;

namespace OutScribed.Modules.Identity.Domain.Models
{

    public class Profile : Entity
    {

        public Ulid AccountId { get; private set; }

        public Account Account { get; private set; } = default!;

        public string? Photo { get; private set; }

        public string Title { get; private set; } = string.Empty!;

        public string? Bio { get; private set; }

        public bool IsAnnonymous { get; private set; }

        public DateTime CreatedAt { get; private set; }


        private Profile() { }

       
        public static Profile Create()
        {
       
            return new Profile();

        }

        public void Update(string title, string? bio, string? photo, bool isHidden)
        {

            //Ensure a photo exists to create profile
            if(string.IsNullOrWhiteSpace(photo) && Photo == null)
            {
                throw new PhotoNotFoundException();
            }

            var invalidFields = Validator.GetInvalidFields(
            [
                  new("Title", title, maxLength: 128),
                  new("Bio", bio, maxLength: 512, isOptional:true),
                  new("Photo URL", photo, maxLength: 60, isOptional:true),
                  new("IsAnnonymous", isHidden)
            ]
          );

            if (invalidFields.Count != 0)
            {
                throw new InvalidParametersException(invalidFields);
            }

            Title = title;

            if (bio != null)
                Bio = bio;

            IsAnnonymous = isHidden;

            if(photo != null)
                Photo = photo;

        }

    }

}
