using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Calculator; 
using Calculator.Services;



var builder = WebApplication.CreateBuilder(args);

// Add any services you need
builder.Services.AddSingleton<SimpleCalculator>();
builder.Services.AddSingleton<CachedCalculator>();
builder.Services.AddSingleton<HistoryService>();

var app = builder.Build();


app.MapCalculatorEndpoints();


app.Run();


public partial class Program { }
