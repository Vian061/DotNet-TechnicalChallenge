using BuildingBlocks.Shared.Configs;
using HealthCare.Application;
using HealthCare.Infrastructure;
using Newtonsoft.Json;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Load JSON file
var databaseJsonString = File.ReadAllText("databaseconfig.json");

DatabaseConfig? databaseConfig = JsonConvert.DeserializeObject<DatabaseConfig>(databaseJsonString);

builder.Services.AddSingleton(databaseConfig ?? throw new InvalidOperationException("Database configuration is missing or invalid."));
#endregion

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
	// Specify the OpenAPI version to use
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
