using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual("\"HOURLY\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.HOURLY));
            Assert.AreEqual("\"WEEKLY\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.WEEKLY));
            Assert.AreEqual("\"BIWEEKLY\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.BIWEEKLY));
            Assert.AreEqual("\"SEMIMONTHLY\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.SEMIMONTHLY));
            Assert.AreEqual("\"MONTHLY\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.MONTHLY));
            Assert.AreEqual("\"ANNUALLY\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.ANNUALLY));
            Assert.AreEqual("\"FLAT\"", Newtonsoft.Json.JsonConvert.SerializeObject(PayrollPayUnit.FLAT));
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

            var comp = Newtonsoft.Json.JsonConvert.DeserializeObject<EmployeeCompensation>(json);

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

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(filter);

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

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(filter);

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

            var conn = Newtonsoft.Json.JsonConvert.DeserializeObject<EmployeeCompensationConnection>(json);

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

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphQLResponse<EmployeeCompensationsQueryData>>(json);

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
