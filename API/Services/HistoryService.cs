using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Calculator.Services
{
    public class HistoryService
    {
        private readonly string _connectionString;

        public HistoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveCalculation(string operationName, int operandA, int? operandB, double result)
{
    const string insertSql = @"
        INSERT INTO CalculationHistory (Operation, OperandA, OperandB, Result)
        VALUES (@op, @a, @b, @res)";

    try
    {
        using var con = new MySqlConnection(_connectionString);
        con.Open();

        using var cmd = new MySqlCommand(insertSql, con);
        cmd.Parameters.AddWithValue("@op", operationName);
        cmd.Parameters.AddWithValue("@a", operandA);
        cmd.Parameters.AddWithValue("@b", operandB.HasValue ? operandB : (object)DBNull.Value);
        cmd.Parameters.AddWithValue("@res", result);

        cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
      
        Console.WriteLine($"[HistoryService] SaveCalculation error: {ex.Message}");
        throw; 
    }
}

        public List<CalculationHistory> GetLatestCalculations()
        {
            const string selectSql = @"
                SELECT Id, Operation, OperandA, OperandB, Result, CreatedAt
                FROM CalculationHistory
                ORDER BY Id DESC
                LIMIT 5";

            var calculations = new List<CalculationHistory>();

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            using var cmd = new MySqlCommand(selectSql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int idOrdinal = reader.GetOrdinal("Id");
                int operationOrdinal = reader.GetOrdinal("Operation");
                int operandAOrdinal = reader.GetOrdinal("OperandA");
                int operandBOrdinal = reader.GetOrdinal("OperandB");
                int resultOrdinal = reader.GetOrdinal("Result");
                int createdAtOrdinal = reader.GetOrdinal("CreatedAt");

                var record = new CalculationHistory
                {
                    Id = reader.GetInt32(idOrdinal),
                    Operation = reader.GetString(operationOrdinal),
                    OperandA = reader.IsDBNull(operandAOrdinal)
                        ? (int?)null
                        : reader.GetInt32(operandAOrdinal),
                    OperandB = reader.IsDBNull(operandBOrdinal)
                        ? (int?)null
                        : reader.GetInt32(operandBOrdinal),
                    Result = reader.GetDouble(resultOrdinal),
                    CreatedAt = reader.GetDateTime(createdAtOrdinal)
                };

                calculations.Add(record);
            }

            return calculations;
        }
    }
}
