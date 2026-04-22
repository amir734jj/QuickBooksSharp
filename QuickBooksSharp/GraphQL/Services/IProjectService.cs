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
        Task<GraphQLResponse<ProjectQueryData>> GetProjectAsync(string id, string? fields = null);

        /// <summary>
        /// Query a list of projects with pagination, filtering, and ordering.
        /// Scope required: project-management.project
        /// </summary>
        Task<GraphQLResponse<ProjectsQueryData>> GetProjectsAsync(int first, string? after = null, ProjectFilter? filter = null, ProjectOrderBy[]? orderBy = null, string? fields = null);

        /// <summary>
        /// Create a new project.
        /// Scope required: project-management.project
        /// </summary>
        Task<GraphQLResponse<CreateProjectData>> CreateProjectAsync(CreateProjectInput input, string? fields = null);

        /// <summary>
        /// Update an existing project.
        /// Scope required: project-management.project
        /// </summary>
        Task<GraphQLResponse<UpdateProjectData>> UpdateProjectAsync(UpdateProjectInput input, string? fields = null);

        /// <summary>
        /// Delete a project (soft delete).
        /// Scope required: project-management.project
        /// </summary>
        Task<GraphQLResponse<DeleteProjectData>> DeleteProjectAsync(DeleteProjectInput input);
    }
}
