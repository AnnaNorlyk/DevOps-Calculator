using System.Collections.Generic;

namespace Calculator.Services
{

    public interface IDatabaseClient
    {
        // For INSERT/UPDATE/DELETE commands
        void ExecuteNonQuery(string sql, Dictionary<string, object> parameters);

        // For SELECT commands returning multiple rows
        IEnumerable<Dictionary<string, object>> ExecuteReader(
            string sql,
            Dictionary<string, object> parameters
        );
    }
}
