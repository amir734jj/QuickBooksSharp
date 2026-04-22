using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class CustomFieldDefinitionsQueryData
    {
        [JsonPropertyName("appFoundationsCustomFieldDefinitions")]
        public CustomFieldDefinitionsConnection? CustomFieldDefinitions { get; set; }
    }

    public class CreateCustomFieldDefinitionData
    {
        [JsonPropertyName("appFoundationsCreateCustomFieldDefinition")]
        public CustomFieldDefinition? CustomFieldDefinition { get; set; }
    }

    public class UpdateCustomFieldDefinitionData
    {
        [JsonPropertyName("appFoundationsUpdateCustomFieldDefinition")]
        public CustomFieldDefinition? CustomFieldDefinition { get; set; }
    }

    public class CustomFieldService : ICustomFieldService
    {
        private const string DefaultCustomFieldFields = @"
            id
            legacyID
            legacyIDV2
            label
            dataType
            active
            required
            entityVersion
            associations {
                entityType
                allowedOperations
                condition
            }
            dropDownOptions {
                id
                value
                active
            }";

        private readonly GraphQLClient _client;

        public CustomFieldService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<CustomFieldDefinitionsQueryData>> GetCustomFieldDefinitionsAsync(int? first = null, string? after = null, CustomFieldDefinitionsFilter? filters = null, string? fields = null)
        {
            var query = $@"
                query GetCustomFieldDefinitions($first: Int, $after: String, $filters: AppFoundations_CustomExtensionsDefinitionFilterBy) {{
                    appFoundationsCustomFieldDefinitions(first: $first, after: $after, filters: $filters) {{
                        edges {{
                            node {{
                                {fields ?? DefaultCustomFieldFields}
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

            return await _client.SendQueryAsync<CustomFieldDefinitionsQueryData>(query, new { first, after, filters }, "GetCustomFieldDefinitions");
        }

        public async Task<GraphQLResponse<CreateCustomFieldDefinitionData>> CreateCustomFieldDefinitionAsync(CustomFieldDefinitionCreateInput input, string? fields = null)
        {
            var query = $@"
                mutation CreateCustomFieldDefinition($input: AppFoundations_CustomFieldDefinitionCreateInput!) {{
                    appFoundationsCreateCustomFieldDefinition(input: $input) {{
                        {fields ?? DefaultCustomFieldFields}
                    }}
                }}";

            return await _client.SendMutationAsync<CreateCustomFieldDefinitionData>(query, new { input }, "CreateCustomFieldDefinition");
        }

        public async Task<GraphQLResponse<UpdateCustomFieldDefinitionData>> UpdateCustomFieldDefinitionAsync(CustomFieldDefinitionUpdateInput input, string? fields = null)
        {
            var query = $@"
                mutation UpdateCustomFieldDefinition($input: AppFoundations_CustomFieldDefinitionUpdateInput!) {{
                    appFoundationsUpdateCustomFieldDefinition(input: $input) {{
                        {fields ?? DefaultCustomFieldFields}
                    }}
                }}";

            return await _client.SendMutationAsync<UpdateCustomFieldDefinitionData>(query, new { input }, "UpdateCustomFieldDefinition");
        }
    }
}
