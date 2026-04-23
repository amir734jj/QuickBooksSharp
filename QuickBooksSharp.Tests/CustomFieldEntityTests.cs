using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class CustomFieldEntityTests
    {
        
        [TestMethod]
        public void Deserialize_CustomFieldDefinition()
        {
            var json = @"{
                ""id"": ""cf-1"",
                ""legacyID"": ""legacy-1"",
                ""legacyIDV2"": ""540344"",
                ""label"": ""Priority Level"",
                ""dataType"": ""STRING"",
                ""active"": true,
                ""required"": false,
                ""entityVersion"": 2,
                ""associations"": [
                    {
                        ""entityType"": ""CUSTOMER"",
                        ""allowedOperations"": [""READ"", ""WRITE""],
                        ""condition"": ""OPTIONAL""
                    }
                ],
                ""dropDownOptions"": null
            }";

            var def = JsonConvert.DeserializeObject<CustomFieldDefinition>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(def);
            Assert.AreEqual("cf-1", def.Id);
            Assert.AreEqual("540344", def.LegacyIDV2);
            Assert.AreEqual("Priority Level", def.Label);
            Assert.AreEqual(CustomFieldDataType.STRING, def.DataType);
            Assert.AreEqual(true, def.Active);
            Assert.AreEqual(false, def.Required);
            Assert.AreEqual(2, def.EntityVersion);
            Assert.IsNotNull(def.Associations);
            Assert.AreEqual(1, def.Associations.Length);
            Assert.AreEqual("CUSTOMER", def.Associations[0].EntityType);
            Assert.AreEqual(CustomFieldAllowedOperation.READ, def.Associations[0].AllowedOperations![0]);
            Assert.AreEqual(CustomFieldAllowedOperation.WRITE, def.Associations[0].AllowedOperations![1]);
            Assert.AreEqual(CustomFieldAssociationCondition.OPTIONAL, def.Associations[0].Condition);
        }

        [TestMethod]
        public void Serialize_CustomFieldDataType_AllValues()
        {
            Assert.AreEqual("\"STRING\"", JsonConvert.SerializeObject(CustomFieldDataType.STRING, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"NUMBER\"", JsonConvert.SerializeObject(CustomFieldDataType.NUMBER, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"DATE\"", JsonConvert.SerializeObject(CustomFieldDataType.DATE, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"DROPDOWN\"", JsonConvert.SerializeObject(CustomFieldDataType.DROPDOWN, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"BOOLEAN\"", JsonConvert.SerializeObject(CustomFieldDataType.BOOLEAN, GraphQLClient.JsonSettings));
        }

        [TestMethod]
        public void Serialize_CustomFieldDefinitionCreateInput()
        {
            var input = new CustomFieldDefinitionCreateInput
            {
                Label = "Region",
                DataType = CustomFieldDataType.DROPDOWN,
                Associations = new[]
                {
                    new CustomFieldAssociationInput
                    {
                        EntityType = "INVOICE",
                        AllowedOperations = new[] { CustomFieldAllowedOperation.READ, CustomFieldAllowedOperation.WRITE }
                    }
                },
                DropDownOptions = new[]
                {
                    new CustomFieldDropDownOptionInput { Value = "East" },
                    new CustomFieldDropDownOptionInput { Value = "West" }
                }
            };

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"label\":\"Region\""));
            Assert.IsTrue(json.Contains("\"dataType\":\"DROPDOWN\""));
            Assert.IsTrue(json.Contains("\"entityType\":\"INVOICE\""));
            Assert.IsTrue(json.Contains("\"value\":\"East\""));
            Assert.IsTrue(json.Contains("\"value\":\"West\""));
        }

        [TestMethod]
        public void Serialize_CustomFieldDefinitionUpdateInput_NullFieldsOmitted()
        {
            var input = new CustomFieldDefinitionUpdateInput
            {
                Id = "cf-1",
                Active = false
            };

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"id\":\"cf-1\""));
            Assert.IsTrue(json.Contains("\"active\":false"));
            Assert.IsFalse(json.Contains("\"label\""));
            Assert.IsFalse(json.Contains("\"dataType\""));
            Assert.IsFalse(json.Contains("\"associations\""));
        }

        [TestMethod]
        public void Deserialize_CustomFieldDefinitionsConnection()
        {
            var json = @"{
                ""edges"": [
                    {
                        ""node"": { ""id"": ""cf-1"", ""label"": ""Field A"", ""dataType"": ""STRING"", ""legacyID"": ""l1"", ""legacyIDV2"": ""l2"", ""active"": true, ""associations"": [] },
                        ""cursor"": ""c1""
                    }
                ],
                ""pageInfo"": {
                    ""hasNextPage"": false,
                    ""hasPreviousPage"": false
                }
            }";

            var conn = JsonConvert.DeserializeObject<CustomFieldDefinitionsConnection>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(conn);
            Assert.AreEqual(1, conn.Edges!.Length);
            Assert.AreEqual("cf-1", conn.Edges[0].Node!.Id);
            Assert.AreEqual("Field A", conn.Edges[0].Node.Label);
            Assert.IsFalse(conn.PageInfo!.HasNextPage);
        }

        [TestMethod]
        public void Deserialize_FullCustomFieldGraphQLResponse()
        {
            var json = @"{
                ""data"": {
                    ""appFoundationsCustomFieldDefinitions"": {
                        ""edges"": [
                            {
                                ""node"": { ""id"": ""cf-1"", ""label"": ""Custom 1"", ""dataType"": ""NUMBER"", ""legacyID"": ""l1"", ""legacyIDV2"": ""l2"", ""active"": true, ""associations"": [] },
                                ""cursor"": ""a""
                            }
                        ],
                        ""pageInfo"": { ""hasNextPage"": true, ""hasPreviousPage"": false, ""endCursor"": ""a"" }
                    }
                }
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<CustomFieldDefinitionsQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.CustomFieldDefinitions);
            Assert.AreEqual(1, response.Data.CustomFieldDefinitions.Edges!.Length);
            Assert.AreEqual(CustomFieldDataType.NUMBER, response.Data.CustomFieldDefinitions.Edges[0].Node!.DataType);
            Assert.IsTrue(response.Data.CustomFieldDefinitions.PageInfo!.HasNextPage);
        }
    }
}
