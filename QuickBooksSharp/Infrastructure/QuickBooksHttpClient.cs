using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.Infrastructure
{
    public class QuickBooksHttpClient(string? accessToken, long? realmId, IRunPolicy runPolicy) : IQuickBooksHttpClient
    {
        private static HttpClient _httpClient = new(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip
        });

        public static readonly JsonSerializerSettings JsonSettings = new()
        {
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            Converters = { new StringEnumConverter() }
        };

        static QuickBooksHttpClient()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(QuickBooksSharp), typeof(QuickBooksHttpClient).Assembly.GetName().Version!.ToString()));
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(github.com/better-reports/QuickBooksSharp)"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TResponse> GetAsync<TResponse>(Url url)
        {
            Func<HttpRequestMessage> makeRequest = () => new HttpRequestMessage(HttpMethod.Get, url);
            return await this.SendAsync<TResponse>(makeRequest);
        }

        public async Task<TResponse> PostAsync<TResponse>(Url url, object content)
        {
            Func<HttpRequestMessage> makeRequest = () => new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(content, JsonSettings), Encoding.UTF8, "application/json")
            };
            return await this.SendAsync<TResponse>(makeRequest);
        }

        public async Task<TResponse> SendAsync<TResponse>(Func<HttpRequestMessage> makeRequest)
        {
            var response = await this.SendAsync(makeRequest);
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(content, JsonSettings)!;
        }

        public async Task<HttpResponseMessage> SendAsync(Func<HttpRequestMessage> makeRequest)
        {
            var response = await runPolicy.RunAsync(realmId, async () =>
            {
                using var request = makeRequest();
                if (accessToken != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                }

                var response = await _httpClient.SendAsync(request);
                var ex = response.IsSuccessStatusCode ? null : new QuickBooksException(request, response, await response.Content.ReadAsStringAsync());

                if (ex?.IsRateLimit == true)
                {
                    RunPolicy.NotifyRateLimt(new RateLimitEvent(realmId, request.RequestUri));
                }

                return new QuickBooksAPIResponse(response, ex);
            });

            return response;
        }
    }
}
