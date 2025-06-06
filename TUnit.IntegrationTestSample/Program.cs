using Microsoft.EntityFrameworkCore;
using Npgsql;
using TUnit.IntegrationTestSample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration["DbContextOptions:ConnectionString"]);
// _ = dataSourceBuilder.EnableDynamicJson();
// var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(c =>
{
	c.UseSqlite("Data Source=db.sql");
});
builder.Services.AddTUnitIntegrationTestSampleHandlers();
builder.Services.AddTUnitIntegrationTestSampleBehaviors();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();
_ = app.UseEndpoints(endpoints =>
	{
		_ = endpoints.MapTUnitIntegrationTestSampleEndpoints();
	}
);

app.Run();

public sealed partial class Program;