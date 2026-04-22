namespace QuickBooksSharp.GraphQL
{
    public static class GraphQLUrl
    {
        public const string ProductionEndpoint = "https://qb.api.intuit.com/graphql";
        public const string SandboxEndpoint = "https://qb-sandbox.api.intuit.com/graphql";

        public static string GetEndpoint(bool useSandbox) => useSandbox ? SandboxEndpoint : ProductionEndpoint;
    }
}
