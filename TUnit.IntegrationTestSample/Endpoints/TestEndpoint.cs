using Immediate.Apis.Shared;
using Immediate.Handlers.Shared;

namespace TUnit.IntegrationTestSample.Endpoints;

[Handler]
[MapPost("/api/test")]
public static partial class TestEndpoint
{
	public record Command
	{
		public required string Name { get; init; }
		public required int Age { get; init; }
	}

	public record Response
	{
		public required bool Test { get; init; }
	}

	private static async ValueTask<Response> HandleAsync(Command command, AppDbContext db, CancellationToken token)
	{
		return new Response()
		{
			Test = true
		};
	}
}
