using BuildingBlocks.Shared.Configs;
using HealthCare.API.Middlewares;
using HealthCare.Application;
using HealthCare.Domain.Configs;
using HealthCare.Infrastructure;
using Newtonsoft.Json;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Load JSON file
var appConfigJsonString = File.ReadAllText("appconfigs.json");
var databaseJsonString = File.ReadAllText("databaseconfig.json");

AppConfig? appConfig = JsonConvert.DeserializeObject<AppConfig>(appConfigJsonString);
DatabaseConfig? databaseConfig = JsonConvert.DeserializeObject<DatabaseConfig>(databaseJsonString);

builder.Services.AddSingleton(appConfig ?? throw new InvalidOperationException("App configuration is missing or invalid."));
builder.Services.AddSingleton(databaseConfig ?? throw new InvalidOperationException("Database configuration is missing or invalid."));
#endregion

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

builder.Services.RegisterInfrastructure(databaseConfig);
builder.Services.RegisterApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/openapi/v1.json");

    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("HealthCare API")
            .WithTheme(ScalarTheme.DeepSpace);
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
