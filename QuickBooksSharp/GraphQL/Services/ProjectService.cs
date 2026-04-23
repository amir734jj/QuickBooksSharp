using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class ProjectQueryData
    {
        [JsonProperty("projectManagementProject")]
        public Project? Project { get; set; }
    }

    public class ProjectsQueryData
    {
        [JsonProperty("projectManagementProjects")]
        public ProjectConnection? Projects { get; set; }
    }

    public class CreateProjectData
    {
        [JsonProperty("projectManagementCreateProject")]
        public ProjectMutationResult? Result { get; set; }
    }

    public class UpdateProjectData
    {
        [JsonProperty("projectManagementUpdateProject")]
        public ProjectMutationResult? Result { get; set; }
    }

    public class DeleteProjectData
    {
        [JsonProperty("projectManagementDeleteProject")]
        public ProjectMutationResult? Result { get; set; }
    }

    /// <summary>
    /// Union type: ProjectManagement_ProjectResponse = ProjectManagement_Project | ProjectManagement_Error
    /// Deserialize by checking which fields are populated.
    /// </summary>
    public class ProjectMutationResult
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("version")]
        public int? Version { get; set; }

        [JsonProperty("status")]
        public ProjectStatus? Status { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("message")]
        public string? ErrorMessage { get; set; }

        [JsonProperty("classification")]
        public string? ErrorClassification { get; set; }

        public bool IsError => ErrorMessage != null;
    }

    public class ProjectService : IProjectService
    {
        private readonly GraphQLClient _client;

        public ProjectService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null, ILogger? logger = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy, logger);
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
