using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;
using QuickBooksSharp.Infrastructure;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class DimensionEntityTests
    {
        private static readonly JsonSerializerOptions _options = QuickBooksHttpClient.JsonSerializerOptions;

        [TestMethod]
        public void Deserialize_DimensionDefinition()
        {
            var json = @"{
                ""id"": ""dim-1"",
                ""label"": ""Business Unit"",
                ""dataType"": ""STRING"",
                ""active"": true,
                ""required"": false,
                ""associations"": [
                    {
                        ""entityType"": ""INVOICE"",
                        ""allowedOperations"": [""READ""]
                    }
                ],
                ""sharedInfo"": {
                    ""name"": ""Business Unit"",
                    ""description"": ""Business unit dimension""
                }
            }";

            var def = JsonSerializer.Deserialize<DimensionDefinition>(json, _options);

            Assert.IsNotNull(def);
            Assert.AreEqual("dim-1", def.Id);
            Assert.AreEqual("Business Unit", def.Label);
            Assert.AreEqual(CustomFieldDataType.STRING, def.DataType);
            Assert.AreEqual(true, def.Active);
            Assert.IsNotNull(def.Associations);
            Assert.AreEqual(1, def.Associations.Length);
            Assert.AreEqual("INVOICE", def.Associations[0].EntityType);
            Assert.IsNotNull(def.SharedInfo);
            Assert.AreEqual("Business Unit", def.SharedInfo.Name);
            Assert.AreEqual("Business unit dimension", def.SharedInfo.Description);
        }

        [TestMethod]
        public void Deserialize_DimensionValue()
        {
            var json = @"{
                ""id"": ""dv-1"",
                ""value"": ""Marketing"",
                ""active"": true,
                ""entityVersion"": 3
            }";

            var val = JsonSerializer.Deserialize<DimensionValue>(json, _options);

            Assert.IsNotNull(val);
            Assert.AreEqual("dv-1", val.Id);
            Assert.AreEqual("Marketing", val.Value);
            Assert.AreEqual(true, val.Active);
            Assert.AreEqual(3, val.EntityVersion);
        }

        [TestMethod]
        public void Serialize_DimensionValueCreateInput()
        {
            var input = new DimensionValueCreateInput
            {
                DimensionDefinitionId = "dim-1",
                Value = "Sales"
            };

            var json = JsonSerializer.Serialize(input, _options);

            Assert.IsTrue(json.Contains("\"dimensionDefinitionId\":\"dim-1\""));
            Assert.IsTrue(json.Contains("\"value\":\"Sales\""));
        }

        [TestMethod]
        public void Serialize_DimensionValuesFilter()
        {
            var filter = new DimensionValuesFilter
            {
                DimensionDefinitionId = "dim-1"
            };

            var json = JsonSerializer.Serialize(filter, _options);

            Assert.IsTrue(json.Contains("\"dimensionDefinitionId\":\"dim-1\""));
        }

        [TestMethod]
        public void Deserialize_DimensionDefinitionsConnection()
        {
            var json = @"{
                ""edges"": [
                    {
                        ""node"": { ""id"": ""dim-1"", ""label"": ""Dept"", ""dataType"": ""STRING"", ""active"": true, ""associations"": [] },
                        ""cursor"": ""c1""
                    }
                ],
                ""pageInfo"": { ""hasNextPage"": false, ""hasPreviousPage"": false }
            }";

            var conn = JsonSerializer.Deserialize<DimensionDefinitionsConnection>(json, _options);

            Assert.IsNotNull(conn);
            Assert.AreEqual(1, conn.Edges!.Length);
            Assert.AreEqual("dim-1", conn.Edges[0].Node!.Id);
            Assert.AreEqual("Dept", conn.Edges[0].Node.Label);
        }

        [TestMethod]
        public void Deserialize_FullDimensionDefinitionsGraphQLResponse()
        {
            var json = @"{
                ""data"": {
                    ""appFoundationsActiveCustomDimensionDefinitions"": {
                        ""edges"": [
                            {
                                ""node"": { ""id"": ""dim-1"", ""label"": ""Location"", ""dataType"": ""STRING"", ""active"": true, ""associations"": [] },
                                ""cursor"": ""c1""
                            }
                        ],
                        ""pageInfo"": { ""hasNextPage"": false, ""hasPreviousPage"": false }
                    }
                }
            }";

            var response = JsonSerializer.Deserialize<GraphQLResponse<DimensionDefinitionsQueryData>>(json, _options);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.DimensionDefinitions);
            Assert.AreEqual(1, response.Data.DimensionDefinitions.Edges!.Length);
            Assert.AreEqual("Location", response.Data.DimensionDefinitions.Edges[0].Node!.Label);
        }
    }
}
