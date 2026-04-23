using Newtonsoft.Json;

namespace QuickBooksSharp.Webhooks
{
    public class DataChangeEvent
    {
        [JsonProperty("entities")]
        public EntityChange[] Entities { get; set; } = default!;
    }
}
