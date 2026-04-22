using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;
using QuickBooksSharp.Infrastructure;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class ProjectEntityTests
    {
        private static readonly JsonSerializerOptions _options = QuickBooksHttpClient.JsonSerializerOptions;

        [TestMethod]
        public void Deserialize_Project()
        {
            var json = @"{
                ""id"": ""djQ6123"",
                ""name"": ""Website Redesign"",
                ""description"": ""Full site overhaul"",
                ""status"": ""IN_PROGRESS"",
                ""version"": 3,
                ""startDate"": ""2026-01-15"",
                ""dueDate"": ""2026-06-30"",
                ""completedDate"": null,
                ""completionRate"": 45.5,
                ""deleted"": false,
                ""pinned"": true,
                ""priority"": 7,
                ""type"": ""Development"",
                ""customer"": {
                    ""id"": ""cust-1"",
                    ""displayName"": ""Acme Corp""
                }
            }";

            var project = JsonSerializer.Deserialize<Project>(json, _options);

            Assert.IsNotNull(project);
            Assert.AreEqual("djQ6123", project.Id);
            Assert.AreEqual("Website Redesign", project.Name);
            Assert.AreEqual("Full site overhaul", project.Description);
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, project.Status);
            Assert.AreEqual(3, project.Version);
            Assert.AreEqual("2026-01-15", project.StartDate);
            Assert.AreEqual("2026-06-30", project.DueDate);
            Assert.IsNull(project.CompletedDate);
            Assert.AreEqual(45.5m, project.CompletionRate);
            Assert.AreEqual(false, project.Deleted);
            Assert.AreEqual(true, project.Pinned);
            Assert.AreEqual(7, project.Priority);
            Assert.AreEqual("Development", project.Type);
            Assert.IsNotNull(project.Customer);
            Assert.AreEqual("cust-1", project.Customer.Id);
            Assert.AreEqual("Acme Corp", project.Customer.DisplayName);
        }

        [TestMethod]
        public void Serialize_ProjectStatus_AllValues()
        {
            Assert.AreEqual("\"OPEN\"", JsonSerializer.Serialize(ProjectStatus.OPEN, _options));
            Assert.AreEqual("\"IN_PROGRESS\"", JsonSerializer.Serialize(ProjectStatus.IN_PROGRESS, _options));
            Assert.AreEqual("\"BLOCKED\"", JsonSerializer.Serialize(ProjectStatus.BLOCKED, _options));
            Assert.AreEqual("\"CANCELED\"", JsonSerializer.Serialize(ProjectStatus.CANCELED, _options));
            Assert.AreEqual("\"COMPLETE\"", JsonSerializer.Serialize(ProjectStatus.COMPLETE, _options));
            Assert.AreEqual("\"OTHER\"", JsonSerializer.Serialize(ProjectStatus.OTHER, _options));
            Assert.AreEqual("\"WAITING_ON_CLIENT\"", JsonSerializer.Serialize(ProjectStatus.WAITING_ON_CLIENT, _options));
        }

        [TestMethod]
        public void Deserialize_ProjectStatus_AllValues()
        {
            Assert.AreEqual(ProjectStatus.OPEN, JsonSerializer.Deserialize<ProjectStatus>("\"OPEN\"", _options));
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, JsonSerializer.Deserialize<ProjectStatus>("\"IN_PROGRESS\"", _options));
            Assert.AreEqual(ProjectStatus.BLOCKED, JsonSerializer.Deserialize<ProjectStatus>("\"BLOCKED\"", _options));
            Assert.AreEqual(ProjectStatus.CANCELED, JsonSerializer.Deserialize<ProjectStatus>("\"CANCELED\"", _options));
            Assert.AreEqual(ProjectStatus.COMPLETE, JsonSerializer.Deserialize<ProjectStatus>("\"COMPLETE\"", _options));
            Assert.AreEqual(ProjectStatus.OTHER, JsonSerializer.Deserialize<ProjectStatus>("\"OTHER\"", _options));
            Assert.AreEqual(ProjectStatus.WAITING_ON_CLIENT, JsonSerializer.Deserialize<ProjectStatus>("\"WAITING_ON_CLIENT\"", _options));
        }

        [TestMethod]
        public void Serialize_ProjectOrderBy()
        {
            Assert.AreEqual("\"NAME_ASC\"", JsonSerializer.Serialize(ProjectOrderBy.NAME_ASC, _options));
            Assert.AreEqual("\"DUE_DATE_DESC\"", JsonSerializer.Serialize(ProjectOrderBy.DUE_DATE_DESC, _options));
            Assert.AreEqual("\"STATUS_ASC\"", JsonSerializer.Serialize(ProjectOrderBy.STATUS_ASC, _options));
        }

        [TestMethod]
        public void Serialize_CreateProjectInput()
        {
            var input = new CreateProjectInput
            {
                Name = "New Project",
                Description = "Description",
                Customer = new ProjectCustomerInput { Id = "cust-1" },
                Status = ProjectStatus.OPEN,
                StartDate = "2026-05-01",
                Priority = 5
            };

            var json = JsonSerializer.Serialize(input, _options);

            Assert.IsTrue(json.Contains("\"name\":\"New Project\""));
            Assert.IsTrue(json.Contains("\"description\":\"Description\""));
            Assert.IsTrue(json.Contains("\"status\":\"OPEN\""));
            Assert.IsTrue(json.Contains("\"startDate\":\"2026-05-01\""));
            Assert.IsTrue(json.Contains("\"priority\":5"));
            Assert.IsTrue(json.Contains("\"id\":\"cust-1\""));
        }

        [TestMethod]
        public void Serialize_CreateProjectInput_NullFieldsOmitted()
        {
            var input = new CreateProjectInput
            {
                Name = "Minimal Project"
            };

            var json = JsonSerializer.Serialize(input, _options);

            Assert.IsTrue(json.Contains("\"name\":\"Minimal Project\""));
            Assert.IsFalse(json.Contains("\"description\""));
            Assert.IsFalse(json.Contains("\"customer\""));
            Assert.IsFalse(json.Contains("\"startDate\""));
            Assert.IsFalse(json.Contains("\"dueDate\""));
        }

        [TestMethod]
        public void Serialize_DeleteProjectInput()
        {
            var input = new DeleteProjectInput { Id = "proj-1", Version = 5 };

            var json = JsonSerializer.Serialize(input, _options);

            Assert.IsTrue(json.Contains("\"id\":\"proj-1\""));
            Assert.IsTrue(json.Contains("\"version\":5"));
        }

        [TestMethod]
        public void Serialize_ProjectFilter()
        {
            var filter = new ProjectFilter
            {
                Status = new ProjectStatusExpression { EqualsValue = ProjectStatus.COMPLETE },
                Deleted = false
            };

            var json = JsonSerializer.Serialize(filter, _options);

            Assert.IsTrue(json.Contains("\"equals\":\"COMPLETE\""));
            Assert.IsTrue(json.Contains("\"deleted\":false"));
        }

        [TestMethod]
        public void Deserialize_ProjectConnection()
        {
            var json = @"{
                ""edges"": [
                    {
                        ""node"": { ""id"": ""p1"", ""name"": ""Project 1"", ""status"": ""OPEN"", ""version"": 1 },
                        ""cursor"": ""abc""
                    },
                    {
                        ""node"": { ""id"": ""p2"", ""name"": ""Project 2"", ""status"": ""COMPLETE"", ""version"": 2 },
                        ""cursor"": ""def""
                    }
                ],
                ""pageInfo"": {
                    ""hasNextPage"": true,
                    ""hasPreviousPage"": false,
                    ""startCursor"": ""abc"",
                    ""endCursor"": ""def""
                }
            }";

            var connection = JsonSerializer.Deserialize<ProjectConnection>(json, _options);

            Assert.IsNotNull(connection);
            Assert.AreEqual(2, connection.Edges!.Length);
            Assert.AreEqual("p1", connection.Edges[0].Node!.Id);
            Assert.AreEqual(ProjectStatus.OPEN, connection.Edges[0].Node.Status);
            Assert.AreEqual("abc", connection.Edges[0].Cursor);
            Assert.AreEqual("p2", connection.Edges[1].Node!.Id);
            Assert.AreEqual(ProjectStatus.COMPLETE, connection.Edges[1].Node.Status);
            Assert.IsNotNull(connection.PageInfo);
            Assert.IsTrue(connection.PageInfo.HasNextPage);
            Assert.IsFalse(connection.PageInfo.HasPreviousPage);
            Assert.AreEqual("abc", connection.PageInfo.StartCursor);
            Assert.AreEqual("def", connection.PageInfo.EndCursor);
        }

        [TestMethod]
        public void Deserialize_ProjectMutationResult_Success()
        {
            var json = @"{ ""id"": ""p1"", ""name"": ""Created"", ""version"": 1, ""status"": ""OPEN"" }";

            var result = JsonSerializer.Deserialize<ProjectMutationResult>(json, _options);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("p1", result.Id);
            Assert.AreEqual("Created", result.Name);
        }

        [TestMethod]
        public void Deserialize_ProjectMutationResult_Error()
        {
            var json = @"{ ""message"": ""Project not found"", ""classification"": ""NOT_FOUND"" }";

            var result = JsonSerializer.Deserialize<ProjectMutationResult>(json, _options);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsError);
            Assert.AreEqual("Project not found", result.ErrorMessage);
            Assert.AreEqual("NOT_FOUND", result.ErrorClassification);
        }

        [TestMethod]
        public void Deserialize_FullProjectsGraphQLResponse()
        {
            var json = @"{
                ""data"": {
                    ""projectManagementProjects"": {
                        ""edges"": [
                            {
                                ""node"": { ""id"": ""p1"", ""name"": ""Alpha"", ""status"": ""IN_PROGRESS"", ""version"": 1 },
                                ""cursor"": ""c1""
                            }
                        ],
                        ""pageInfo"": {
                            ""hasNextPage"": false,
                            ""hasPreviousPage"": false,
                            ""startCursor"": ""c1"",
                            ""endCursor"": ""c1""
                        }
                    }
                }
            }";

            var response = JsonSerializer.Deserialize<GraphQLResponse<ProjectsQueryData>>(json, _options);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.Projects);
            Assert.AreEqual(1, response.Data.Projects.Edges!.Length);
            Assert.AreEqual("Alpha", response.Data.Projects.Edges[0].Node!.Name);
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, response.Data.Projects.Edges[0].Node.Status);
        }
    }
}
