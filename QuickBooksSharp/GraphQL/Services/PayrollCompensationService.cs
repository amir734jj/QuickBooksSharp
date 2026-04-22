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
        private const string DefaultCompensationFields = @"
            id
            employeeId
            active
            employerCompensation {
                id
                name
                active
                type {
                    value
                }
            }
            rate {
                amount {
                    amount
                    currencyCode
                }
                payUnit
            }";

        private readonly GraphQLClient _client;

        public PayrollCompensationService(string accessToken, long realmId, bool useSandbox, IRunPolicy? runPolicy = null)
        {
            _client = new GraphQLClient(accessToken, realmId, useSandbox, runPolicy);
        }

        public async Task<GraphQLResponse<EmployeeCompensationsQueryData>> GetEmployeeCompensationsAsync(EmployeeCompensationsFilter filter, int? first = null, string? after = null, string? fields = null)
        {
            var query = $@"
                query GetEmployeeCompensations($filter: Payroll_EmployeeCompensationsFilter!, $first: Int, $after: String) {{
                    payrollEmployeeCompensations(filter: $filter, first: $first, after: $after) {{
                        edges {{
                            node {{
                                {fields ?? DefaultCompensationFields}
                            }}
                            cursor
                        }}
                        nodes {{
                            {fields ?? DefaultCompensationFields}
                        }}
                        pageInfo {{
                            hasNextPage
                            hasPreviousPage
                            startCursor
                            endCursor
                        }}
                    }}
                }}";

            return await _client.SendQueryAsync<EmployeeCompensationsQueryData>(query, new { filter, first, after }, "GetEmployeeCompensations");
        }
    }
}
