using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
        private readonly ILogger _logger;

        public static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = { new StringEnumConverter() }
        };

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

        public GraphQLClient(string accessToken, long? realmId, bool useSandbox, IRunPolicy? runPolicy = null, ILogger? logger = null)
        {
            _accessToken = accessToken;
            _realmId = realmId;
            _endpoint = GraphQLUrl.GetEndpoint(useSandbox);
            _runPolicy = runPolicy ?? RunPolicy.DefaultRunPolicy;
            _logger = logger ?? NullLogger.Instance;
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

            var jsonContent = JsonConvert.SerializeObject(graphQLRequest, JsonSettings);

            _logger.LogDebug("GraphQL {Operation} to {Endpoint}", operationName ?? "(unnamed)", _endpoint);

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
                    {
                        _logger.LogWarning("Rate limit hit for realm {RealmId} on {Uri}", _realmId, request.RequestUri);
                        RunPolicy.NotifyRateLimt(new RateLimitEvent(_realmId, request.RequestUri!));
                    }
                    else if (ex != null)
                    {
                        _logger.LogError("GraphQL request failed: {StatusCode} {Reason}", (int)httpResponse.StatusCode, httpResponse.ReasonPhrase);
                    }

                    return new QuickBooksAPIResponse(httpResponse, ex);
                }
            });

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GraphQLResponse<TData>>(responseContent, JsonSettings);

            if (result?.HasErrors == true)
            {
                _logger.LogWarning("GraphQL response contains {ErrorCount} error(s): {Errors}",
                    result.Errors!.Length,
                    string.Join("; ", System.Linq.Enumerable.Select(result.Errors, e => e.Message)));
            }
            else
            {
                _logger.LogDebug("GraphQL {Operation} completed successfully", operationName ?? "(unnamed)");
            }

            return result!;
        }
    }
}
