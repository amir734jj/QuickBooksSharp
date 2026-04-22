using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickBooksSharp.GraphQL;

namespace QuickBooksSharp.Tests
{
    [TestClass]
    public class GraphQLExceptionTests
    {
        [TestMethod]
        public void Constructor_FormatsMessageFromErrors()
        {
            var errors = new[]
            {
                new GraphQLError { Message = "Field not found" },
                new GraphQLError { Message = "Unauthorized access" }
            };

            var ex = new GraphQLException(errors);

            Assert.AreEqual(2, ex.Errors.Length);
            Assert.IsTrue(ex.Message.Contains("2 error(s)"));
            Assert.IsTrue(ex.Message.Contains("Field not found"));
            Assert.IsTrue(ex.Message.Contains("Unauthorized access"));
        }

        [TestMethod]
        public void Constructor_SingleError()
        {
            var errors = new[] { new GraphQLError { Message = "Something went wrong" } };

            var ex = new GraphQLException(errors);

            Assert.AreEqual(1, ex.Errors.Length);
            Assert.IsTrue(ex.Message.Contains("1 error(s)"));
            Assert.IsTrue(ex.Message.Contains("Something went wrong"));
        }
    }
}
