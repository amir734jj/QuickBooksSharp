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
        private readonly GraphQLClient _client;

        public ProjectService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<ProjectQueryData>> GetProjectAsync(string id, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetProject");
            return await _client.SendQueryAsync<ProjectQueryData>(query, new { id }, "GetProject");
        }

        public async Task<GraphQLResponse<ProjectsQueryData>> GetProjectsAsync(int first, string? after = null, ProjectFilter? filter = null, ProjectOrderBy[]? orderBy = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetProjects");
            return await _client.SendQueryAsync<ProjectsQueryData>(query, new { first, after, filter, orderBy }, "GetProjects");
        }

        public async Task<GraphQLResponse<CreateProjectData>> CreateProjectAsync(CreateProjectInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("CreateProject");
            return await _client.SendMutationAsync<CreateProjectData>(query, new { input }, "CreateProject");
        }

        public async Task<GraphQLResponse<UpdateProjectData>> UpdateProjectAsync(UpdateProjectInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("UpdateProject");
            return await _client.SendMutationAsync<UpdateProjectData>(query, new { input }, "UpdateProject");
        }

        public async Task<GraphQLResponse<DeleteProjectData>> DeleteProjectAsync(DeleteProjectInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("DeleteProject");
            return await _client.SendMutationAsync<DeleteProjectData>(query, new { input }, "DeleteProject");
        }
    }
}
