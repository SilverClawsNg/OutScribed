using OutScribed.Application.Features.UserManagement.Common;
using System.Text.Json.Serialization;

namespace OutScribed.Application.Features.UserManagement.Queries.LoadUsernameProfile
{

    public class LoadUsernameProfileQueryResponse
    {

        public Guid Id { get; set; }

        public string Username { get; set; } = default!;

        [JsonIgnore]
        public string RawTitle { get; set; } = default!;

        [JsonIgnore]
        public string? RawBio { get; set; }

        [JsonIgnore]
        public List<Contacts> RawContacts { get; set; } = default!;

        public string? RawDisplayPhoto { get; set; }

        public string? Title => IsHidden ? null : RawTitle;

        public string? Bio => IsHidden ? null : RawBio;

        public List<Contacts>? Contacts => IsHidden ? null : RawContacts;

        public string? DisplayPhoto => IsHidden ? null : RawDisplayPhoto;

        public int Tales { get; set; }

        public int Threads { get; set; }

        public int Followers { get; set; }

        public int ProfileViews { get; set; }

        public bool IsFollowingUser { get; set; }

        public bool IsHidden { get; set; }

        public DateTime RegisterDate { get; set; }

    }

}