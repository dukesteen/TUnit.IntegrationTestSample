using Testcontainers.PostgreSql;
using TUnit.Core.Interfaces;

namespace Timespace.IntegrationTests.Setup.New;

public class PostgresWrapper : IAsyncInitializer, IAsyncDisposable
{
	public PostgreSqlContainer Container { get; } = new PostgreSqlBuilder().Build();

	public Task InitializeAsync()
	{
		return Container.StartAsync();
	}

	public async ValueTask DisposeAsync()
	{
		await Container.DisposeAsync();
	}
}
