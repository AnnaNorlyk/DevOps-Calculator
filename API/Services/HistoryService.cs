using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Calculator.Services
{
    public class HistoryService
{
    private readonly IDatabaseClient _db;

    public HistoryService(IDatabaseClient db)
    {
        _db = db;
    }

    public void SaveCalculation(string operationName, int operandA, int? operandB, double result)
    {
        const string insertSql = @"
            INSERT INTO CalculationHistory (Operation, OperandA, OperandB, Result)
            VALUES (@op, @a, @b, @res)";

        var parameters = new Dictionary<string, object>
        {
            ["@op"] = operationName,
            ["@a"] = operandA,
            ["@b"] = operandB ?? (object)DBNull.Value,
            ["@res"] = result
        };

        _db.ExecuteNonQuery(insertSql, parameters);
    }

    public List<CalculationHistory> GetLatestCalculations()
    {
        const string selectSql = @"
            SELECT Id, Operation, OperandA, OperandB, Result, CreatedAt
            FROM CalculationHistory
            ORDER BY Id DESC
            LIMIT 5";

        var rows = _db.ExecuteReader(selectSql, new Dictionary<string, object>());
        var calculations = new List<CalculationHistory>();

        foreach (var row in rows)
        {
            var record = new CalculationHistory
            {
                Id        = Convert.ToInt32(row["Id"]),
                Operation = row["Operation"].ToString(),
                OperandA  = row["OperandA"] is DBNull ? null : (int?)Convert.ToInt32(row["OperandA"]),
                OperandB  = row["OperandB"] is DBNull ? null : (int?)Convert.ToInt32(row["OperandB"]),
                Result    = Convert.ToDouble(row["Result"]),
                CreatedAt = Convert.ToDateTime(row["CreatedAt"])
            };
            calculations.Add(record);
        }
        return calculations;
        }   
    }
}
