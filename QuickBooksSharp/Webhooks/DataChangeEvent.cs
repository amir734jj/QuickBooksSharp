using System.Text.Json.Serialization;

namespace QuickBooksSharp.Webhooks
{
    public class DataChangeEvent
    {
        [JsonPropertyName("entities")]
        public EntityChange[] Entities { get; set; } = default!;
    }
}
