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

    public class PayrollCompensationService(
        string accessToken,
        long realmId,
        bool useSandbox,
        IRunPolicy? runPolicy = null,
        ILogger? logger = null)
        : IPayrollCompensationService
    {
        private readonly GraphQLClient _client = new(accessToken, realmId, useSandbox, runPolicy, logger);

        public async Task<GraphQLResponse<EmployeeCompensationsQueryData>> GetEmployeeCompensationsAsync(EmployeeCompensationsFilter filter, int? first = null, string? after = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetEmployeeCompensations");
            return await _client.SendQueryAsync<EmployeeCompensationsQueryData>(query, new { filter, first, after }, "GetEmployeeCompensations");
        }
    }
}
