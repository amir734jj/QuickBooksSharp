using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
    public class DimensionDefinition
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("label")]
        public string Label { get; set; } = null!;

        [JsonPropertyName("dataType")]
        public CustomFieldDataType DataType { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }

        [JsonPropertyName("associations")]
        public CustomFieldAssociation[]? Associations { get; set; }

        [JsonPropertyName("sharedInfo")]
        public DimensionSharedInfo? SharedInfo { get; set; }
    }

    public class DimensionSharedInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class DimensionDefinitionEdge
    {
        [JsonPropertyName("node")]
        public DimensionDefinition? Node { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
    }

    public class DimensionDefinitionsConnection
    {
        [JsonPropertyName("edges")]
        public DimensionDefinitionEdge[]? Edges { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class DimensionValue
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("entityVersion")]
        public int? EntityVersion { get; set; }
    }

    public class DimensionValueEdge
    {
        [JsonPropertyName("node")]
        public DimensionValue? Node { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
    }

    public class DimensionValuesConnection
    {
        [JsonPropertyName("edges")]
        public DimensionValueEdge[]? Edges { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class DimensionDefinitionsFilter
    {
        [JsonPropertyName("entityType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EntityType { get; set; }
    }

    public class DimensionValuesFilter
    {
        [JsonPropertyName("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;
    }

    public class DimensionValueCreateInput
    {
        [JsonPropertyName("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;

        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;
    }

    public class DimensionValueUpdateInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;

        [JsonPropertyName("value")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Value { get; set; }

        [JsonPropertyName("entityVersion")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? EntityVersion { get; set; }
    }

    public class DimensionValueDisableInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("dimensionDefinitionId")]
        public string DimensionDefinitionId { get; set; } = null!;

        [JsonPropertyName("entityVersion")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? EntityVersion { get; set; }
    }
}
