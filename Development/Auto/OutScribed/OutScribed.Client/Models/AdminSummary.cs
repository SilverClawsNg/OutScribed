using OutScribed.Client.Enums;
using OutScribed.Client.Globals;

namespace OutScribed.Client.Models
{
    public class AdminSummary
    {

        private static string Url => Constants.Url;

        public Guid Id { get; set; }

        public RoleTypes Role { get; set; }

        public string RoleToString => Role.ToString().Replace("_", " ");

        public Countries Country { get; set; }

        public string CountryToString => Country.ToString().Replace("_", " ");

        public string Address { get; set; } = default!;

        public string Username { get; set; } = default!;

        public string ApplicationUrl { get; set; } = default!;

        public string? ApplicationUrlToString => ApplicationUrl == null ? null : string.Format("{0}{1}{2}", Url, "documents/applications/", ApplicationUrl);

        public DateTime ApplicationDate { get; set; }

        public string ApplicationDateToString => ApplicationDate.ToString("dddd, dd MMMM yyyy");

    }
}
