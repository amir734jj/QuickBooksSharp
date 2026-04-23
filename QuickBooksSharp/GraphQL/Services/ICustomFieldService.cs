using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface ICustomFieldService
    {
        /// <summary>
        /// Query all custom field definitions with optional filtering and pagination.        /// </summary>
        Task<GraphQLResponse<CustomFieldDefinitionsQueryData>> GetCustomFieldDefinitionsAsync(int? first = null, string? after = null, CustomFieldDefinitionsFilter? filters = null, string? customQuery = null);

        /// <summary>
        /// Create a new custom field definition.        /// </summary>
        Task<GraphQLResponse<CreateCustomFieldDefinitionData>> CreateCustomFieldDefinitionAsync(CustomFieldDefinitionCreateInput input, string? customQuery = null);

        /// <summary>
        /// Update an existing custom field definition.        /// </summary>
        Task<GraphQLResponse<UpdateCustomFieldDefinitionData>> UpdateCustomFieldDefinitionAsync(CustomFieldDefinitionUpdateInput input, string? customQuery = null);
    }
}
