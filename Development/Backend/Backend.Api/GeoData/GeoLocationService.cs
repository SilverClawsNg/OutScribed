using MaxMind.GeoIP2;
using System.Net;

namespace Backend.Api.GeoData
{
    public class GeoLocationService
    {
        private readonly DatabaseReader _reader;

        public GeoLocationService(IWebHostEnvironment env)
        {
            var dbPath = Path.Combine(env.ContentRootPath, "GeoData", "GeoLite2-City.mmdb");
            _reader = new DatabaseReader(dbPath);
        }

        public (string Continent, string Country, string Region, string City) GetLocation(string? ipAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out var ip)) return ("Unknown", "Unknown", "Unknown", "Unknown");

            try
            {
                var city = _reader.City(ip);

                return (city?.Continent?.Name ?? "Unknown", city?.Country?.Name ?? "Unknown", city?.MostSpecificSubdivision?.Name ?? "Unknown", city?.City?.Name ?? "Unknown");
            }
            catch
            {
                return ("Unknown", "Unknown", "Unknown", "Unknown");
            }
        }
    }
}
