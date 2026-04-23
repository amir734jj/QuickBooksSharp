using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using QuickBooksSharp.GraphQL;
using QuickBooksSharp.GraphQL.Entities;
using QuickBooksSharp.GraphQL.Services;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class ProjectEntityTests
    {
        
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

            var project = JsonConvert.DeserializeObject<Project>(json, GraphQLClient.JsonSettings);

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
            Assert.AreEqual("\"OPEN\"", JsonConvert.SerializeObject(ProjectStatus.OPEN, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"IN_PROGRESS\"", JsonConvert.SerializeObject(ProjectStatus.IN_PROGRESS, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"BLOCKED\"", JsonConvert.SerializeObject(ProjectStatus.BLOCKED, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"CANCELED\"", JsonConvert.SerializeObject(ProjectStatus.CANCELED, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"COMPLETE\"", JsonConvert.SerializeObject(ProjectStatus.COMPLETE, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"OTHER\"", JsonConvert.SerializeObject(ProjectStatus.OTHER, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"WAITING_ON_CLIENT\"", JsonConvert.SerializeObject(ProjectStatus.WAITING_ON_CLIENT, GraphQLClient.JsonSettings));
        }

        [TestMethod]
        public void Deserialize_ProjectStatus_AllValues()
        {
            Assert.AreEqual(ProjectStatus.OPEN, JsonConvert.DeserializeObject<ProjectStatus>("\"OPEN\"", GraphQLClient.JsonSettings));
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, JsonConvert.DeserializeObject<ProjectStatus>("\"IN_PROGRESS\"", GraphQLClient.JsonSettings));
            Assert.AreEqual(ProjectStatus.BLOCKED, JsonConvert.DeserializeObject<ProjectStatus>("\"BLOCKED\"", GraphQLClient.JsonSettings));
            Assert.AreEqual(ProjectStatus.CANCELED, JsonConvert.DeserializeObject<ProjectStatus>("\"CANCELED\"", GraphQLClient.JsonSettings));
            Assert.AreEqual(ProjectStatus.COMPLETE, JsonConvert.DeserializeObject<ProjectStatus>("\"COMPLETE\"", GraphQLClient.JsonSettings));
            Assert.AreEqual(ProjectStatus.OTHER, JsonConvert.DeserializeObject<ProjectStatus>("\"OTHER\"", GraphQLClient.JsonSettings));
            Assert.AreEqual(ProjectStatus.WAITING_ON_CLIENT, JsonConvert.DeserializeObject<ProjectStatus>("\"WAITING_ON_CLIENT\"", GraphQLClient.JsonSettings));
        }

        [TestMethod]
        public void Serialize_ProjectOrderBy()
        {
            Assert.AreEqual("\"NAME_ASC\"", JsonConvert.SerializeObject(ProjectOrderBy.NAME_ASC, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"DUE_DATE_DESC\"", JsonConvert.SerializeObject(ProjectOrderBy.DUE_DATE_DESC, GraphQLClient.JsonSettings));
            Assert.AreEqual("\"STATUS_ASC\"", JsonConvert.SerializeObject(ProjectOrderBy.STATUS_ASC, GraphQLClient.JsonSettings));
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

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

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

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

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

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

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

            var json = JsonConvert.SerializeObject(filter, GraphQLClient.JsonSettings);

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

            var connection = JsonConvert.DeserializeObject<ProjectConnection>(json, GraphQLClient.JsonSettings);

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

            var result = JsonConvert.DeserializeObject<ProjectMutationResult>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("p1", result.Id);
            Assert.AreEqual("Created", result.Name);
        }

        [TestMethod]
        public void Deserialize_ProjectMutationResult_Error()
        {
            var json = @"{ ""message"": ""Project not found"", ""classification"": ""NOT_FOUND"" }";

            var result = JsonConvert.DeserializeObject<ProjectMutationResult>(json, GraphQLClient.JsonSettings);

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

            var response = JsonConvert.DeserializeObject<GraphQLResponse<ProjectsQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.Projects);
            Assert.AreEqual(1, response.Data.Projects.Edges!.Length);
            Assert.AreEqual("Alpha", response.Data.Projects.Edges[0].Node!.Name);
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, response.Data.Projects.Edges[0].Node.Status);
        }

        [TestMethod]
        public void Serialize_UpdateProjectInput()
        {
            var input = new UpdateProjectInput
            {
                Id = "proj-1",
                Version = 2,
                Name = "Updated",
                Status = ProjectStatus.COMPLETE,
                Description = "Done",
                Customer = new ProjectCustomerInput { Id = "c1" },
                Client = new ProjectClientInput { Id = "cl1" },
                Assignee = new ProjectPersonaInput { Id = "p1" },
                StartDate = "2026-01-01",
                DueDate = "2026-12-31",
                CompletedDate = "2026-06-15",
                CompletionRate = 100m,
                Pinned = false,
                Priority = 9,
                Type = "Consulting"
            };

            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"id\":\"proj-1\""));
            Assert.IsTrue(json.Contains("\"version\":2"));
            Assert.IsTrue(json.Contains("\"name\":\"Updated\""));
            Assert.IsTrue(json.Contains("\"status\":\"COMPLETE\""));
        }

        [TestMethod]
        public void Serialize_DeleteProjectInput_WithVersion()
        {
            var input = new DeleteProjectInput { Id = "proj-1", Version = 3 };
            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"id\":\"proj-1\""));
            Assert.IsTrue(json.Contains("\"version\":3"));
        }

        [TestMethod]
        public void Deserialize_Project_WithAllNestedTypes()
        {
            var json = @"{
                ""id"": ""p1"",
                ""name"": ""Test"",
                ""version"": 1,
                ""client"": { ""id"": ""cl1"", ""displayName"": ""Client Co"" },
                ""assignee"": { ""id"": ""a1"", ""displayName"": ""John"" },
                ""completedBy"": { ""id"": ""u1"", ""displayName"": ""Jane"" }
            }";

            var project = JsonConvert.DeserializeObject<Project>(json, GraphQLClient.JsonSettings);

            Assert.IsNotNull(project);
            Assert.AreEqual("cl1", project.Client?.Id);
            Assert.AreEqual("Client Co", project.Client?.DisplayName);
            Assert.AreEqual("a1", project.Assignee?.Id);
            Assert.AreEqual("John", project.Assignee?.DisplayName);
            Assert.AreEqual("u1", project.CompletedBy?.Id);
            Assert.AreEqual("Jane", project.CompletedBy?.DisplayName);
        }

        [TestMethod]
        public void Serialize_ProjectFilter_AllFields()
        {
            var filter = new ProjectFilter
            {
                Status = new ProjectStatusExpression { EqualsValue = ProjectStatus.OPEN },
                Customer = new ProjectIdExpression { EqualsValue = "c1" },
                Id = new ProjectIdExpression { EqualsValue = "p1" },
                Deleted = true,
                IncludeTasks = true,
                Type = new ProjectStringExpression { EqualsValue = "Dev" }
            };

            var json = JsonConvert.SerializeObject(filter, GraphQLClient.JsonSettings);

            Assert.IsTrue(json.Contains("\"equals\":\"OPEN\""));
            Assert.IsTrue(json.Contains("\"deleted\":true"));
            Assert.IsTrue(json.Contains("\"includeTasks\":true"));
        }

        [TestMethod]
        public void Serialize_ProjectUserInput()
        {
            var input = new ProjectUserInput { Id = "u1" };
            var json = JsonConvert.SerializeObject(input, GraphQLClient.JsonSettings);
            Assert.IsTrue(json.Contains("\"id\":\"u1\""));
        }
    }
}
