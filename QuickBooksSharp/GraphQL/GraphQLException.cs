using System;
using System.Linq;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLException(GraphQLError[] errors) : Exception(FormatMessage(errors))
    {
        public GraphQLError[] Errors { get; } = errors;

        private static string FormatMessage(GraphQLError[] errors)
        {
            var messages = errors.Select(e => e.Message);
            return $"GraphQL request failed with {errors.Length} error(s): {string.Join("; ", messages)}";
        }
    }
}
