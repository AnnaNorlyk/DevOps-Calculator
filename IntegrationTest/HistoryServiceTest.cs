using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Calculator.Services.Tests
{
    [TestFixture]
    public class HistoryServiceTests
    {
        [Test]
        public void SaveCalc_NonNullB()
        {
            // SaveCalculation with non-null operandB
            var mockDb = new Mock<IDatabaseClient>();
            var svc = new HistoryService(mockDb.Object);

            svc.SaveCalculation("Add", 5, 3, 8);

            mockDb.Verify(db => db.ExecuteNonQuery(
                It.Is<string>(sql => sql.Contains("INSERT INTO CalculationHistory")),
                It.Is<Dictionary<string, object>>(prms =>
                     prms["@op"].Equals("Add") &&
                     prms["@a"].Equals(5) &&
                     prms["@b"].Equals(3) &&
                     prms["@res"].Equals(8.0)
                )
            ), Times.Once);
        }

        [Test]
        public void SaveCalc_NullB()
        {
            // SaveCalculation with null operandB
            var mockDb = new Mock<IDatabaseClient>();
            var svc = new HistoryService(mockDb.Object);

            svc.SaveCalculation("Divide", 10, null, 5.0);

            mockDb.Verify(db => db.ExecuteNonQuery(
                It.IsAny<string>(),
                It.Is<Dictionary<string, object>>(prms =>
                     prms["@op"].Equals("Divide") &&
                     prms["@a"].Equals(10) &&
                     prms["@b"] == DBNull.Value &&
                     prms["@res"].Equals(5.0)
                )
            ), Times.Once);
        }

        [Test]
        public void GetCalc_NullB()
        {
            var mockDb = new Mock<IDatabaseClient>();
            var rows = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    ["Id"] = 101,
                    ["Operation"] = "Factorial",
                    ["OperandA"] = 5,
                    ["OperandB"] = DBNull.Value,
                    ["Result"] = 120.0,
                    ["CreatedAt"] = new DateTime(2024,1,1)
                }
            };
            mockDb.Setup(db => db.ExecuteReader(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                  .Returns(rows);

            var svc = new HistoryService(mockDb.Object);
            var results = svc.GetLatestCalculations();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Id, Is.EqualTo(101));
            Assert.That(results[0].OperandB, Is.Null);
        }

        [Test]
        public void GetCalc_NonNullB()
        {
            // GetLatestCalculations with a normal integer for operandB
            var mockDb = new Mock<IDatabaseClient>();
            var rows = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    ["Id"] = 102,
                    ["Operation"] = "Add",
                    ["OperandA"] = 2,
                    ["OperandB"] = 3,
                    ["Result"] = 5.0,
                    ["CreatedAt"] = new DateTime(2024,2,2)
                }
            };
            mockDb.Setup(db => db.ExecuteReader(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                  .Returns(rows);

            var svc = new HistoryService(mockDb.Object);
            var results = svc.GetLatestCalculations();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Id, Is.EqualTo(102));
            Assert.That(results[0].OperandB, Is.EqualTo(3));
        }

        [Test]
        public void GetCalc_MultipleRows()
        {
            // GetLatestCalculations with multiple rows
            var mockDb = new Mock<IDatabaseClient>();
            var rows = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    ["Id"] = 200,
                    ["Operation"] = "Subtract",
                    ["OperandA"] = 10,
                    ["OperandB"] = 5,
                    ["Result"] = 5.0,
                    ["CreatedAt"] = new DateTime(2024,3,3)
                },
                new Dictionary<string, object>
                {
                    ["Id"] = 201,
                    ["Operation"] = "Add",
                    ["OperandA"] = 6,
                    ["OperandB"] = 4,
                    ["Result"] = 10.0,
                    ["CreatedAt"] = new DateTime(2024,3,4)
                }
            };
            mockDb.Setup(db => db.ExecuteReader(It.IsAny<string>(), It.IsAny<Dictionary<string, object>>()))
                  .Returns(rows);

            var svc = new HistoryService(mockDb.Object);
            var results = svc.GetLatestCalculations();

            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results[0].Id, Is.EqualTo(200));
            Assert.That(results[1].Id, Is.EqualTo(201));
        }
    }
}
