using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class DimensionDefinitionsQueryData
    {
        [JsonPropertyName("appFoundationsActiveCustomDimensionDefinitions")]
        public DimensionDefinitionsConnection? DimensionDefinitions { get; set; }
    }

    public class DimensionValuesQueryData
    {
        [JsonPropertyName("appFoundationsActiveCustomDimensionValues")]
        public DimensionValuesConnection? DimensionValues { get; set; }
    }

    public class CreateDimensionValueData
    {
        [JsonPropertyName("appFoundationsCommonCreateCustomDimensionValue")]
        public DimensionValue? DimensionValue { get; set; }
    }

    public class UpdateDimensionValueData
    {
        [JsonPropertyName("appFoundationsCommonUpdateCustomDimensionValue")]
        public DimensionValue? DimensionValue { get; set; }
    }

    public class DisableDimensionValueData
    {
        [JsonPropertyName("appFoundationsCommonDisableCustomDimensionValue")]
        public DimensionValue? DimensionValue { get; set; }
    }

    public class DimensionService : IDimensionService
    {
        private readonly GraphQLClient _client;

        public DimensionService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<DimensionDefinitionsQueryData>> GetDimensionDefinitionsAsync(int? first = null, string? after = null, DimensionDefinitionsFilter? filters = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetDimensionDefinitions");
            return await _client.SendQueryAsync<DimensionDefinitionsQueryData>(query, new { first, after, filters }, "GetDimensionDefinitions");
        }

        public async Task<GraphQLResponse<DimensionValuesQueryData>> GetDimensionValuesAsync(DimensionValuesFilter filters, int? first = null, string? after = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetDimensionValues");
            return await _client.SendQueryAsync<DimensionValuesQueryData>(query, new { filters, first, after }, "GetDimensionValues");
        }

        public async Task<GraphQLResponse<CreateDimensionValueData>> CreateDimensionValueAsync(DimensionValueCreateInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("CreateDimensionValue");
            return await _client.SendMutationAsync<CreateDimensionValueData>(query, new { input }, "CreateDimensionValue");
        }

        public async Task<GraphQLResponse<UpdateDimensionValueData>> UpdateDimensionValueAsync(DimensionValueUpdateInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("UpdateDimensionValue");
            return await _client.SendMutationAsync<UpdateDimensionValueData>(query, new { input }, "UpdateDimensionValue");
        }

        public async Task<GraphQLResponse<DisableDimensionValueData>> DisableDimensionValueAsync(DimensionValueDisableInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("DisableDimensionValue");
            return await _client.SendMutationAsync<DisableDimensionValueData>(query, new { input }, "DisableDimensionValue");
        }
    }
}
