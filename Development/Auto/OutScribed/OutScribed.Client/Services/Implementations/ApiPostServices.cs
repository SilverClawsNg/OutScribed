using Blazored.LocalStorage;
using OutScribed.Client.Models;
using OutScribed.Client.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using OutScribed.Client.Requests;

namespace OutScribed.Client.Services.Implementations
{
    public class ApiPostServices<T, R>(HttpClient httpClient, 
        IAuthenticationServices authenticate)
     : IApiPostServices<T, R>
    {

        private readonly HttpClient _httpClient = httpClient;
        private readonly IAuthenticationServices _authenticate = authenticate;

        private static readonly JsonSerializerOptions _readOptions = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task<Result<T>> PostAsync(string url, R FormData, CancellationToken cancellationToken)
        {

            //if (typeof(R) != typeof(SendOTPRequest)
            //     && typeof(R) != typeof(CreateAccountRequest)
            //        && !await _authenticate.CheckJwtTokenAsync())
            //{
            //    return "X";
            //}

            try
            {
                // create request object
                var request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    // set request body
                    Content = new StringContent(JsonSerializer.Serialize(FormData), Encoding.UTF8, "application/json")
                };

                // send request
                using var response = await _httpClient.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    ClientProblemDetails? clientProblemDetails = await response.Content.ReadFromJsonAsync<ClientProblemDetails>(cancellationToken);

                    if (clientProblemDetails != null)
                    {
                        return (int)response.StatusCode + ": [ " + clientProblemDetails.Title + " ] " + clientProblemDetails.Detail;
                    }
                    else
                    {
                        return (int)response.StatusCode + ": [ " + response.ReasonPhrase + " ]";
                    }
                }
                else
                {

                    if (typeof(T) == typeof(bool))
                    {
                        return 0;
                    }
                    else
                    {
                        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

                        if (string.IsNullOrEmpty(responseContent))
                        {
                            return "Null data received";
                        }

                        var result = JsonSerializer.Deserialize<T>(responseContent, _readOptions);

                        if (result == null)
                        {
                            return "Null data received";
                        }
                        else
                        {
                            return result;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
    }

}
