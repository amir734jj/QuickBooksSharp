using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;

namespace QuickBooksSharp.GraphQL
{
    internal static class GraphQLQueryLoader
    {
        private static readonly Assembly _assembly = typeof(GraphQLQueryLoader).Assembly;
        private static readonly ConcurrentDictionary<string, string> _cache = new ConcurrentDictionary<string, string>();        internal static string Load(string queryName)
        {
            return _cache.GetOrAdd(queryName, name =>
            {
                var resourceName = $"QuickBooksSharp.GraphQL.Queries.{name}.graphql";
                using (var stream = _assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");

                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            });
        }
    }
}
