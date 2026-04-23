using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL.Entities
{
    public class DimensionDefinition
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("label")]
        public string Label { get; set; } = null!;
        [JsonProperty("dataType")]
        public CustomFieldDataType DataType { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("required")]
        public bool? Required { get; set; }
        [JsonProperty("associations")]
        public CustomFieldAssociation[]? Associations { get; set; }
        [JsonProperty("sharedInfo")]
        public DimensionSharedInfo? SharedInfo { get; set; }
    }

    public class DimensionSharedInfo
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
    }

    public class DimensionDefinitionEdge
    {
        [JsonProperty("node")]
        public DimensionDefinition? Node { get; set; }
        [JsonProperty("cursor")]
        public string? Cursor { get; set; }
    }

    public class DimensionDefinitionsConnection
    {
        [JsonProperty("edges")]
        public DimensionDefinitionEdge[]? Edges { get; set; }
        [JsonProperty("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class DimensionValue
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("value")]
        public string? Value { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("entityVersion")]
        public int? EntityVersion { get; set; }
    }

    public class DimensionValueEdge
    {
        [JsonProperty("node")]
        public DimensionValue? Node { get; set; }
        [JsonProperty("cursor")]
        public string? Cursor { get; set; }
    }

    public class DimensionValuesConnection
    {
        [JsonProperty("edges")]
        public DimensionValueEdge[]? Edges { get; set; }
        [JsonProperty("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class DimensionDefinitionsFilter
    {
        [JsonProperty("entityType")]
        public string? EntityType { get; set; }
    }

    public class DimensionValuesFilter
    {
        [JsonProperty("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;
    }

    public class DimensionValueCreateInput
    {
        [JsonProperty("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;
        [JsonProperty("value")]
        public string Value { get; set; } = null!;
    }

    public class DimensionValueUpdateInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;
        [JsonProperty("value")]
        public string? Value { get; set; }
        [JsonProperty("entityVersion")]
        public int? EntityVersion { get; set; }
    }

    public class DimensionValueDisableInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;
        [JsonProperty("entityVersion")]
        public int? EntityVersion { get; set; }
    }
}