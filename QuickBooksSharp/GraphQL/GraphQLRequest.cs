using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLRequest
    {
        [JsonProperty("query")]
        public string Query { get; set; } = null!;

        [JsonProperty("operationName")]
        public string? OperationName { get; set; }

        [JsonProperty("variables")]
        public object? Variables { get; set; }
    }
}
