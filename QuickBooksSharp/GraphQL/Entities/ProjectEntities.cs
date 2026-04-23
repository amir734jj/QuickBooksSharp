using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace QuickBooksSharp.GraphQL.Entities
{
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
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("status")]
        public ProjectStatus? Status { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("startDate")]
        public string? StartDate { get; set; }
        [JsonProperty("dueDate")]
        public string? DueDate { get; set; }
        [JsonProperty("completedDate")]
        public string? CompletedDate { get; set; }
        [JsonProperty("completionRate")]
        public decimal? CompletionRate { get; set; }
        [JsonProperty("deleted")]
        public bool? Deleted { get; set; }
        [JsonProperty("pinned")]
        public bool? Pinned { get; set; }
        [JsonProperty("priority")]
        public int? Priority { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
        [JsonProperty("customer")]
        public ProjectCustomer? Customer { get; set; }
        [JsonProperty("client")]
        public ProjectClient? Client { get; set; }
        [JsonProperty("assignee")]
        public ProjectPersona? Assignee { get; set; }
        [JsonProperty("completedBy")]
        public ProjectUser? CompletedBy { get; set; }
    }

    public class ProjectCustomer
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectClient
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectPersona
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectUser
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
    }

    public class ProjectEdge
    {
        [JsonProperty("node")]
        public Project? Node { get; set; }
        [JsonProperty("cursor")]
        public string? Cursor { get; set; }
    }

    public class ProjectConnection
    {
        [JsonProperty("edges")]
        public ProjectEdge[]? Edges { get; set; }
        [JsonProperty("nodes")]
        public Project[]? Nodes { get; set; }
        [JsonProperty("pageInfo")]
        public PageInfo? PageInfo { get; set; }
    }

    public class ProjectError
    {
        [JsonProperty("message")]
        public string Message { get; set; } = null!;
        [JsonProperty("classification")]
        public string? Classification { get; set; }
    }

    public class ProjectFilter
    {
        [JsonProperty("status")]
        public ProjectStatusExpression? Status { get; set; }
        [JsonProperty("customer")]
        public ProjectIdExpression? Customer { get; set; }
        [JsonProperty("deleted")]
        public bool? Deleted { get; set; }
        [JsonProperty("id")]
        public ProjectIdExpression? Id { get; set; }
        [JsonProperty("includeTasks")]
        public bool? IncludeTasks { get; set; }
        [JsonProperty("type")]
        public ProjectStringExpression? Type { get; set; }
    }

    public class ProjectStatusExpression
    {
        [JsonProperty("equals")]
        public ProjectStatus? EqualsValue { get; set; }
    }

    public class ProjectIdExpression
    {
        [JsonProperty("equals")]
        public string? EqualsValue { get; set; }
    }

    public class ProjectStringExpression
    {
        [JsonProperty("equals")]
        public string? EqualsValue { get; set; }
    }

    public class CreateProjectInput
    {
        [JsonProperty("name")]
        public string Name { get; set; } = null!;
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("customer")]
        public ProjectCustomerInput? Customer { get; set; }
        [JsonProperty("client")]
        public ProjectClientInput? Client { get; set; }
        [JsonProperty("assignee")]
        public ProjectPersonaInput? Assignee { get; set; }
        [JsonProperty("status")]
        public ProjectStatus? Status { get; set; }
        [JsonProperty("startDate")]
        public string? StartDate { get; set; }
        [JsonProperty("dueDate")]
        public string? DueDate { get; set; }
        [JsonProperty("completedDate")]
        public string? CompletedDate { get; set; }
        [JsonProperty("completionRate")]
        public decimal? CompletionRate { get; set; }
        [JsonProperty("pinned")]
        public bool? Pinned { get; set; }
        [JsonProperty("priority")]
        public int? Priority { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class UpdateProjectInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("version")]
        public int? Version { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("customer")]
        public ProjectCustomerInput? Customer { get; set; }
        [JsonProperty("client")]
        public ProjectClientInput? Client { get; set; }
        [JsonProperty("assignee")]
        public ProjectPersonaInput? Assignee { get; set; }
        [JsonProperty("status")]
        public ProjectStatus? Status { get; set; }
        [JsonProperty("startDate")]
        public string? StartDate { get; set; }
        [JsonProperty("dueDate")]
        public string? DueDate { get; set; }
        [JsonProperty("completedDate")]
        public string? CompletedDate { get; set; }
        [JsonProperty("completionRate")]
        public decimal? CompletionRate { get; set; }
        [JsonProperty("pinned")]
        public bool? Pinned { get; set; }
        [JsonProperty("priority")]
        public int? Priority { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
    }

    public class DeleteProjectInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
        [JsonProperty("version")]
        public int? Version { get; set; }
    }

    public class ProjectCustomerInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }

    public class ProjectClientInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }

    public class ProjectPersonaInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }

    public class ProjectUserInput
    {
        [JsonProperty("id")]
        public string Id { get; set; } = null!;
    }
}