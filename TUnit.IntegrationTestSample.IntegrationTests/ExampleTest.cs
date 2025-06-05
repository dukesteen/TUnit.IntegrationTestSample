using Shouldly;
using TUnit.IntegrationTestSample.Endpoints;

namespace TUnit.IntegrationTestSample.IntegrationTests;

[MyDataProvider]
public class ExampleTest(TestEndpoint.Handler testEndpointHandler)
{
	[Test]
	public async Task Test()
	{
		var res = await testEndpointHandler.HandleAsync(new TestEndpoint.Command()
		{
			Age = 10,
			Name = "Test"
		});

		res.Test.ShouldBe(true);
	}
}