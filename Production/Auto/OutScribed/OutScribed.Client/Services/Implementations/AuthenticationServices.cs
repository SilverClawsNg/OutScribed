using Blazored.LocalStorage;
using OutScribed.Client.Enums;
using OutScribed.Client.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using OutScribed.Client.Responses;
using OutScribed.Client.Requests;
using System.Data;

namespace OutScribed.Client.Services.Implementations
{
    public class AuthenticationServices(ILocalStorageService localStorage, HttpClient httpClient)
     : IAuthenticationServices
    {

        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly HttpClient _httpClient = httpClient;

        public async Task<bool> CheckJwtTokenAsync()
        {

            var token = await _localStorage.GetItemAsync<string>("jwtToken");

            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            if (jwtSecurityToken == null)
                return false;

            var jwtExpiration = jwtSecurityToken.Claims.Where(c => c.Type == "exp").Select(c => c.Value).FirstOrDefault();

            if (jwtExpiration == null)
                return false;

            var ticks = long.Parse(jwtExpiration);

            var jwtDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;

            if (jwtDate <= DateTime.UtcNow) // access token has expired
            {

                // create request object
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "accounts/refresh/token");

                // get refresh token from storage
                var refreshTokenFromStorage = await _localStorage.GetItemAsync<string>("refreshToken");

                // check if refresh token exists
                if (refreshTokenFromStorage == null)
                {
                    return false;
                }

                // initialize refresh token form data
                var postRefreshTokenRequest = new RefreshTokenRequest { Token = refreshTokenFromStorage };

                // add the request model to http request
                httpRequest.Content = new StringContent(JsonSerializer.Serialize(postRefreshTokenRequest), Encoding.UTF8, "application/json");

                // send request
                using var httpResponse = await _httpClient.SendAsync(httpRequest);

                if (!httpResponse.IsSuccessStatusCode) // problem encountered refreshing token
                {
                    return false;
                }

                // retrieve the response model from http response 
                var refreshToken = await httpResponse.Content.ReadFromJsonAsync<RefreshTokenResponse>();

                if (refreshToken == null || refreshToken.IsSuccessful == false)
                {
                    return false;
                }
                else
                {

                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", refreshToken.Token);

                    //add token to local storage for future use
                    await _localStorage.SetItemAsync("jwtToken", refreshToken.Token);

                    //add token to local storage for future use
                    await _localStorage.SetItemAsync("refreshToken", refreshToken.RefreshToken);

                    return true;
                }

            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return true;

        }

        public async Task<RoleTypes> GetAdminRoleAsync()
        {

            var token = await _localStorage.GetItemAsync<string>("jwtToken");

            if (token == null)
                return RoleTypes.None;

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            if (jwtSecurityToken == null)
                return RoleTypes.None;

            var jwtExpiration = jwtSecurityToken.Claims.Where(c => c.Type == "exp").Select(c => c.Value).FirstOrDefault();

            if (jwtExpiration == null)
                return RoleTypes.None;

            var ticks = long.Parse(jwtExpiration);

            var jwtDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;

            if (jwtDate <= DateTime.UtcNow) // access token has expired
                return RoleTypes.None;


            var role = jwtSecurityToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault();

            if (role == null)
                return RoleTypes.None;

            var result = Enum.TryParse(role, out RoleTypes roleType);

            if (result == false)
                return RoleTypes.None;

            return roleType;

        }

        public async Task<bool> CheckLoggedInAsync()
        {

            var token = await _localStorage.GetItemAsync<string>("jwtToken");

            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

            if (jwtSecurityToken == null)
                return false;

            var jwtExpiration = jwtSecurityToken.Claims.Where(c => c.Type == "exp").Select(c => c.Value).FirstOrDefault();

            if (jwtExpiration == null)
                return false;

            var ticks = long.Parse(jwtExpiration);

            var jwtDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;

            if (jwtDate <= DateTime.UtcNow) // access token has expired
                return false;

            return true;
        }

    }

}
