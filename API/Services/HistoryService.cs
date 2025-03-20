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
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            using var cmd = new MySqlCommand(
                @"INSERT INTO tblCalculations (expression, result) 
                  VALUES (@expression, @result)", con);
            cmd.Parameters.AddWithValue("@expression", expression);
            cmd.Parameters.AddWithValue("@result", result);
            cmd.ExecuteNonQuery();
        }

        public List<string> GetLatestCalculations()
        {
            var calculations = new List<string>();

            using var con = new MySqlConnection(_connectionString);
            con.Open();

            using var cmd = new MySqlCommand(
                @"SELECT expression, result 
                  FROM tblCalculations 
                  ORDER BY id DESC 
                  LIMIT 5", con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var expr = reader.GetString(0);
                var res  = reader.GetString(1);
                calculations.Add($"{expr} = {res}");
            }

            return calculations;
        }
    }
}
