using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL.Entities
{
    /// <summary>
    /// Relay-spec compliant PageInfo.
    /// See: https://relay.dev/graphql/connections.htm
    /// </summary>
    public class PageInfo
    {
        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; set; }

        [JsonProperty("hasPreviousPage")]
        public bool HasPreviousPage { get; set; }

        [JsonProperty("startCursor")]
        public string? StartCursor { get; set; }

        [JsonProperty("endCursor")]
        public string? EndCursor { get; set; }
    }
}
