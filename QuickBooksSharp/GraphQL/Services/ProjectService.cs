using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class ProjectQueryData
    {
        [JsonPropertyName("projectManagementProject")]
        public Project? Project { get; set; }
    }

    public class ProjectsQueryData
    {
        [JsonPropertyName("projectManagementProjects")]
        public ProjectConnection? Projects { get; set; }
    }

    public class CreateProjectData
    {
        [JsonPropertyName("projectManagementCreateProject")]
        public ProjectMutationResult? Result { get; set; }
    }

    public class UpdateProjectData
    {
        [JsonPropertyName("projectManagementUpdateProject")]
        public ProjectMutationResult? Result { get; set; }
    }

    public class DeleteProjectData
    {
        [JsonPropertyName("projectManagementDeleteProject")]
        public ProjectMutationResult? Result { get; set; }
    }

    /// <summary>
    /// Union type: ProjectManagement_ProjectResponse = ProjectManagement_Project | ProjectManagement_Error
    /// Deserialize by checking which fields are populated.
    /// </summary>
    public class ProjectMutationResult
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("status")]
        public ProjectStatus? Status { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("message")]
        public string? ErrorMessage { get; set; }

        [JsonPropertyName("classification")]
        public string? ErrorClassification { get; set; }

        public bool IsError => ErrorMessage != null;
    }

    public class ProjectService : IProjectService
    {
        private const string DefaultProjectFields = @"
            id
            name
            description
            status
            version
            startDate
            dueDate
            completedDate
            completionRate
            deleted
            pinned
            priority
            type
            customer {
                id
                displayName
            }";

        private readonly GraphQLClient _client;

        public ProjectService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<ProjectQueryData>> GetProjectAsync(string id, string? fields = null)
        {
            var query = $@"
                query GetProject($id: ID!) {{
                    projectManagementProject(id: $id) {{
                        {fields ?? DefaultProjectFields}
                    }}
                }}";

            return await _client.SendQueryAsync<ProjectQueryData>(query, new { id }, "GetProject");
        }

        public async Task<GraphQLResponse<ProjectsQueryData>> GetProjectsAsync(int first, string? after = null, ProjectFilter? filter = null, ProjectOrderBy[]? orderBy = null, string? fields = null)
        {
            var query = $@"
                query GetProjects($first: PositiveInt!, $after: String, $filter: ProjectManagement_ProjectFilter, $orderBy: [ProjectManagement_OrderBy]) {{
                    projectManagementProjects(first: $first, after: $after, filter: $filter, orderBy: $orderBy) {{
                        edges {{
                            node {{
                                {fields ?? DefaultProjectFields}
                            }}
                            cursor
                        }}
                        pageInfo {{
                            hasNextPage
                            hasPreviousPage
                            startCursor
                            endCursor
                        }}
                    }}
                }}";

            return await _client.SendQueryAsync<ProjectsQueryData>(query, new { first, after, filter, orderBy }, "GetProjects");
        }

        public async Task<GraphQLResponse<CreateProjectData>> CreateProjectAsync(CreateProjectInput input, string? fields = null)
        {
            var selectedFields = fields ?? DefaultProjectFields;
            var query = $@"
                mutation CreateProject($input: ProjectManagement_CreateProjectInput!) {{
                    projectManagementCreateProject(input: $input) {{
                        ... on ProjectManagement_Project {{
                            {selectedFields}
                        }}
                        ... on ProjectManagement_Error {{
                            message
                            classification
                        }}
                    }}
                }}";

            return await _client.SendMutationAsync<CreateProjectData>(query, new { input }, "CreateProject");
        }

        public async Task<GraphQLResponse<UpdateProjectData>> UpdateProjectAsync(UpdateProjectInput input, string? fields = null)
        {
            var selectedFields = fields ?? DefaultProjectFields;
            var query = $@"
                mutation UpdateProject($input: ProjectManagement_UpdateProjectInput!) {{
                    projectManagementUpdateProject(input: $input) {{
                        ... on ProjectManagement_Project {{
                            {selectedFields}
                        }}
                        ... on ProjectManagement_Error {{
                            message
                            classification
                        }}
                    }}
                }}";

            return await _client.SendMutationAsync<UpdateProjectData>(query, new { input }, "UpdateProject");
        }

        public async Task<GraphQLResponse<DeleteProjectData>> DeleteProjectAsync(DeleteProjectInput input)
        {
            var query = @"
                mutation DeleteProject($input: ProjectManagement_DeleteProjectInput!) {
                    projectManagementDeleteProject(input: $input) {
                        ... on ProjectManagement_Project {
                            id
                            deleted
                        }
                        ... on ProjectManagement_Error {
                            message
                            classification
                        }
                    }
                }";

            return await _client.SendMutationAsync<DeleteProjectData>(query, new { input }, "DeleteProject");
        }
    }
}
