using OutScribed.Domain.Abstracts;
using OutScribed.Domain.Exceptions;
using OutScribed.Domain.Models.Common;
using Microsoft.AspNetCore.Http;

namespace OutScribed.Domain.Models.UserManagement.Entities
{

    public class Profile : Entity
    {

        public Guid AccountId { get; private set; }

        public Label PhotoUrl { get; private set; }

        public Label Title { get; private set; }

        public Label Bio { get; private set; }

        public bool IsHidden { get; private set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Profile() : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        public static Result<Profile> Create()
        {

            return new Profile();

        }

        public Result<bool> Update(string title, string bio, string? photoUrl, bool isHidden)
        {

            var titleResult = Label.Create(title, "User Title", 128);

            if (titleResult.IsFailure)
                return titleResult.Error;

            var bioResult = Label.Create(bio, "User Bio", 512);

            if (bioResult.IsFailure)
                return bioResult.Error;

            if (PhotoUrl == null & photoUrl == null)
            {
                return new Error(
                      Code: StatusCodes.Status400BadRequest,
                      Title: $"Null Photo",
                      Description: $"Photo is required to create a profile."
                      );
            }

            if (photoUrl != null)
            {

                var photoUrlResult = Label.Create(photoUrl, "Display Photo", 60);

                if (photoUrlResult.IsFailure)
                    return photoUrlResult.Error;

                PhotoUrl = photoUrlResult.Value;

            }

            Title = titleResult.Value;

            Bio = bioResult.Value;

            IsHidden = isHidden;

            return true;

        }

        public Result<bool> SetVisibility(bool isHidden)
        {

            IsHidden = isHidden;

            return true;

        }

    }

}
