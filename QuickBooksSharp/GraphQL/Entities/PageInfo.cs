using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL.Entities
{    public class PageInfo
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
