using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;
using QuickBooksSharp.Infrastructure;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class PayrollEntityTests
    {
        private static readonly JsonSerializerOptions _options = QuickBooksHttpClient.JsonSerializerOptions;

        [TestMethod]
        public void Serialize_PayrollPayUnit_AllValues()
        {
            Assert.AreEqual("\"HOURLY\"", JsonSerializer.Serialize(PayrollPayUnit.HOURLY, _options));
            Assert.AreEqual("\"WEEKLY\"", JsonSerializer.Serialize(PayrollPayUnit.WEEKLY, _options));
            Assert.AreEqual("\"BIWEEKLY\"", JsonSerializer.Serialize(PayrollPayUnit.BIWEEKLY, _options));
            Assert.AreEqual("\"SEMIMONTHLY\"", JsonSerializer.Serialize(PayrollPayUnit.SEMIMONTHLY, _options));
            Assert.AreEqual("\"MONTHLY\"", JsonSerializer.Serialize(PayrollPayUnit.MONTHLY, _options));
            Assert.AreEqual("\"ANNUALLY\"", JsonSerializer.Serialize(PayrollPayUnit.ANNUALLY, _options));
            Assert.AreEqual("\"FLAT\"", JsonSerializer.Serialize(PayrollPayUnit.FLAT, _options));
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

            var comp = JsonSerializer.Deserialize<EmployeeCompensation>(json, _options);

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

            var json = JsonSerializer.Serialize(filter, _options);

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

            var json = JsonSerializer.Serialize(filter, _options);

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

            var conn = JsonSerializer.Deserialize<EmployeeCompensationConnection>(json, _options);

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

            var response = JsonSerializer.Deserialize<GraphQLResponse<EmployeeCompensationsQueryData>>(json, _options);

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
