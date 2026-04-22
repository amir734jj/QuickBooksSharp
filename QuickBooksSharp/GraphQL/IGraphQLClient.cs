using System.Threading.Tasks;

namespace QuickBooksSharp.GraphQL
{
    public interface IGraphQLClient
    {
        Task<GraphQLResponse<TData>> SendQueryAsync<TData>(string query, object? variables = null, string? operationName = null) where TData : class;

        Task<GraphQLResponse<TData>> SendMutationAsync<TData>(string query, object? variables = null, string? operationName = null) where TData : class;
    }
}
