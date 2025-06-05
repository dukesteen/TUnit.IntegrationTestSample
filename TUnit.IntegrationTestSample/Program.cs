using Microsoft.EntityFrameworkCore;
using Npgsql;
using TUnit.IntegrationTestSample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration["DbContextOptions:ConnectionString"]);
_ = dataSourceBuilder.EnableDynamicJson();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(c =>
{
	c.UseNpgsql(dataSource);
});
builder.Services.AddTUnitIntegrationTestSampleHandlers();
builder.Services.AddTUnitIntegrationTestSampleBehaviors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

_ = app.UseEndpoints(endpoints =>
	{
		_ = endpoints.MapTUnitIntegrationTestSampleEndpoints();
	}
);

app.Run();

public sealed partial class Program;