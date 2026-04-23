using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class GraphQLRequestTests
    {
        
        [TestMethod]
        public void Serialize_QueryOnly()
        {
            var request = new GraphQLRequest
            {
                Query = "{ company { id } }"
            };

            var json = JsonConvert.SerializeObject(request, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"query\":\"{ company { id } }\""));
            Assert.IsFalse(json.Contains("\"operationName\""));
            Assert.IsFalse(json.Contains("\"variables\""));
        }

        [TestMethod]
        public void Serialize_WithOperationNameAndVariables()
        {
            var request = new GraphQLRequest
            {
                Query = "query GetProject($id: ID!) { projectManagementProject(id: $id) { id name } }",
                OperationName = "GetProject",
                Variables = new { id = "123" }
            };

            var json = JsonConvert.SerializeObject(request, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"operationName\":\"GetProject\""));
            Assert.IsTrue(json.Contains("\"variables\""));
            Assert.IsTrue(json.Contains("\"id\":\"123\""));
        }

        [TestMethod]
        public void Serialize_NullOperationNameOmitted()
        {
            var request = new GraphQLRequest
            {
                Query = "{ test }",
                OperationName = null,
                Variables = null
            };

            var json = JsonConvert.SerializeObject(request, GraphQLClient.JsonSettings);

            Assert.IsFalse(json.Contains("operationName"));
            Assert.IsFalse(json.Contains("variables"));
        }
    }
}
