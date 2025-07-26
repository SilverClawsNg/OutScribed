using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace OutScribed.Client.Services
{
    public class MyAuthenticationStateProvider(HttpClient httpClient,
         ILocalStorageService localStorage) : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly ILocalStorageService _localStorage = localStorage;
        private readonly AuthenticationState _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("jwtToken");

            var tokenHandler = new JwtSecurityTokenHandler();

            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            if (tokenHandler.CanReadToken(token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

                return new AuthenticationState(
                             new ClaimsPrincipal(
                                 new ClaimsIdentity(jwtSecurityToken.Claims, "UserType")));
            }

            return _anonymous;

        }

    }
}
