using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL.Entities
{
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
    public enum CustomFieldAssociationCondition
    {
        [EnumMember(Value = "REQUIRED")]
        REQUIRED,

        [EnumMember(Value = "OPTIONAL")]
        OPTIONAL
    }
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
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        [JsonProperty("legacyID")]
        public string LegacyID { get; set; } = null!;

        [JsonProperty("legacyIDV2")]
        public string LegacyIDV2 { get; set; } = null!;
        [JsonProperty("label")]
        public string? Label { get; set; }
        [JsonProperty("dataType")]
        public CustomFieldDataType DataType { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("required")]
        public bool? Required { get; set; }
        [JsonProperty("entityVersion")]
        public int? EntityVersion { get; set; }
        [JsonProperty("colorCode")]
        public string? ColorCode { get; set; }
        [JsonProperty("createdSource")]
        public CustomFieldCreatedSource? CreatedSource { get; set; }
        [JsonProperty("associations")]
        public CustomFieldAssociation[]? Associations { get; set; }
        [JsonProperty("dropDownOptions")]
        public CustomFieldDropDownOption[]? DropDownOptions { get; set; }
    }

    public class CustomFieldAssociation
    {
        [JsonProperty("entityType")]
        public string? EntityType { get; set; }
        [JsonProperty("allowedOperations")]
        public CustomFieldAllowedOperation[]? AllowedOperations { get; set; }
        [JsonProperty("condition")]
        public CustomFieldAssociationCondition? Condition { get; set; }
    }

    public class CustomFieldDropDownOption
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("value")]
        public string? Value { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
    }

    public class CustomFieldDefinitionEdge
    {
        [JsonProperty("node")]
        public CustomFieldDefinition? Node { get; set; }
        [JsonProperty("cursor")]
        public string? Cursor { get; set; }
    }

    public class CustomFieldDefinitionsConnection
    {
        [JsonProperty("edges")]
        public CustomFieldDefinitionEdge[]? Edges { get; set; }
        [JsonProperty("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class CustomFieldDefinitionCreateInput
    {
        [JsonProperty("label")]
        public string Label { get; set; } = null!;
        [JsonProperty("dataType")]
        public CustomFieldDataType DataType { get; set; }
        [JsonProperty("associations")]
        public CustomFieldAssociationInput[] Associations { get; set; } = null!;
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("dropDownOptions")]
        public CustomFieldDropDownOptionInput[]? DropDownOptions { get; set; }
    }

    public class CustomFieldDefinitionUpdateInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("label")]
        public string? Label { get; set; }
        [JsonProperty("dataType")]
        public CustomFieldDataType? DataType { get; set; }
        [JsonProperty("associations")]
        public CustomFieldAssociationInput[]? Associations { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("dropDownOptions")]
        public CustomFieldDropDownOptionInput[]? DropDownOptions { get; set; }

        [JsonProperty("legacyIDV2")]
        public string? LegacyIDV2 { get; set; }
    }

    public class CustomFieldAssociationInput
    {
        [JsonProperty("entityType")]
        public string EntityType { get; set; } = null!;
        [JsonProperty("allowedOperations")]
        public CustomFieldAllowedOperation[]? AllowedOperations { get; set; }
        [JsonProperty("condition")]
        public CustomFieldAssociationCondition? Condition { get; set; }
    }

    public class CustomFieldDropDownOptionInput
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; } = null!;
        [JsonProperty("active")]
        public bool? Active { get; set; }
    }

    public class CustomFieldDefinitionsFilter
    {
        [JsonProperty("active")]
        public bool? Active { get; set; }
        [JsonProperty("entityType")]
        public string? EntityType { get; set; }
    }
}