using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface ISalesTaxService
    {
        /// <summary>
        /// Calculate sales tax for a transaction based on the input information.
        /// Scope required: indirect-tax.tax-calculation.quickbooks
        /// </summary>
        Task<GraphQLResponse<CalculateSalesTaxData>> CalculateSalesTaxAsync(SalesTaxCalculationInput input, string? fields = null);
    }
}
