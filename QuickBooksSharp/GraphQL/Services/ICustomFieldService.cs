using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface ICustomFieldService
    {
        /// <summary>
        /// Query all custom field definitions with optional filtering and pagination.
        /// Scope required: app-foundations.custom-field-definitions.read
        /// </summary>
        Task<GraphQLResponse<CustomFieldDefinitionsQueryData>> GetCustomFieldDefinitionsAsync(int? first = null, string? after = null, CustomFieldDefinitionsFilter? filters = null, string? fields = null);

        /// <summary>
        /// Create a new custom field definition.
        /// Scope required: app-foundations.custom-field-definitions
        /// </summary>
        Task<GraphQLResponse<CreateCustomFieldDefinitionData>> CreateCustomFieldDefinitionAsync(CustomFieldDefinitionCreateInput input, string? fields = null);

        /// <summary>
        /// Update an existing custom field definition.
        /// Scope required: app-foundations.custom-field-definitions
        /// </summary>
        Task<GraphQLResponse<UpdateCustomFieldDefinitionData>> UpdateCustomFieldDefinitionAsync(CustomFieldDefinitionUpdateInput input, string? fields = null);
    }
}
