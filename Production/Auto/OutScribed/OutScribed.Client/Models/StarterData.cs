using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Globals;

namespace OutScribed.Client.Models
{
    public class StarterData
    {

        private static string Url => Constants.Url;

        public StarterData()
        {
            Title = "Title";
            Username = "Username";
            Bio = "Bio";
            DisplayPhoto = "placeholder.jpeg";
            Role = RoleTypes.None;
        }

        public StarterData(DateTime registerDate,
            string username,
            string title,
            string? bio,
            string? emailAddress,
            string? phoneNumber,
             int followers,
            int profileViews,
            bool isHidden,
            string? displayPhoto,
            RoleTypes role,
            List<Contacts>? contacts)
        {
            Title = title;
            Username = username;
            Bio = bio;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            IsHidden = isHidden;
            Followers = followers;
            ProfileViews = profileViews;
            DisplayPhoto = displayPhoto;
            Role = role;
            Contacts = contacts;
            RegisterDate = registerDate;
        }

        public void UpdateProfile(string title, string? bio, string? emailAddress,
            string? phoneNumber, bool isHidden, string? displayPhoto)
        {

            Title = title;
            Bio = bio;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            IsHidden = isHidden;

            if (displayPhoto != null)
                DisplayPhoto = displayPhoto;

        }


        public void UpdateContact(ContactTypes type, string text)
        {
            Contacts ??= [];

            var contact = Contacts.Where(c => c.Type == type).SingleOrDefault();

            if (contact != null)
            {

                contact.Text = text;
            }
            else
            {

                var newContact = new Contacts() { Type = type, Text = text };

                Contacts.Add(newContact);

            }
        }

        public string Username { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? Bio { get; set; }

        public string? DisplayPhoto { get; set; }

        public string? DisplayPhotoToString => DisplayPhoto == null ? "/images/users/placeholder.jpeg" : string.Format("{0}{1}{2}", Url, "outscribed/users/", DisplayPhoto);

        public RoleTypes Role { get; set; }

        public string? PhoneNumber { get; set; }

        public string? EmailAddress { get; set; }

        public string RoleToString => Role.ToString().Replace("_", " ");

        public bool IsHidden { get; set; }

        public int Followers { get; set; }

        public string FollowersToString => Followers.ToString().GetCounts();

        public int ProfileViews { get; set; }

        public string ProfileViewsToString => ProfileViews.ToString().GetCounts();

        public List<Contacts>? Contacts { get; set; }

        public string? Facebook => Contacts?
            .Where(c => c.Type == ContactTypes.Facebook)
            .Select(c => c.Text)
            .SingleOrDefault();

        public string? Twitter => Contacts?
          .Where(c => c.Type == ContactTypes.Twitter)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? Instagram => Contacts?
          .Where(c => c.Type == ContactTypes.Instagram)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? Telephone => Contacts?
          .Where(c => c.Type == ContactTypes.Telephone)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? Email => Contacts?
          .Where(c => c.Type == ContactTypes.Email)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? TikTok => Contacts?
          .Where(c => c.Type == ContactTypes.TikTok)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? LinkedIn => Contacts?
          .Where(c => c.Type == ContactTypes.LinkedIn)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? WhatsApp => Contacts?
          .Where(c => c.Type == ContactTypes.WhatsApp)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? Telegram => Contacts?
          .Where(c => c.Type == ContactTypes.Telegram)
          .Select(c => c.Text)
          .SingleOrDefault();

        public string? Website => Contacts?
          .Where(c => c.Type == ContactTypes.Website)
          .Select(c => c.Text)
          .SingleOrDefault();

        public DateTime RegisterDate { get; set; }

        public string RegisterDateToString => RegisterDate.ToString("MMMM dd yyyy");
    }
}
