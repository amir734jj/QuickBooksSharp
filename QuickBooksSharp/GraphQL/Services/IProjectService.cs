using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Query a single project by ID.
        /// </summary>
        Task<GraphQLResponse<ProjectQueryData>> GetProjectAsync(string id, string? customQuery = null);

        /// <summary>
        /// Query a list of projects with pagination, filtering, and ordering.
        /// </summary>
        Task<GraphQLResponse<ProjectsQueryData>> GetProjectsAsync(int first, string? after = null, ProjectFilter? filter = null, ProjectOrderBy[]? orderBy = null, string? customQuery = null);

        /// <summary>
        /// Create a new project.
        /// </summary>
        Task<GraphQLResponse<CreateProjectData>> CreateProjectAsync(CreateProjectInput input, string? customQuery = null);

        /// <summary>
        /// Update an existing project.
        /// </summary>
        Task<GraphQLResponse<UpdateProjectData>> UpdateProjectAsync(UpdateProjectInput input, string? customQuery = null);

        /// <summary>
        /// Delete a project (soft delete).
        /// </summary>
        Task<GraphQLResponse<DeleteProjectData>> DeleteProjectAsync(DeleteProjectInput input, string? customQuery = null);
    }
}
