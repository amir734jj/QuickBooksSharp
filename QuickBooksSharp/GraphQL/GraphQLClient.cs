using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using QuickBooksSharp.Infrastructure;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLClient : IGraphQLClient
    {
        private readonly string _accessToken;
        private readonly long? _realmId;
        private readonly string _endpoint;
        private readonly IRunPolicy _runPolicy;

        private static readonly HttpClient _httpClient = new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = System.Net.DecompressionMethods.GZip
        });

        static GraphQLClient()
        {
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(QuickBooksSharp), typeof(GraphQLClient).Assembly.GetName().Version!.ToString()));
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(github.com/better-reports/QuickBooksSharp)"));
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public GraphQLClient(string accessToken, long? realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _accessToken = accessToken;
            _realmId = realmId;
            _endpoint = GraphQLUrl.GetEndpoint(useSandbox);
            _runPolicy = runPolicy ?? RunPolicy.DefaultRunPolicy;
        }

        public Task<GraphQLResponse<TData>> SendQueryAsync<TData>(string query, object? variables = null, string? operationName = null) where TData : class
        {
            return SendAsync<TData>(query, variables, operationName);
        }

        public Task<GraphQLResponse<TData>> SendMutationAsync<TData>(string query, object? variables = null, string? operationName = null) where TData : class
        {
            return SendAsync<TData>(query, variables, operationName);
        }

        private async Task<GraphQLResponse<TData>> SendAsync<TData>(string query, object? variables, string? operationName) where TData : class
        {
            var graphQLRequest = new GraphQLRequest
            {
                Query = query,
                Variables = variables,
                OperationName = operationName
            };

            var jsonContent = JsonSerializer.Serialize(graphQLRequest, QuickBooksHttpClient.JsonSerializerOptions);

            var response = await _runPolicy.RunAsync(_realmId, async () =>
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, _endpoint)
                {
                    Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
                })
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    var httpResponse = await _httpClient.SendAsync(request);
                    var ex = httpResponse.IsSuccessStatusCode ? null : new QuickBooksException(request, httpResponse, await httpResponse.Content.ReadAsStringAsync());

                    if (ex?.IsRateLimit == true)
                        RunPolicy.NotifyRateLimt(new RateLimitEvent(_realmId, request.RequestUri!));

                    return new QuickBooksAPIResponse(httpResponse, ex);
                }
            });

            var result = await response.Content.ReadFromJsonAsync<GraphQLResponse<TData>>(QuickBooksHttpClient.JsonSerializerOptions);
            return result!;
        }
    }
}
