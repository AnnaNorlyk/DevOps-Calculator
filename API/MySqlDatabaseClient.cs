using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Calculator.Services
{
    public class MySqlDatabaseClient : IDatabaseClient
    {
        private readonly string _connectionString;

        // Constructor receives the connection string
        public MySqlDatabaseClient(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Used for INSERT, UPDATE, or DELETE statements
        public void ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            using var con = new MySqlConnection(_connectionString);
            con.Open();
            using var cmd = new MySqlCommand(sql, con);
            foreach (var kvp in parameters) cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
            cmd.ExecuteNonQuery();
        }
    

        // Used for SELECT statements that return rows
        public IEnumerable<Dictionary<string, object>> ExecuteReader(string sql, Dictionary<string, object> parameters)
        {
            var results = new List<Dictionary<string, object>>();

            // Create and open a MySQL connection
            using var con = new MySqlConnection(_connectionString);
            con.Open();

            // Build the command with parameters
            using var cmd = new MySqlCommand(sql, con);
            foreach (var kvp in parameters) cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);

            // Read each row from the data reader
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var colName = reader.GetName(i);
                    row[colName] = reader.IsDBNull(i) ? DBNull.Value : reader.GetValue(i);
                }
                results.Add(row);
            }
            return results;
        }
    }
}
