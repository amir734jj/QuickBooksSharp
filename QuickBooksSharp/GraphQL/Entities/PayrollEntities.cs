using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
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
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("employeeId")]
        public string EmployeeId { get; set; } = null!;
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("employerCompensation")]
        public EmployerCompensation? EmployerCompensation { get; set; }
        [JsonProperty("rate")]
        public PayRate? Rate { get; set; }
    }

    public class EmployerCompensation
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
        [JsonProperty("active")]
        public bool Active { get; set; }
        [JsonProperty("type")]
        public PayrollVariableStringField? Type { get; set; }
    }

    public class PayrollVariableStringField
    {
        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    public class PayRate
    {
        [JsonProperty("amount")]
        public PayrollMoneyAmount? Amount { get; set; }
        [JsonProperty("payUnit")]
        public PayrollPayUnit? PayUnit { get; set; }
    }

    public class PayrollMoneyAmount
    {
        [JsonProperty("amount")]
        public decimal? Amount { get; set; }
        [JsonProperty("currencyCode")]
        public string? CurrencyCode { get; set; }
    }

    public class EmployeeCompensationEdge
    {
        [JsonProperty("node")]
        public EmployeeCompensation? Node { get; set; }
        [JsonProperty("cursor")]
        public string? Cursor { get; set; }
    }

    public class EmployeeCompensationConnection
    {
        [JsonProperty("edges")]
        public EmployeeCompensationEdge[]? Edges { get; set; }
        [JsonProperty("nodes")]
        public EmployeeCompensation[]? Nodes { get; set; }
        [JsonProperty("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class EmployeeCompensationsFilter
    {
        [JsonProperty("employeeId")]
        public string? EmployeeId { get; set; }
        [JsonProperty("intuitAccountId")]
        public string? IntuitAccountId { get; set; }
        [JsonProperty("active")]
        public bool? Active { get; set; }
    }
}