using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class CalculateSalesTaxData
    {
        [JsonPropertyName("indirectTaxCalculateSaleTransactionTax")]
        public SalesTaxCalculationPayload? Result { get; set; }
    }

    public class SalesTaxCalculationPayload
    {
        [JsonPropertyName("taxCalculation")]
        public SalesTaxCalculation? TaxCalculation { get; set; }
    }

    public class SalesTaxService : ISalesTaxService
    {
        private readonly GraphQLClient _client;

        public SalesTaxService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<CalculateSalesTaxData>> CalculateSalesTaxAsync(SalesTaxCalculationInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("CalculateSalesTax");
            return await _client.SendMutationAsync<CalculateSalesTaxData>(query, new { input }, "CalculateSalesTax");
        }
    }
}
