using Yeast.DomainClasses.Entities;
using Yeast.DomainClasses.EntityConfiguration;
using System.Data.Entity;

namespace Yeast.Datalayer.Context
{
	public class YeastDbContext : DbContext
	{
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Option> Options { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new CommentConfig());
			modelBuilder.Configurations.Add(new UserConfig());
			modelBuilder.Configurations.Add(new PostConfig());
			modelBuilder.Configurations.Add(new CategoryConfig());
			modelBuilder.Configurations.Add(new OptionConfig());

			base.OnModelCreating(modelBuilder);
		}

		#region IUnitOfWork Members
		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}
		#endregion
	}
}
