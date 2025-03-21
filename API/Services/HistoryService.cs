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

        public void SaveCalculation(
            string operationName,
            int? operandA,
            int? operandB,
            double result)
        {
            const string insertSql = @"
                INSERT INTO CalculationHistory (Operation, OperandA, OperandB, Result)
                VALUES (@op, @a, @b, @res)";

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            using var cmd = new MySqlCommand(insertSql, con);
            cmd.Parameters.AddWithValue("@op", operationName);
            cmd.Parameters.AddWithValue("@a", operandA.HasValue ? operandA : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@b", operandB.HasValue ? operandB : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@res", result);

            cmd.ExecuteNonQuery();
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
                var record = new CalculationHistory
                {
                    Id        = reader.GetInt32("Id"),
                    Operation = reader.GetString("Operation"),
                    OperandA  = reader.IsDBNull("OperandA") ? null : reader.GetInt32("OperandA"),
                    OperandB  = reader.IsDBNull("OperandB") ? null : reader.GetInt32("OperandB"),
                    Result    = reader.GetDouble("Result"),
                    CreatedAt = reader.GetDateTime("CreatedAt")
                };
                calculations.Add(record);
            }

            return calculations;
        }
    }
}
