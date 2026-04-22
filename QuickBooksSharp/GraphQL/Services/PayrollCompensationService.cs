using System.Text.Json.Serialization;
using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.Policies;

namespace QuickBooksSharp.GraphQL.Services
{
    public class EmployeeCompensationsQueryData
    {
        [JsonPropertyName("payrollEmployeeCompensations")]
        public EmployeeCompensationConnection? EmployeeCompensations { get; set; }
    }

    public class PayrollCompensationService : IPayrollCompensationService
    {
        private readonly GraphQLClient _client;

        public PayrollCompensationService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<EmployeeCompensationsQueryData>> GetEmployeeCompensationsAsync(EmployeeCompensationsFilter filter, int? first = null, string? after = null, string? customQuery = null)
        {
            var query = customQuery ?? GraphQLQueryLoader.Load("GetEmployeeCompensations");
            return await _client.SendQueryAsync<EmployeeCompensationsQueryData>(query, new { filter, first, after }, "GetEmployeeCompensations");
        }
    }
}
