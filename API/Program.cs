using Calculator;
using Calculator.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddEnvironmentVariables();  

var dbHost     = Environment.GetEnvironmentVariable("DB_HOST");
var dbName     = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser     = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};";

builder.Services.AddSingleton(new HistoryService(connString));
builder.Services.AddSingleton<CachedCalculator>();
builder.Services.AddSingleton<SimpleCalculator>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin() 
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();
app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); //Logging
}

app.UseCors("AllowAll");

app.MapCalculatorEndpoints();

app.Run();
public partial class Program { } 