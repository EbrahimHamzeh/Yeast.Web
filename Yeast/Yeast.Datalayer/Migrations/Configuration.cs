using System.Data.Entity.Migrations;
using Yeast.Datalayer.Context;

namespace Yeast.Datalayer.Migrations
{
	public sealed class Configuration : DbMigrationsConfiguration<YeastDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}
		protected override void Seed(YeastDbContext context)
		{

		}
	}
}