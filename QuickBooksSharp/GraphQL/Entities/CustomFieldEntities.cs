using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CustomFieldDataType
    {
        [EnumMember(Value = "STRING")]
        STRING,

        [EnumMember(Value = "NUMBER")]
        NUMBER,

        [EnumMember(Value = "DATE")]
        DATE,

        [EnumMember(Value = "DROPDOWN")]
        DROPDOWN,

        [EnumMember(Value = "BOOLEAN")]
        BOOLEAN
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CustomFieldAllowedOperation
    {
        [EnumMember(Value = "READ")]
        READ,

        [EnumMember(Value = "WRITE")]
        WRITE,

        [EnumMember(Value = "FILTERABLE")]
        FILTERABLE,

        [EnumMember(Value = "SORTABLE")]
        SORTABLE
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CustomFieldAssociationCondition
    {
        [EnumMember(Value = "REQUIRED")]
        REQUIRED,

        [EnumMember(Value = "OPTIONAL")]
        OPTIONAL
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum CustomFieldCreatedSource
    {
        [EnumMember(Value = "AUTO_ENABLE_CF")]
        AUTO_ENABLE_CF,

        [EnumMember(Value = "ONE_CLICK_CF")]
        ONE_CLICK_CF,

        [EnumMember(Value = "MANUAL")]
        MANUAL
    }

    public class CustomFieldDefinition
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("legacyID")]
        public string LegacyID { get; set; } = null!;

        [JsonPropertyName("legacyIDV2")]
        public string LegacyIDV2 { get; set; } = null!;

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("dataType")]
        public CustomFieldDataType DataType { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("required")]
        public bool? Required { get; set; }

        [JsonPropertyName("entityVersion")]
        public int? EntityVersion { get; set; }

        [JsonPropertyName("colorCode")]
        public string? ColorCode { get; set; }

        [JsonPropertyName("createdSource")]
        public CustomFieldCreatedSource? CreatedSource { get; set; }

        [JsonPropertyName("associations")]
        public CustomFieldAssociation[]? Associations { get; set; }

        [JsonPropertyName("dropDownOptions")]
        public CustomFieldDropDownOption[]? DropDownOptions { get; set; }
    }

    public class CustomFieldAssociation
    {
        [JsonPropertyName("entityType")]
        public string? EntityType { get; set; }

        [JsonPropertyName("allowedOperations")]
        public CustomFieldAllowedOperation[]? AllowedOperations { get; set; }

        [JsonPropertyName("condition")]
        public CustomFieldAssociationCondition? Condition { get; set; }
    }

    public class CustomFieldDropDownOption
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }
    }

    public class CustomFieldDefinitionEdge
    {
        [JsonPropertyName("node")]
        public CustomFieldDefinition? Node { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
    }

    public class CustomFieldDefinitionsConnection
    {
        [JsonPropertyName("edges")]
        public CustomFieldDefinitionEdge[]? Edges { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class CustomFieldDefinitionCreateInput
    {
        [JsonPropertyName("label")]
        public string Label { get; set; } = null!;

        [JsonPropertyName("dataType")]
        public CustomFieldDataType DataType { get; set; }

        [JsonPropertyName("associations")]
        public CustomFieldAssociationInput[] Associations { get; set; } = null!;

        [JsonPropertyName("active")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Active { get; set; }

        [JsonPropertyName("dropDownOptions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomFieldDropDownOptionInput[]? DropDownOptions { get; set; }
    }

    public class CustomFieldDefinitionUpdateInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("label")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Label { get; set; }

        [JsonPropertyName("dataType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomFieldDataType? DataType { get; set; }

        [JsonPropertyName("associations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomFieldAssociationInput[]? Associations { get; set; }

        [JsonPropertyName("active")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Active { get; set; }

        [JsonPropertyName("dropDownOptions")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomFieldDropDownOptionInput[]? DropDownOptions { get; set; }

        [JsonPropertyName("legacyIDV2")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LegacyIDV2 { get; set; }
    }

    public class CustomFieldAssociationInput
    {
        [JsonPropertyName("entityType")]
        public string EntityType { get; set; } = null!;

        [JsonPropertyName("allowedOperations")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomFieldAllowedOperation[]? AllowedOperations { get; set; }

        [JsonPropertyName("condition")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CustomFieldAssociationCondition? Condition { get; set; }
    }

    public class CustomFieldDropDownOptionInput
    {
        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Id { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;

        [JsonPropertyName("active")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Active { get; set; }
    }

    public class CustomFieldDefinitionsFilter
    {
        [JsonPropertyName("active")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Active { get; set; }

        [JsonPropertyName("entityType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EntityType { get; set; }
    }
}
