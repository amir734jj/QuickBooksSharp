using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class CalculateSalesTaxData
    {
        [JsonProperty("indirectTaxCalculateSaleTransactionTax")]
        public SalesTaxCalculationPayload? Result { get; set; }
    }

    public class SalesTaxCalculationPayload
    {
        [JsonProperty("taxCalculation")]
        public SalesTaxCalculation? TaxCalculation { get; set; }
    }

    public class SalesTaxService(
        string accessToken,
        long realmId,
        bool useSandbox,
        IRunPolicy? runPolicy = null,
        ILogger? logger = null)
        : ISalesTaxService
    {
        private readonly GraphQLClient _client = new(accessToken, realmId, useSandbox, runPolicy, logger);

        public async Task<GraphQLResponse<CalculateSalesTaxData>> CalculateSalesTaxAsync(SalesTaxCalculationInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("CalculateSalesTax");
            return await _client.SendMutationAsync<CalculateSalesTaxData>(query, new { input }, "CalculateSalesTax");
        }
    }
}
