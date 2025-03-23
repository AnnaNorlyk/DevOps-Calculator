using Calculator;
using Calculator.Services;
using Microsoft.AspNetCore.Builder;
using MySql.Data.MySqlClient;


public static class CalculatorEndpoints
{
    public static void MapCalculatorEndpoints(this WebApplication app)
    {
        // ===================== SIMPLE ============================
        app.MapGet("api/simple/add", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            var result = calc.Add(a, b);
            history.SaveCalculation("Add", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/simple/subtract", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            var result = calc.Subtract(a, b);
            history.SaveCalculation("Subtract", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/simple/multiply", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            var result = calc.Multiply(a, b);
            history.SaveCalculation("Multiply", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/simple/divide", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            if (b == 0) return Results.BadRequest("Cannot divide by zero.");
            var result = calc.Divide(a, b);
            history.SaveCalculation("Divide", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/simple/factorial", (int a, SimpleCalculator calc, HistoryService history) =>
        {
            if (a < 0) return Results.BadRequest("Factorial cannot be negative.");
            var result = calc.Factorial(a);
            history.SaveCalculation("Factorial", a, null, result);
            return Results.Ok(result);
        });

        app.MapGet("api/simple/prime", (int a, SimpleCalculator calc, HistoryService history) =>
{
            bool isPrime = calc.IsPrime(a);
            string primeString = isPrime.ToString().ToLowerInvariant(); 
            history.SaveCalculation("IsPrime", a, null, primeString);
            return Results.Ok(isPrime);
        });


        // ===================== CACHED ============================
        app.MapGet("api/cached/add", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            var result = calc.Add(a, b);
            history.SaveCalculation("Add (cached)", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/cached/subtract", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            var result = calc.Subtract(a, b);
            history.SaveCalculation("Subtract (cached)", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/cached/multiply", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            var result = calc.Multiply(a, b);
            history.SaveCalculation("Multiply (cached)", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/cached/divide", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            if (b == 0) return Results.BadRequest("Cannot divide by zero.");
            var result = calc.Divide(a, b);
            history.SaveCalculation("Divide (cached)", a, b, result);
            return Results.Ok(result);
        });

        app.MapGet("api/cached/factorial", (int a, CachedCalculator calc, HistoryService history) =>
        {
            if (a < 0) return Results.BadRequest("Factorial cannot be negative.");
            var result = calc.Factorial(a);
            history.SaveCalculation("Factorial (cached)", a, null, result);
            return Results.Ok(result);
        });

        app.MapGet("api/cached/prime", (int a, CachedCalculator calc, HistoryService history) =>
        {
            var isPrime = calc.IsPrime(a);
            double numericResult = isPrime ? 1 : 0;
            history.SaveCalculation("IsPrime (cached)", a, null, numericResult);
            return Results.Ok(isPrime);
        });

        // Return the last 5 calculations
        app.MapGet("api/history", (HistoryService history) =>
        {
            var calculations = history.GetLatestCalculations();
            return Results.Ok(calculations);
        });


        //Debug endpoint
        app.MapGet("/debug/dbtest", async (IConfiguration config) =>
        {
            var cs = config.GetConnectionString("DefaultConnection") 
                     ?? "Server=mariadb;Database=mariadatabase;User=secretuser;Password=secretpassword;";

            try
            {
                using var con = new MySqlConnection(cs);
                await con.OpenAsync();
                return Results.Ok("DB connection succeeded");
            }
            catch (Exception ex)
            {
                return Results.Problem($"Failed: {ex.Message}");
            }
        });

    }
}
