using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class PayrollEntityTests
    {
        
        [TestMethod]
        public void Serialize_PayrollPayUnit_AllValues()
        {
            Assert.AreEqual("\"HOURLY\"", JsonConvert.SerializeObject(PayrollPayUnit.HOURLY, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"WEEKLY\"", JsonConvert.SerializeObject(PayrollPayUnit.WEEKLY, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"BIWEEKLY\"", JsonConvert.SerializeObject(PayrollPayUnit.BIWEEKLY, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"SEMIMONTHLY\"", JsonConvert.SerializeObject(PayrollPayUnit.SEMIMONTHLY, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"MONTHLY\"", JsonConvert.SerializeObject(PayrollPayUnit.MONTHLY, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"ANNUALLY\"", JsonConvert.SerializeObject(PayrollPayUnit.ANNUALLY, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"FLAT\"", JsonConvert.SerializeObject(PayrollPayUnit.FLAT, GraphQLClient.JsonSettings));
        }

        [TestMethod]
        public void Deserialize_EmployeeCompensation()
        {
            var json = @"{
                ""id"": ""comp-1"",
                ""employeeId"": ""emp-123"",
                ""active"": true,
                ""employerCompensation"": {
                    ""id"": ""ec-1"",
                    ""name"": ""Overtime"",
                    ""active"": true,
                    ""type"": { ""value"": ""OVERTIME"" }
                },
                ""rate"": {
                    ""amount"": {
                        ""amount"": 45.00,
                        ""currencyCode"": ""USD""
                    },
                    ""payUnit"": ""HOURLY""
                }
            }";

            var comp = JsonConvert.DeserializeObject<EmployeeCompensation>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(comp);
            Assert.AreEqual("comp-1", comp.Id);
            Assert.AreEqual("emp-123", comp.EmployeeId);
            Assert.IsTrue(comp.Active);

            Assert.IsNotNull(comp.EmployerCompensation);
            Assert.AreEqual("ec-1", comp.EmployerCompensation.Id);
            Assert.AreEqual("Overtime", comp.EmployerCompensation.Name);
            Assert.IsTrue(comp.EmployerCompensation.Active);
            Assert.AreEqual("OVERTIME", comp.EmployerCompensation.Type?.Value);

            Assert.IsNotNull(comp.Rate);
            Assert.AreEqual(45.00m, comp.Rate.Amount?.Amount);
            Assert.AreEqual("USD", comp.Rate.Amount?.CurrencyCode);
            Assert.AreEqual(PayrollPayUnit.HOURLY, comp.Rate.PayUnit);
        }

        [TestMethod]
        public void Serialize_EmployeeCompensationsFilter()
        {
            var filter = new EmployeeCompensationsFilter
            {
                EmployeeId = "emp-123",
                Active = true
            };

            var json = JsonConvert.SerializeObject(filter, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"employeeId\":\"emp-123\""));
            Assert.IsTrue(json.Contains("\"active\":true"));
            Assert.IsFalse(json.Contains("\"intuitAccountId\""));
        }

        [TestMethod]
        public void Serialize_EmployeeCompensationsFilter_NullFieldsOmitted()
        {
            var filter = new EmployeeCompensationsFilter
            {
                EmployeeId = "emp-1"
            };

            var json = JsonConvert.SerializeObject(filter, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"employeeId\":\"emp-1\""));
            Assert.IsFalse(json.Contains("\"active\""));
            Assert.IsFalse(json.Contains("\"intuitAccountId\""));
        }

        [TestMethod]
        public void Deserialize_EmployeeCompensationConnection()
        {
            var json = @"{
                ""edges"": [
                    {
                        ""node"": {
                            ""id"": ""comp-1"",
                            ""employeeId"": ""emp-1"",
                            ""active"": true,
                            ""employerCompensation"": {
                                ""id"": ""ec-1"",
                                ""name"": ""Salary"",
                                ""active"": true
                            }
                        },
                        ""cursor"": ""c1""
                    }
                ],
                ""pageInfo"": {
                    ""hasNextPage"": false,
                    ""hasPreviousPage"": false,
                    ""startCursor"": ""c1"",
                    ""endCursor"": ""c1""
                }
            }";

            var conn = JsonConvert.DeserializeObject<EmployeeCompensationConnection>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(conn);
            Assert.AreEqual(1, conn.Edges!.Length);
            Assert.AreEqual("comp-1", conn.Edges[0].Node!.Id);
            Assert.AreEqual("Salary", conn.Edges[0].Node.EmployerCompensation?.Name);
            Assert.IsFalse(conn.PageInfo!.HasNextPage);
        }

        [TestMethod]
        public void Deserialize_FullPayrollGraphQLResponse()
        {
            var json = @"{
                ""data"": {
                    ""payrollEmployeeCompensations"": {
                        ""edges"": [
                            {
                                ""node"": {
                                    ""id"": ""comp-1"",
                                    ""employeeId"": ""emp-1"",
                                    ""active"": true,
                                    ""employerCompensation"": { ""id"": ""ec-1"", ""name"": ""Vacation Pay"", ""active"": true },
                                    ""rate"": { ""amount"": { ""amount"": 30.00, ""currencyCode"": ""USD"" }, ""payUnit"": ""HOURLY"" }
                                },
                                ""cursor"": ""c1""
                            }
                        ],
                        ""nodes"": [
                            {
                                ""id"": ""comp-1"",
                                ""employeeId"": ""emp-1"",
                                ""active"": true,
                                ""employerCompensation"": { ""id"": ""ec-1"", ""name"": ""Vacation Pay"", ""active"": true },
                                ""rate"": { ""amount"": { ""amount"": 30.00, ""currencyCode"": ""USD"" }, ""payUnit"": ""HOURLY"" }
                            }
                        ],
                        ""pageInfo"": { ""hasNextPage"": false, ""hasPreviousPage"": false }
                    }
                }
            }";

            var response = JsonConvert.DeserializeObject<GraphQLResponse<EmployeeCompensationsQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.EmployeeCompensations);
            Assert.AreEqual(1, response.Data.EmployeeCompensations.Edges!.Length);
            Assert.AreEqual("Vacation Pay", response.Data.EmployeeCompensations.Edges[0].Node!.EmployerCompensation?.Name);
            Assert.AreEqual(30.00m, response.Data.EmployeeCompensations.Edges[0].Node.Rate?.Amount?.Amount);
            Assert.AreEqual(PayrollPayUnit.HOURLY, response.Data.EmployeeCompensations.Edges[0].Node.Rate?.PayUnit);
            Assert.AreEqual(1, response.Data.EmployeeCompensations.Nodes!.Length);
        }
    }
}
