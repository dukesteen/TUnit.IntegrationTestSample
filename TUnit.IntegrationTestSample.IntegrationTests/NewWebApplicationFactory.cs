using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Timespace.IntegrationTests.Setup.New;

namespace TUnit.IntegrationTestSample.IntegrationTests;

public sealed class NewWebApplicationFactory : WebApplicationFactory<Program>
{
	[ClassDataSource<PostgresWrapper>(Shared = SharedType.PerTestSession)]
	public required PostgresWrapper Postgres { get; init; }

	protected override IHost CreateHost(IHostBuilder builder)
	{
		_ = builder.UseEnvironment("Testing");

		_ = builder.ConfigureServices(services =>
		{
			
		});
		_ = builder.ConfigureHostConfiguration(cb => cb.AddInMemoryCollection(
				new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase)
				{
					["UseSecretsJson"] = bool.FalseString,
					["UseHttpsRedirection"] = bool.FalseString,
					["DbContextOptions:ConnectionString"] = Postgres.Container.GetConnectionString()
				}
			)
		);

		return base.CreateHost(builder);
	}

	// public async Task InitializeAsync()
	// {
	// 	using var scope = Services.CreateAsyncScope();
	// 	await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	//
	// 	_ = await dbContext.Database.EnsureCreatedAsync();
	//
	// 	await dbContext.Database.MigrateAsync();
	// }
}
