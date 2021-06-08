using System.Net.Http;

namespace CovidTracking.API.Client.ApiConnection
{
    public class HttpInstance
    {
        private static HttpClient httpClientInstace;

        private HttpInstance() {}

        public static HttpClient GetHttpClientInstance()
        {
            if (httpClientInstace == null)
            {
                httpClientInstace = new HttpClient();
                httpClientInstace.DefaultRequestHeaders.ConnectionClose = false;
            }
            
            return httpClientInstace;
        }
    }
}