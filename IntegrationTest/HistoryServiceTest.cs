using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Services;

namespace IntegrationTest
{
    [TestFixture]
    public class HistoryServiceTests
    {
        [Test]
        public void SaveCalculation_CallsExecuteNonQuery()
        {
            // Arrange
            var mockDb = new Mock<IDatabaseClient>();
            var service = new HistoryService(mockDb.Object);

            // Act
            service.SaveCalculation("Add", 5, 3, 8.0);

            // Assert
            mockDb.Verify(
                db => db.ExecuteNonQuery(
                    It.Is<string>(sql => sql.Contains("INSERT INTO CalculationHistory")),
                    It.IsAny<Dictionary<string, object>>()
                ),
                Times.Once
            );
        }

        [Test]
        public void GetLatestCalculations_Returns_ProperData()
        {
            // Arrange
            var mockDb = new Mock<IDatabaseClient>();

            // Fake two rows from the DB
            var fakeRows = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> {
                    ["Id"] = 101,
                    ["Operation"] = "Add",
                    ["OperandA"] = 5,
                    ["OperandB"] = 3,
                    ["Result"]   = 8.0,
                    ["CreatedAt"] = new DateTime(2023,1,1)
                },
                new Dictionary<string, object> {
                    ["Id"] = 102,
                    ["Operation"] = "Factorial",
                    ["OperandA"] = 5,
                    ["OperandB"] = DBNull.Value,
                    ["Result"]   = 120.0,
                    ["CreatedAt"] = new DateTime(2023,1,2)
                }
            };

            mockDb
                .Setup(db => db.ExecuteReader(
                    It.Is<string>(sql => sql.Contains("SELECT Id, Operation")),
                    It.IsAny<Dictionary<string, object>>()
                ))
                .Returns(fakeRows);

            var service = new HistoryService(mockDb.Object);

            // Act
            var calculations = service.GetLatestCalculations();

            // Assert
            Assert.That(calculations.Count, Is.EqualTo(2));
            var first = calculations.First();
            Assert.That(first.Id, Is.EqualTo(101));
            Assert.That(first.Operation, Is.EqualTo("Add"));
            Assert.That(first.OperandA, Is.EqualTo(5));
            Assert.That(first.OperandB, Is.EqualTo(3));
            Assert.That(first.Result, Is.EqualTo(8.0));
            Assert.That(first.CreatedAt, Is.EqualTo(new DateTime(2023,1,1)));

            var second = calculations.Last();
            Assert.That(second.Id, Is.EqualTo(102));
            Assert.That(second.Operation, Is.EqualTo("Factorial"));
            Assert.That(second.OperandA, Is.EqualTo(5));
            Assert.That(second.OperandB, Is.Null);
            Assert.That(second.Result, Is.EqualTo(120.0));
            Assert.That(second.CreatedAt, Is.EqualTo(new DateTime(2023,1,2)));
        }
    }
}
