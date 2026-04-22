using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLResponse<TData> where TData : class
    {
        [JsonPropertyName("data")]
        public TData? Data { get; set; }

        [JsonPropertyName("errors")]
        public GraphQLError[]? Errors { get; set; }

        [JsonPropertyName("extensions")]
        public GraphQLExtensions? Extensions { get; set; }

        public bool HasErrors => Errors != null && Errors.Length > 0;
    }

    public class GraphQLError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("locations")]
        public GraphQLErrorLocation[]? Locations { get; set; }

        [JsonPropertyName("path")]
        public object[]? Path { get; set; }

        [JsonPropertyName("extensions")]
        public GraphQLErrorExtensions? Extensions { get; set; }
    }

    public class GraphQLErrorLocation
    {
        [JsonPropertyName("line")]
        public int Line { get; set; }

        [JsonPropertyName("column")]
        public int Column { get; set; }
    }

    public class GraphQLErrorExtensions
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("classification")]
        public string? Classification { get; set; }
    }

    public class GraphQLExtensions
    {
        [JsonPropertyName("requestId")]
        public string? RequestId { get; set; }
    }
}
