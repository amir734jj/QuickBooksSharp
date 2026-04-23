using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLResponse<TData> where TData : class
    {
        [JsonProperty("data")]
        public TData? Data { get; set; }

        [JsonProperty("errors")]
        public GraphQLError[]? Errors { get; set; }

        [JsonProperty("extensions")]
        public GraphQLExtensions? Extensions { get; set; }

        public bool HasErrors => Errors != null && Errors.Length > 0;
    }

    public class GraphQLError
    {
        [JsonProperty("message")]
        public string Message { get; set; } = null!;

        [JsonProperty("locations")]
        public GraphQLErrorLocation[]? Locations { get; set; }

        [JsonProperty("path")]
        public object[]? Path { get; set; }

        [JsonProperty("extensions")]
        public GraphQLErrorExtensions? Extensions { get; set; }
    }

    public class GraphQLErrorLocation
    {
        [JsonProperty("line")]
        public int Line { get; set; }

        [JsonProperty("column")]
        public int Column { get; set; }
    }

    public class GraphQLErrorExtensions
    {
        [JsonProperty("code")]
        public string? Code { get; set; }

        [JsonProperty("classification")]
        public string? Classification { get; set; }
    }

    public class GraphQLExtensions
    {
        [JsonProperty("requestId")]
        public string? RequestId { get; set; }
    }
}
