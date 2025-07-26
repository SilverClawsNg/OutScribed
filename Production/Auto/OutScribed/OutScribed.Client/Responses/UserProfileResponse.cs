using OutScribed.Client.Enums;
using OutScribed.Client.Extensions;
using OutScribed.Client.Globals;
using OutScribed.Client.Models;

namespace OutScribed.Client.Responses
{
    public class UserProfileResponse
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public string Username { get; set; } = default!;

        public string Title { get; set; } = default!;

        public string? Bio { get; set; }

        public string? DisplayPhoto { get; set; }

        public bool IsHidden { get; set; }

        public int Tales { get; set; }

        public string TalesToString => Tales.ToString().GetCounts();

        public int Threads { get; set; }

        public string ThreadsToString => Threads.ToString().GetCounts();

        public int Followers { get; set; }

        public string FollowersToString => Followers.ToString().GetCounts();

        public int ProfileViews { get; set; }

        public string ProfileViewsToString => ProfileViews.ToString().GetCounts();

        public bool IsFollowingUser { get; set; }

        public string? DisplayPhotoToString => DisplayPhoto == null ? "/images/users/placeholder.jpeg" : string.Format("{0}{1}{2}", Url, "outscribed/users/", DisplayPhoto);

        public List<Contacts> Contacts { get; set; } = default!;

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
