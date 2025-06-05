using Microsoft.EntityFrameworkCore;

namespace TUnit.IntegrationTestSample;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
	}
}