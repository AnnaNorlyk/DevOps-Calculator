using Calculator;
using Calculator.Services;
using Microsoft.AspNetCore.Builder;

public static class CalculatorEndpoints
{
    public static void MapCalculatorEndpoints(this WebApplication app)
    {
        // ============================== SIMPLE ==========================
        app.MapGet("api/simple/add", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            var result = calc.Add(a, b);
            var expression = $"{a}+{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/simple/subtract", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            var result = calc.Subtract(a, b);
            var expression = $"{a}-{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/simple/multiply", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            var result = calc.Multiply(a, b);
            var expression = $"{a}X{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/simple/divide", (int a, int b, SimpleCalculator calc, HistoryService history) =>
        {
            if (b == 0) return Results.BadRequest("Cannot divide by zero.");
            var result = calc.Divide(a, b);
            var expression = $"{a}/{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/simple/factorial", (int a, SimpleCalculator calc, HistoryService history) =>
        {
            if (a < 0) return Results.BadRequest("Factorial cannot be negative.");
            var result = calc.Factorial(a);
            var expression = $"Factorial({a})";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/simple/prime", (int a, SimpleCalculator calc, HistoryService history) =>
        {
            var isPrime = calc.IsPrime(a);
            var expression = $"IsPrime({a})";
            history.SaveCalculation(expression, isPrime.ToString());
            return Results.Ok(isPrime);
        });

        // ======================== CACHED ==================================
        app.MapGet("api/cached/add", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            var result = calc.Add(a, b);
            var expression = $"{a}+{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/cached/subtract", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            var result = calc.Subtract(a, b);
            var expression = $"{a}-{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/cached/multiply", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            var result = calc.Multiply(a, b);
            var expression = $"{a}X{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/cached/divide", (int a, int b, CachedCalculator calc, HistoryService history) =>
        {
            if (b == 0) return Results.BadRequest("Cannot divide by zero.");
            var result = calc.Divide(a, b);
            var expression = $"{a}/{b}";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/cached/factorial", (int a, CachedCalculator calc, HistoryService history) =>
        {
            if (a < 0) return Results.BadRequest("Factorial cannot be negative.");
            var result = calc.Factorial(a);
            var expression = $"Factorial({a})";
            history.SaveCalculation(expression, result.ToString());
            return Results.Ok(result);
        });

        app.MapGet("api/cached/prime", (int a, CachedCalculator calc, HistoryService history) =>
        {
            var isPrime = calc.IsPrime(a);
            var expression = $"IsPrime({a})";
            history.SaveCalculation(expression, isPrime.ToString());
            return Results.Ok(isPrime);
        });


        app.MapGet("api/history", (HistoryService history) =>
        {
            var calculations = history.GetLatestCalculations(); 
            return Results.Ok(calculations);
        });
    }
}
