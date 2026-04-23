using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface ISalesTaxService
    {
        /// <summary>
        /// Calculate sales tax for a transaction.
        /// </summary>
        Task<GraphQLResponse<CalculateSalesTaxData>> CalculateSalesTaxAsync(SalesTaxCalculationInput input, string? customQuery = null);
    }
}
