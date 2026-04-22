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
        private const string DefaultDefinitionFields = @"
            id
            label
            dataType
            active
            required
            associations {
                entityType
                allowedOperations
                condition
            }
            sharedInfo {
                name
                description
            }";

        private const string DefaultValueFields = @"
            id
            value
            active
            entityVersion";

        private readonly GraphQLClient _client;

        public DimensionService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<DimensionDefinitionsQueryData>> GetDimensionDefinitionsAsync(int? first = null, string? after = null, DimensionDefinitionsFilter? filters = null, string? fields = null)
        {
            var query = $@"
                query GetDimensionDefinitions($first: Int, $after: String, $filters: AppFoundations_ActiveCustomDimensionDefinitionFilterBy) {{
                    appFoundationsActiveCustomDimensionDefinitions(first: $first, after: $after, filters: $filters) {{
                        edges {{
                            node {{
                                {fields ?? DefaultDefinitionFields}
                            }}
                            cursor
                        }}
                        pageInfo {{
                            hasNextPage
                            hasPreviousPage
                            startCursor
                            endCursor
                        }}
                    }}
                }}";

            return await _client.SendQueryAsync<DimensionDefinitionsQueryData>(query, new { first, after, filters }, "GetDimensionDefinitions");
        }

        public async Task<GraphQLResponse<DimensionValuesQueryData>> GetDimensionValuesAsync(DimensionValuesFilter filters, int? first = null, string? after = null, string? fields = null)
        {
            var query = $@"
                query GetDimensionValues($filters: AppFoundations_ActiveCustomDimensionValuesFilterBy!, $first: Int, $after: String) {{
                    appFoundationsActiveCustomDimensionValues(filters: $filters, first: $first, after: $after) {{
                        edges {{
                            node {{
                                {fields ?? DefaultValueFields}
                            }}
                            cursor
                        }}
                        pageInfo {{
                            hasNextPage
                            hasPreviousPage
                            startCursor
                            endCursor
                        }}
                    }}
                }}";

            return await _client.SendQueryAsync<DimensionValuesQueryData>(query, new { filters, first, after }, "GetDimensionValues");
        }

        public async Task<GraphQLResponse<CreateDimensionValueData>> CreateDimensionValueAsync(DimensionValueCreateInput input, string? fields = null)
        {
            var query = $@"
                mutation CreateDimensionValue($input: AppFoundations_CustomDimensionValueCreateInput!) {{
                    appFoundationsCommonCreateCustomDimensionValue(input: $input) {{
                        {fields ?? DefaultValueFields}
                    }}
                }}";

            return await _client.SendMutationAsync<CreateDimensionValueData>(query, new { input }, "CreateDimensionValue");
        }

        public async Task<GraphQLResponse<UpdateDimensionValueData>> UpdateDimensionValueAsync(DimensionValueUpdateInput input, string? fields = null)
        {
            var query = $@"
                mutation UpdateDimensionValue($input: AppFoundations_CustomDimensionValueUpdateInput!) {{
                    appFoundationsCommonUpdateCustomDimensionValue(input: $input) {{
                        {fields ?? DefaultValueFields}
                    }}
                }}";

            return await _client.SendMutationAsync<UpdateDimensionValueData>(query, new { input }, "UpdateDimensionValue");
        }

        public async Task<GraphQLResponse<DisableDimensionValueData>> DisableDimensionValueAsync(DimensionValueDisableInput input, string? fields = null)
        {
            var query = $@"
                mutation DisableDimensionValue($input: AppFoundations_CustomDimensionValueDisableInput!) {{
                    appFoundationsCommonDisableCustomDimensionValue(input: $input) {{
                        {fields ?? DefaultValueFields}
                    }}
                }}";

            return await _client.SendMutationAsync<DisableDimensionValueData>(query, new { input }, "DisableDimensionValue");
        }
    }
}
