using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; } = null!;

        [JsonPropertyName("operationName")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? OperationName { get; set; }

        [JsonPropertyName("variables")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Variables { get; set; }
    }
}
