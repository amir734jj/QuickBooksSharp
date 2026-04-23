using System.Threading.Tasks;
using QuickBooksSharp.GraphQL.Entities;

namespace QuickBooksSharp.GraphQL.Services
{
    public interface IPayrollCompensationService
    {
        /// <summary>
        /// Get compensations such as Overtime, Vacation Pay, etc. for a given employee.        Task<GraphQLResponse<EmployeeCompensationsQueryData>> GetEmployeeCompensationsAsync(EmployeeCompensationsFilter filter, int? first = null, string? after = null, string? customQuery = null);
    }
}
