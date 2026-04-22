using System;
using System.Linq;

namespace QuickBooksSharp.GraphQL
{
    public class GraphQLException : Exception
    {
        public GraphQLError[] Errors { get; }

        public GraphQLException(GraphQLError[] errors)
            : base(FormatMessage(errors))
        {
            Errors = errors;
        }

        private static string FormatMessage(GraphQLError[] errors)
        {
            var messages = errors.Select(e => e.Message);
            return $"GraphQL request failed with {errors.Length} error(s): {string.Join("; ", messages)}";
        }
    }
}
