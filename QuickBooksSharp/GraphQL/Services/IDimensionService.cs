using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface IDimensionService
    {
        /// <summary>
        /// Query all active custom dimension definitions.
        /// Scope required: app-foundations.custom-dimensions.read
        /// </summary>
        Task<GraphQLResponse<DimensionDefinitionsQueryData>> GetDimensionDefinitionsAsync(int? first = null, string? after = null, DimensionDefinitionsFilter? filters = null, string? fields = null);

        /// <summary>
        /// Query all active custom dimension values within a definition.
        /// Scope required: app-foundations.custom-dimensions.read
        /// </summary>
        Task<GraphQLResponse<DimensionValuesQueryData>> GetDimensionValuesAsync(DimensionValuesFilter filters, int? first = null, string? after = null, string? fields = null);

        /// <summary>
        /// Create a custom dimension value.
        /// Scope required: app-foundations.custom-dimensions.read (write access via separate scope if available)
        /// </summary>
        Task<GraphQLResponse<CreateDimensionValueData>> CreateDimensionValueAsync(DimensionValueCreateInput input, string? fields = null);

        /// <summary>
        /// Update a custom dimension value.
        /// </summary>
        Task<GraphQLResponse<UpdateDimensionValueData>> UpdateDimensionValueAsync(DimensionValueUpdateInput input, string? fields = null);

        /// <summary>
        /// Disable a custom dimension value.
        /// </summary>
        Task<GraphQLResponse<DisableDimensionValueData>> DisableDimensionValueAsync(DimensionValueDisableInput input, string? fields = null);
    }
}
