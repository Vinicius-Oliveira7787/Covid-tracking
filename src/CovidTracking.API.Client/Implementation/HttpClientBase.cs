using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CovidTracking.API.Client.Implementation
{
    public abstract class HttpClientBase
    {
        protected abstract HttpClient CreateHttpClient();

        private static HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string uri)
        {
            return new HttpRequestMessage(method, uri);
        }

        private static void HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                //var content = await response.Content.ReadAsStringAsync();
                // TODO: Notificar erro
            }
        }

        private static T FromJson<T>(string json)
        {
            return (T)JsonSerializer.Deserialize(json, typeof(T));
        }

        protected async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            var client = CreateHttpClient();
            var request = CreateHttpRequestMessage(HttpMethod.Get, uri);

            var response = await client.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);

            HandleResponse(response);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            var result = FromJson<TResponse>(content);

            return result;
        }
    }
}
