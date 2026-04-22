using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class GraphQLQueryLoaderTests
    {
        [TestMethod]
        public void Load_GetProject_ReturnsValidQuery()
        {
            var query = GraphQLQueryLoader.Load("GetProject");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("query GetProject"));
            Assert.IsTrue(query.Contains("projectManagementProject"));
            Assert.IsTrue(query.Contains("$id: ID!"));
        }

        [TestMethod]
        public void Load_GetProjects_ReturnsValidQuery()
        {
            var query = GraphQLQueryLoader.Load("GetProjects");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("query GetProjects"));
            Assert.IsTrue(query.Contains("projectManagementProjects"));
            Assert.IsTrue(query.Contains("pageInfo"));
            Assert.IsTrue(query.Contains("hasNextPage"));
        }

        [TestMethod]
        public void Load_CreateProject_ReturnsValidMutation()
        {
            var query = GraphQLQueryLoader.Load("CreateProject");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("mutation CreateProject"));
            Assert.IsTrue(query.Contains("projectManagementCreateProject"));
            Assert.IsTrue(query.Contains("... on ProjectManagement_Project"));
            Assert.IsTrue(query.Contains("... on ProjectManagement_Error"));
        }

        [TestMethod]
        public void Load_DeleteProject_ReturnsValidMutation()
        {
            var query = GraphQLQueryLoader.Load("DeleteProject");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("mutation DeleteProject"));
            Assert.IsTrue(query.Contains("projectManagementDeleteProject"));
        }

        [TestMethod]
        public void Load_GetCustomFieldDefinitions_ReturnsValidQuery()
        {
            var query = GraphQLQueryLoader.Load("GetCustomFieldDefinitions");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("appFoundationsCustomFieldDefinitions"));
            Assert.IsTrue(query.Contains("legacyIDV2"));
            Assert.IsTrue(query.Contains("associations"));
        }

        [TestMethod]
        public void Load_CalculateSalesTax_ReturnsValidMutation()
        {
            var query = GraphQLQueryLoader.Load("CalculateSalesTax");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("mutation CalculateSalesTax"));
            Assert.IsTrue(query.Contains("indirectTaxCalculateSaleTransactionTax"));
            Assert.IsTrue(query.Contains("IndirectTax_TaxCalculationPayload"));
        }

        [TestMethod]
        public void Load_GetDimensionDefinitions_ReturnsValidQuery()
        {
            var query = GraphQLQueryLoader.Load("GetDimensionDefinitions");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("appFoundationsActiveCustomDimensionDefinitions"));
            Assert.IsTrue(query.Contains("sharedInfo"));
        }

        [TestMethod]
        public void Load_GetEmployeeCompensations_ReturnsValidQuery()
        {
            var query = GraphQLQueryLoader.Load("GetEmployeeCompensations");

            Assert.IsNotNull(query);
            Assert.IsTrue(query.Contains("payrollEmployeeCompensations"));
            Assert.IsTrue(query.Contains("employerCompensation"));
            Assert.IsTrue(query.Contains("payUnit"));
        }

        [TestMethod]
        public void Load_CachesResult()
        {
            var first = GraphQLQueryLoader.Load("GetProject");
            var second = GraphQLQueryLoader.Load("GetProject");

            Assert.AreSame(first, second);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void Load_NonExistentResource_Throws()
        {
            GraphQLQueryLoader.Load("NonExistentQuery");
        }
    }
}
