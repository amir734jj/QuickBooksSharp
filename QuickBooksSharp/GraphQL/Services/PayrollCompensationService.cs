using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class EmployeeCompensationsQueryData
    {
        [JsonProperty("payrollEmployeeCompensations")]
        public EmployeeCompensationConnection? EmployeeCompensations { get; set; }
    }

    public class PayrollCompensationService : IPayrollCompensationService
    {
        private readonly GraphQLClient _client;

        public PayrollCompensationService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null, ILogger? logger = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy, logger);
        }

        public async Task<GraphQLResponse<EmployeeCompensationsQueryData>> GetEmployeeCompensationsAsync(EmployeeCompensationsFilter filter, int? first = null, string? after = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetEmployeeCompensations");
            return await _client.SendQueryAsync<EmployeeCompensationsQueryData>(query, new { filter, first, after }, "GetEmployeeCompensations");
        }
    }
}
