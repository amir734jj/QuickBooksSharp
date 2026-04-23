using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Services;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class GraphQLResponseTests
    {
        
        [TestMethod]
        public void Deserialize_SuccessResponse()
        {
            var json = @"{
                ""data"": { ""projectManagementProject"": { ""id"": ""123"", ""name"": ""Test"" } },
                ""extensions"": { ""requestId"": ""req-1"" }
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<ProjectQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNull(response.Errors);
            Assert.IsNotNull(response.Data);
            Assert.IsNotNull(response.Data.Project);
            Assert.AreEqual("123", response.Data.Project.Id);
            Assert.AreEqual("Test", response.Data.Project.Name);
            Assert.IsNotNull(response.Extensions);
            Assert.AreEqual("req-1", response.Extensions.RequestId);
        }

        [TestMethod]
        public void Deserialize_ErrorResponse()
        {
            var json = @"{
                ""data"": null,
                ""errors"": [
                    {
                        ""message"": ""Unauthorized"",
                        ""locations"": [{ ""line"": 1, ""column"": 2 }],
                        ""extensions"": { ""code"": ""UNAUTHENTICATED"", ""classification"": ""AuthenticationError"" }
                    }
                ]
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<ProjectQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.AreEqual(1, response.Errors!.Length);
            Assert.AreEqual("Unauthorized", response.Errors[0].Message);
            Assert.AreEqual("UNAUTHENTICATED", response.Errors[0].Extensions?.Code);
            Assert.AreEqual("AuthenticationError", response.Errors[0].Extensions?.Classification);
            Assert.AreEqual(1, response.Errors[0].Locations![0].Line);
            Assert.AreEqual(2, response.Errors[0].Locations![0].Column);
        }

        [TestMethod]
        public void Deserialize_PartialResponse_WithDataAndErrors()
        {
            var json = @"{
                ""data"": { ""projectManagementProject"": { ""id"": ""123"", ""name"": ""Test"" } },
                ""errors"": [{ ""message"": ""Partial error on optional field"" }]
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<ProjectQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasErrors);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual("123", response.Data.Project?.Id);
        }

        [TestMethod]
        public void HasErrors_FalseWhenErrorsNull()
        {
            var response = new GraphQLResponse<ProjectQueryData> { Errors = null };
            Assert.IsFalse(response.HasErrors);
        }

        [TestMethod]
        public void HasErrors_FalseWhenErrorsEmpty()
        {
            var response = new GraphQLResponse<ProjectQueryData> { Errors = [] };
            Assert.IsFalse(response.HasErrors);
        }

        [TestMethod]
        public void HasErrors_TrueWhenErrorsPresent()
        {
            var response = new GraphQLResponse<ProjectQueryData>
            {
                Errors = [new GraphQLError { Message = "Err" }]
            };
            Assert.IsTrue(response.HasErrors);
        }

        [TestMethod]
        public void Deserialize_ErrorWithPath()
        {
            var json = @"{
                ""data"": null,
                ""errors"": [{
                    ""message"": ""Field error"",
                    ""path"": [""company"", ""transactions"", 0, ""id""]
                }]
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<ProjectQueryData>>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Errors![0].Path);
            Assert.AreEqual(4, response.Errors[0].Path!.Length);
        }
    }
}
