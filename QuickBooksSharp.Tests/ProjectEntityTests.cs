using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            var project = Newtonsoft.Json.JsonConvert.DeserializeObject<Project>(json);

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
            Assert.AreEqual("\"OPEN\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.OPEN));
            Assert.AreEqual("\"IN_PROGRESS\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.IN_PROGRESS));
            Assert.AreEqual("\"BLOCKED\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.BLOCKED));
            Assert.AreEqual("\"CANCELED\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.CANCELED));
            Assert.AreEqual("\"COMPLETE\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.COMPLETE));
            Assert.AreEqual("\"OTHER\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.OTHER));
            Assert.AreEqual("\"WAITING_ON_CLIENT\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectStatus.WAITING_ON_CLIENT));
        }

        [TestMethod]
        public void Deserialize_ProjectStatus_AllValues()
        {
            Assert.AreEqual(ProjectStatus.OPEN, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"OPEN\""));
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"IN_PROGRESS\""));
            Assert.AreEqual(ProjectStatus.BLOCKED, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"BLOCKED\""));
            Assert.AreEqual(ProjectStatus.CANCELED, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"CANCELED\""));
            Assert.AreEqual(ProjectStatus.COMPLETE, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"COMPLETE\""));
            Assert.AreEqual(ProjectStatus.OTHER, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"OTHER\""));
            Assert.AreEqual(ProjectStatus.WAITING_ON_CLIENT, Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectStatus>("\"WAITING_ON_CLIENT\""));
        }

        [TestMethod]
        public void Serialize_ProjectOrderBy()
        {
            Assert.AreEqual("\"NAME_ASC\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectOrderBy.NAME_ASC));
            Assert.AreEqual("\"DUE_DATE_DESC\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectOrderBy.DUE_DATE_DESC));
            Assert.AreEqual("\"STATUS_ASC\"", Newtonsoft.Json.JsonConvert.SerializeObject(ProjectOrderBy.STATUS_ASC));
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

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);

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

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);

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

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(input);

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

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(filter);

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

            var connection = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectConnection>(json);

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

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectMutationResult>(json);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsError);
            Assert.AreEqual("p1", result.Id);
            Assert.AreEqual("Created", result.Name);
        }

        [TestMethod]
        public void Deserialize_ProjectMutationResult_Error()
        {
            var json = @"{ ""message"": ""Project not found"", ""classification"": ""NOT_FOUND"" }";

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectMutationResult>(json);

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

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<GraphQLResponse<ProjectsQueryData>>(json);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasErrors);
            Assert.IsNotNull(response.Data?.Projects);
            Assert.AreEqual(1, response.Data.Projects.Edges!.Length);
            Assert.AreEqual("Alpha", response.Data.Projects.Edges[0].Node!.Name);
            Assert.AreEqual(ProjectStatus.IN_PROGRESS, response.Data.Projects.Edges[0].Node.Status);
        }
    }
}
