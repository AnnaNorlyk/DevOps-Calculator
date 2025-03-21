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

        public void SaveCalculation(string expression, string result)
        {
            const string insertSql = @"
                INSERT INTO CalculationHistory (Operation, OperandA, OperandB, Result)
                VALUES (@operation, 0, NULL, @result)";

            double numericResult = 0;
            double.TryParse(result, out numericResult);

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            using var cmd = new MySqlCommand(insertSql, con);
            cmd.Parameters.AddWithValue("@operation", expression);
            cmd.Parameters.AddWithValue("@result", numericResult);

            cmd.ExecuteNonQuery();
        }

        public List<string> GetLatestCalculations()
        {
            const string selectSql = @"
                SELECT Operation, Result
                FROM CalculationHistory
                ORDER BY Id DESC
                LIMIT 5";

            var calculations = new List<string>();

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            using var cmd = new MySqlCommand(selectSql, con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var operation = reader.GetString("Operation");
                var numericResult = reader.GetDouble("Result");
                calculations.Add($"{operation} = {numericResult}");
            }

            return calculations;
        }
    }
}
