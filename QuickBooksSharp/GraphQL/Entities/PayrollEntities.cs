using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum PayrollPayUnit
    {
        [EnumMember(Value = "HOURLY")]
        HOURLY,

        [EnumMember(Value = "WEEKLY")]
        WEEKLY,

        [EnumMember(Value = "BIWEEKLY")]
        BIWEEKLY,

        [EnumMember(Value = "SEMIMONTHLY")]
        SEMIMONTHLY,

        [EnumMember(Value = "MONTHLY")]
        MONTHLY,

        [EnumMember(Value = "ANNUALLY")]
        ANNUALLY,

        [EnumMember(Value = "FLAT")]
        FLAT
    }

    public class EmployeeCompensation
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("employeeId")]
        public string EmployeeId { get; set; } = null!;

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("employerCompensation")]
        public EmployerCompensation? EmployerCompensation { get; set; }

        [JsonPropertyName("rate")]
        public PayRate? Rate { get; set; }
    }

    public class EmployerCompensation
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("type")]
        public PayrollVariableStringField? Type { get; set; }
    }

    public class PayrollVariableStringField
    {
        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }

    public class PayRate
    {
        [JsonPropertyName("amount")]
        public PayrollMoneyAmount? Amount { get; set; }

        [JsonPropertyName("payUnit")]
        public PayrollPayUnit? PayUnit { get; set; }
    }

    public class PayrollMoneyAmount
    {
        [JsonPropertyName("amount")]
        public decimal? Amount { get; set; }

        [JsonPropertyName("currencyCode")]
        public string? CurrencyCode { get; set; }
    }

    public class EmployeeCompensationEdge
    {
        [JsonPropertyName("node")]
        public EmployeeCompensation? Node { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
    }

    public class EmployeeCompensationConnection
    {
        [JsonPropertyName("edges")]
        public EmployeeCompensationEdge[]? Edges { get; set; }

        [JsonPropertyName("nodes")]
        public EmployeeCompensation[]? Nodes { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class EmployeeCompensationsFilter
    {
        [JsonPropertyName("employeeId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EmployeeId { get; set; }

        [JsonPropertyName("intuitAccountId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? IntuitAccountId { get; set; }

        [JsonPropertyName("active")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Active { get; set; }
    }
}
