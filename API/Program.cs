using Calculator;
using Calculator.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpLogging;
using Calculator.API.Services;


var builder = WebApplication.CreateBuilder(args);



builder.Configuration.AddEnvironmentVariables();  

var dbHost     = Environment.GetEnvironmentVariable("MYSQL_HOST");
var dbName     = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
var dbUser     = Environment.GetEnvironmentVariable("MYSQL_USER");
var dbPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");


var connString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};";

builder.Services.AddSingleton<IDatabaseClient>(
    _ => new MySqlDatabaseClient(connString)
);

builder.Services.AddSingleton<HistoryService>();
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



var app = builder.Build();


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