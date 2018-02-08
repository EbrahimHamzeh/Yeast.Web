using Yeast.DomainClasses.Entities;
using Yeast.DomainClasses.EntityConfiguration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using EFSecondLevelCache;

namespace Yeast.Datalayer.Context
{
	public class YeastDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
	{
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Option> Options { get; set; }

		public YeastDbContext(): base("YeastDbContext"){}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new CommentConfig());
			modelBuilder.Configurations.Add(new UserConfig());
			modelBuilder.Configurations.Add(new PostConfig());
			modelBuilder.Configurations.Add(new CategoryConfig());
			modelBuilder.Configurations.Add(new OptionConfig());


			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<CustomRole>().ToTable("Roles");
			modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
			modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
			modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
		}

		#region IUnitOfWork Members
		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}
		public int SaveAllChanges()
		{
			return base.SaveChanges();
		}

		public IEnumerable<TEntity> AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
		{
			return ((DbSet<TEntity>)this.Set<TEntity>()).AddRange(entities);
		}

		public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
		{
			Entry(entity).State = EntityState.Modified;
		}

		public IList<T> GetRows<T>(string sql, params object[] parameters) where T : class
		{
			return Database.SqlQuery<T>(sql, parameters).ToList();
		}

		public void ForceDatabaseInitialize()
		{
			this.Database.Initialize(force: true);
		}
		#endregion

		#region EFSecondLevelCache
		public override int SaveChanges()
		{
			return SaveAllChanges(invalidateCacheDependencies: true);
		}

		public int SaveAllChanges(bool invalidateCacheDependencies = true)
		{
			var changedEntityNames = getChangedEntityNames();
			var result = base.SaveChanges();
			if (invalidateCacheDependencies)
			{
				new EFCacheServiceProvider().InvalidateCacheDependencies(changedEntityNames);
			}
			return result;
		}
		private string[] getChangedEntityNames()
		{
			// Updated version of this method: \EFSecondLevelCache\EFSecondLevelCache.Tests\EFSecondLevelCache.TestDataLayer\DataLayer\SampleContext.cs
			return this.ChangeTracker.Entries()
					.Where(x => x.State == EntityState.Added ||
											x.State == EntityState.Modified ||
											x.State == EntityState.Deleted)
					.Select(x => System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(x.Entity.GetType()).FullName)
					.Distinct()
					.ToArray();
		}
		#endregion
	}
}
