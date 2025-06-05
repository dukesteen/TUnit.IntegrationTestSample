using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TUnit.Core.Interfaces;

namespace TUnit.IntegrationTestSample.IntegrationTests;

[method: SetsRequiredMembers]
public sealed class MyDataProviderAttribute() : DependencyInjectionDataSourceAttribute<IServiceScope>, IAsyncInitializer
{
	private AsyncServiceScope _scope;

	[ClassDataSource<NewWebApplicationFactory>(Shared = SharedType.PerTestSession)]
	public required NewWebApplicationFactory WebApplicationFactory { get; set; } = null!;

	public override IServiceScope CreateScope(DataGeneratorMetadata dataGeneratorMetadata)
	{
		return _scope = WebApplicationFactory.Services.CreateAsyncScope();
	}

	public override object? Create(IServiceScope scope, Type type)
	{
		return scope.ServiceProvider.GetService(type);
	}

	// This will be called on every test that it's used on.
	// If this should only happen once, move this initialization code into the WebAppFactory initialization
	// which should happen only once per test session because we're using Shared = SharedType.PerTestSession.
	public async Task InitializeAsync()
	{
		await using var dbContext = _scope.ServiceProvider.GetRequiredService<AppDbContext>();

		_ = await dbContext.Database.EnsureCreatedAsync();

		await dbContext.Database.MigrateAsync();
	}
}
