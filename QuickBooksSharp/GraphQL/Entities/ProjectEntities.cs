using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuickBooksSharp.GraphQL.Entities
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ProjectStatus
    {
        [EnumMember(Value = "OPEN")]
        OPEN,

        [EnumMember(Value = "IN_PROGRESS")]
        IN_PROGRESS,

        [EnumMember(Value = "BLOCKED")]
        BLOCKED,

        [EnumMember(Value = "CANCELED")]
        CANCELED,

        [EnumMember(Value = "COMPLETE")]
        COMPLETE,

        [EnumMember(Value = "OTHER")]
        OTHER,

        [EnumMember(Value = "WAITING_ON_CLIENT")]
        WAITING_ON_CLIENT
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ProjectOrderBy
    {
        [EnumMember(Value = "COMPLETED_DATE_ASC")]
        COMPLETED_DATE_ASC,

        [EnumMember(Value = "COMPLETED_DATE_DESC")]
        COMPLETED_DATE_DESC,

        [EnumMember(Value = "DUE_DATE_ASC")]
        DUE_DATE_ASC,

        [EnumMember(Value = "DUE_DATE_DESC")]
        DUE_DATE_DESC,

        [EnumMember(Value = "NAME_ASC")]
        NAME_ASC,

        [EnumMember(Value = "NAME_DESC")]
        NAME_DESC,

        [EnumMember(Value = "START_DATE_ASC")]
        START_DATE_ASC,

        [EnumMember(Value = "START_DATE_DESC")]
        START_DATE_DESC,

        [EnumMember(Value = "STATUS_ASC")]
        STATUS_ASC,

        [EnumMember(Value = "STATUS_DESC")]
        STATUS_DESC,

        [EnumMember(Value = "TYPE_ASC")]
        TYPE_ASC,

        [EnumMember(Value = "TYPE_DESC")]
        TYPE_DESC
    }

    public class Project
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("status")]
        public ProjectStatus? Status { get; set; }

        [JsonPropertyName("version")]
        public int Version { get; set; }

        [JsonPropertyName("startDate")]
        public string? StartDate { get; set; }

        [JsonPropertyName("dueDate")]
        public string? DueDate { get; set; }

        [JsonPropertyName("completedDate")]
        public string? CompletedDate { get; set; }

        [JsonPropertyName("completionRate")]
        public decimal? CompletionRate { get; set; }

        [JsonPropertyName("deleted")]
        public bool? Deleted { get; set; }

        [JsonPropertyName("pinned")]
        public bool? Pinned { get; set; }

        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("customer")]
        public ProjectCustomer? Customer { get; set; }

        [JsonPropertyName("client")]
        public ProjectClient? Client { get; set; }

        [JsonPropertyName("assignee")]
        public ProjectPersona? Assignee { get; set; }

        [JsonPropertyName("completedBy")]
        public ProjectUser? CompletedBy { get; set; }
    }

    public class ProjectCustomer
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectClient
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectPersona
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectUser
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectEdge
    {
        [JsonPropertyName("node")]
        public Project? Node { get; set; }

        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
    }

    public class ProjectConnection
    {
        [JsonPropertyName("edges")]
        public ProjectEdge[]? Edges { get; set; }

        [JsonPropertyName("nodes")]
        public Project[]? Nodes { get; set; }

        [JsonPropertyName("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class ProjectError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("classification")]
        public string? Classification { get; set; }
    }

    public class ProjectFilter
    {
        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectStatusExpression? Status { get; set; }

        [JsonPropertyName("customer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectIdExpression? Customer { get; set; }

        [JsonPropertyName("deleted")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Deleted { get; set; }

        [JsonPropertyName("id")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectIdExpression? Id { get; set; }

        [JsonPropertyName("includeTasks")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IncludeTasks { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectStringExpression? Type { get; set; }
    }

    public class ProjectStatusExpression
    {
        [JsonPropertyName("equals")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectStatus? EqualsValue { get; set; }
    }

    public class ProjectIdExpression
    {
        [JsonPropertyName("equals")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EqualsValue { get; set; }
    }

    public class ProjectStringExpression
    {
        [JsonPropertyName("equals")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? EqualsValue { get; set; }
    }

    public class CreateProjectInput
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonPropertyName("customer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectCustomerInput? Customer { get; set; }

        [JsonPropertyName("client")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectClientInput? Client { get; set; }

        [JsonPropertyName("assignee")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectPersonaInput? Assignee { get; set; }

        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectStatus? Status { get; set; }

        [JsonPropertyName("startDate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StartDate { get; set; }

        [JsonPropertyName("dueDate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DueDate { get; set; }

        [JsonPropertyName("completedDate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CompletedDate { get; set; }

        [JsonPropertyName("completionRate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? CompletionRate { get; set; }

        [JsonPropertyName("pinned")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Pinned { get; set; }

        [JsonPropertyName("priority")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Priority { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }
    }

    public class UpdateProjectInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Version { get; set; }

        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        [JsonPropertyName("customer")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectCustomerInput? Customer { get; set; }

        [JsonPropertyName("client")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectClientInput? Client { get; set; }

        [JsonPropertyName("assignee")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectPersonaInput? Assignee { get; set; }

        [JsonPropertyName("status")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ProjectStatus? Status { get; set; }

        [JsonPropertyName("startDate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StartDate { get; set; }

        [JsonPropertyName("dueDate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? DueDate { get; set; }

        [JsonPropertyName("completedDate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CompletedDate { get; set; }

        [JsonPropertyName("completionRate")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? CompletionRate { get; set; }

        [JsonPropertyName("pinned")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? Pinned { get; set; }

        [JsonPropertyName("priority")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Priority { get; set; }

        [JsonPropertyName("type")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Type { get; set; }
    }

    public class DeleteProjectInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("version")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Version { get; set; }
    }

    public class ProjectCustomerInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
    }

    public class ProjectClientInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
    }

    public class ProjectPersonaInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
    }

    public class ProjectUserInput
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;
    }
}
