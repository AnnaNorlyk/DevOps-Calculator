using NUnit.Framework;
using System;
using System.Linq;

namespace Calculator.Services.Tests
{
    [TestFixture]
    public class HistoryServiceIntegrationTests
    {
        private HistoryService _historyService;

        [SetUp]
        public void Setup()
        {

            var dbHost = Environment.GetEnvironmentVariable("MYSQL_HOST");
            var dbName = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var dbUser = Environment.GetEnvironmentVariable("MYSQL_USER");
            var dbPass = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

            var connString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPass};";
            
            _historyService = new HistoryService(connString);

        }

        [Test]
        public void Save_And_Retrieve_Correct_Record()
        {
            // Arrange
            _historyService.SaveCalculation("Add", 5, 3, 8);

            // Act
            var results = _historyService.GetLatestCalculations();

            // Assert
            Assert.That(results, Is.Not.Empty);
            var first = results.First();
            Assert.That(first.Operation, Is.EqualTo("Add"));
            Assert.That(first.OperandA, Is.EqualTo(5));
            Assert.That(first.OperandB, Is.EqualTo(3));
            Assert.That(first.Result, Is.EqualTo(8));
        }
    }
}
