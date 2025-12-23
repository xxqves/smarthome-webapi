using System.Net.Http.Headers;

namespace SmHm.IntegrationTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static void Authorize(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "fake-jwt-token");
        }
    }
}
