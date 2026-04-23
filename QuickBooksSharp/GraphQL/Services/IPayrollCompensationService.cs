using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface IPayrollCompensationService
    {
        /// <summary>
        /// Get compensations for a given employee.
        /// </summary>
        Task<GraphQLResponse<EmployeeCompensationsQueryData>> GetEmployeeCompensationsAsync(EmployeeCompensationsFilter filter, int? first = null, string? after = null, string? customQuery = null);
    }
}
