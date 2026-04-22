using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface IProjectService
    {
        /// <summary>
        /// Query a single project by ID.
        /// Scope required: project-management.project
        /// </summary>
        /// <param name="id">The project ID</param>
        /// <param name="customQuery">Optional custom GraphQL query to override the default embedded query</param>
        Task<GraphQLResponse<ProjectQueryData>> GetProjectAsync(string id, string? customQuery = null);

        /// <summary>
        /// Query a list of projects with pagination, filtering, and ordering.
        /// Scope required: project-management.project
        /// </summary>
        /// <param name="customQuery">Optional custom GraphQL query to override the default embedded query</param>
        Task<GraphQLResponse<ProjectsQueryData>> GetProjectsAsync(int first, string? after = null, ProjectFilter? filter = null, ProjectOrderBy[]? orderBy = null, string? customQuery = null);

        /// <summary>
        /// Create a new project.
        /// Scope required: project-management.project
        /// </summary>
        /// <param name="customQuery">Optional custom GraphQL mutation to override the default embedded query</param>
        Task<GraphQLResponse<CreateProjectData>> CreateProjectAsync(CreateProjectInput input, string? customQuery = null);

        /// <summary>
        /// Update an existing project.
        /// Scope required: project-management.project
        /// </summary>
        /// <param name="customQuery">Optional custom GraphQL mutation to override the default embedded query</param>
        Task<GraphQLResponse<UpdateProjectData>> UpdateProjectAsync(UpdateProjectInput input, string? customQuery = null);

        /// <summary>
        /// Delete a project (soft delete).
        /// Scope required: project-management.project
        /// </summary>
        /// <param name="customQuery">Optional custom GraphQL mutation to override the default embedded query</param>
        Task<GraphQLResponse<DeleteProjectData>> DeleteProjectAsync(DeleteProjectInput input, string? customQuery = null);
    }
}
