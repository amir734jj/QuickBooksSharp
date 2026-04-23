using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public static class TestSetup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            // Force GraphQLClient static constructor to run,
            // which sets JsonConvert.DefaultSettings globally.
            _ = GraphQLClient.JsonSettings;
        }
    }
}
