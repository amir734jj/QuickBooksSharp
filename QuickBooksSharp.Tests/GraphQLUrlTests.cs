using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class GraphQLUrlTests
    {
        [TestMethod]
        public void GetEndpoint_Sandbox()
        {
            var url = GraphQLUrl.GetEndpoint(useSandbox: true);
            Assert.AreEqual("https://qb-sandbox.api.intuit.com/graphql", url);
        }

        [TestMethod]
        public void GetEndpoint_Production()
        {
            var url = GraphQLUrl.GetEndpoint(useSandbox: false);
            Assert.AreEqual("https://qb.api.intuit.com/graphql", url);
        }
    }
}
