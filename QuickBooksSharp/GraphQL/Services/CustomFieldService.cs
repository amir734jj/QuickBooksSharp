using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class CustomFieldDefinitionsQueryData
    {
        [JsonProperty("appFoundationsCustomFieldDefinitions")]
        public CustomFieldDefinitionsConnection? CustomFieldDefinitions { get; set; }
    }

    public class CreateCustomFieldDefinitionData
    {
        [JsonProperty("appFoundationsCreateCustomFieldDefinition")]
        public CustomFieldDefinition? CustomFieldDefinition { get; set; }
    }

    public class UpdateCustomFieldDefinitionData
    {
        [JsonProperty("appFoundationsUpdateCustomFieldDefinition")]
        public CustomFieldDefinition? CustomFieldDefinition { get; set; }
    }

    public class CustomFieldService(
        string accessToken,
        long realmId,
        bool useSandbox,
        IRunPolicy? runPolicy = null,
        ILogger? logger = null)
        : ICustomFieldService
    {
        private readonly GraphQLClient _client = new(accessToken, realmId, useSandbox, runPolicy, logger);

        public async Task<GraphQLResponse<CustomFieldDefinitionsQueryData>> GetCustomFieldDefinitionsAsync(int? first = null, string? after = null, CustomFieldDefinitionsFilter? filters = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetCustomFieldDefinitions");
            return await _client.SendQueryAsync<CustomFieldDefinitionsQueryData>(query, new { first, after, filters }, "GetCustomFieldDefinitions");
        }

        public async Task<GraphQLResponse<CreateCustomFieldDefinitionData>> CreateCustomFieldDefinitionAsync(CustomFieldDefinitionCreateInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("CreateCustomFieldDefinition");
            return await _client.SendMutationAsync<CreateCustomFieldDefinitionData>(query, new { input }, "CreateCustomFieldDefinition");
        }

        public async Task<GraphQLResponse<UpdateCustomFieldDefinitionData>> UpdateCustomFieldDefinitionAsync(CustomFieldDefinitionUpdateInput input, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("UpdateCustomFieldDefinition");
            return await _client.SendMutationAsync<UpdateCustomFieldDefinitionData>(query, new { input }, "UpdateCustomFieldDefinition");
        }
    }
}
