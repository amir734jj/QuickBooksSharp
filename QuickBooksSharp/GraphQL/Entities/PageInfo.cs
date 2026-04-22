using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
    /// <summary>
    /// Relay-spec compliant PageInfo.
    /// See: https://relay.dev/graphql/connections.htm
    /// </summary>
    public class PageInfo
    {
        [JsonPropertyName("hasNextPage")]
        public bool HasNextPage { get; set; }

        [JsonPropertyName("hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        [JsonPropertyName("startCursor")]
        public string? StartCursor { get; set; }

        [JsonPropertyName("endCursor")]
        public string? EndCursor { get; set; }
    }
}
