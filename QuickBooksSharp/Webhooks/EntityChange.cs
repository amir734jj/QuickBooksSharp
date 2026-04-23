using System;
using Newtonsoft.Json;
using QuickBooksSharp.Entities;

namespace QuickBooksSharp.Webhooks
{
    /// <summary>
    /// Information about the entity that changed (customer, Invoice, etc.)
    /// </summary>
    public class EntityChange
    {
        /// <summary>
        /// The name of the entity that changed (customer, Invoice, etc.)
        /// </summary>
        [JsonProperty("name")]
        public EntityChangedName Name { get; set; }
        /// <summary>
        /// The ID of the changed entity
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = default!;
        /// <summary>
        /// The type of change
        /// </summary>
        [JsonProperty("operation")]
        public OperationEnum Operation { get; set; }
        /// <summary>
        /// The latest timestamp in UTC
        /// </summary>
        [JsonProperty("lastUpdated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// The ID of the deleted or merged entity (this only applies to merge events)
        /// </summary>
        [JsonProperty("deletedId")]
        public string? DeletedId { get; set; }
    }
}
