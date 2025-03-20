using Calculator;
using Calculator.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();


string connString = builder.Configuration.GetConnectionString("DefaultConnection") //i know its not safe 
    ?? "Server=mariadb;Database=mariadatabase;User=secretuser;Password=secretpassword;";


builder.Services.AddSingleton(new HistoryService(connString));
builder.Services.AddSingleton<CachedCalculator>();
builder.Services.AddSingleton<SimpleCalculator>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocal3000",
        policyBuilder =>
        {
            policyBuilder
                .WithOrigins("http://localhost:3000")
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
}


app.MapCalculatorEndpoints();

app.Run();
