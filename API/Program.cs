using Calculator;
using Calculator.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddEnvironmentVariables();


string connString = builder.Configuration.GetConnectionString("DefaultConnection") //i know its not safe 
    ?? "Server=mariadb;Database=mariadatabase;User=secretuser;Password=secretpassword;";


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
