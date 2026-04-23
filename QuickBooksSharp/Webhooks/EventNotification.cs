using Newtonsoft.Json;

namespace QuickBooksSharp.Webhooks
{
    public class EventNotification
    {
        [JsonProperty("realmId")]
        public string RealmId { get; set; } = default!;
        [JsonProperty("dataChangeEvent")]
        public DataChangeEvent DataChangeEvent { get; set; } = default!;
    }
}
