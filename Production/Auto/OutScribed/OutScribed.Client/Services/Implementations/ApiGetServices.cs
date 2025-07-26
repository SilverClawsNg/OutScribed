using OutScribed.Client.Models;
using OutScribed.Client.Requests;
using OutScribed.Client.Responses;
using OutScribed.Client.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace OutScribed.Client.Services.Implementations
{
    public class ApiGetServices<T>(HttpClient httpClient, IAuthenticationServices authenticate)
     : IApiGetServices<T>
    {

        private readonly HttpClient _httpClient = httpClient;
        private readonly IAuthenticationServices _authenticate = authenticate;

        private static readonly JsonSerializerOptions _readOptions = new()
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task<Result<T>> GetAsync(string url, CancellationToken cancellationToken)
        {
            try
            {

                //if (typeof(T) != typeof(AllTalesResponse)
                //    && typeof(T) != typeof(TaleResponse)
                //    && !await _authenticate.CheckJwtTokenAsync())
                //{
                //    return "X";
                //}

                // send request
                var response = await _httpClient.GetAsync(url, cancellationToken);

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
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

    }

}
