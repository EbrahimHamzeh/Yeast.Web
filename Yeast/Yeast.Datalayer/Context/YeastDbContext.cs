using Yeast.DomainClasses.Entities;
using Yeast.DomainClasses.EntityConfiguration;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using EFSecondLevelCache;
using System.Data.Entity.Infrastructure;
using Yeast.Utilities;

namespace Yeast.Datalayer.Context
{
	public class YeastDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
	{
		#region DbSet 
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<ServiceCategory> ServiceCategories { get; set; }
		public DbSet<Option> Options { get; set; }
		public DbSet<ContactUs> ContactUs { get; set; }
		public DbSet<Email> Email { get; set; }
		public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<RoleAccess> RoleAccesses { get; set; }
        public DbSet<Statistics> Statisticses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Representation> Representations { get; set; }

        public DbSet<Customer> Customers { get; set; }
        #endregion

        #region constucter and EF ModelCreating
        public YeastDbContext(): base("YeastDbContext"){}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Configurations.Add(new CommentConfig());
			modelBuilder.Configurations.Add(new UserConfig());
			modelBuilder.Configurations.Add(new TagConfig());
			modelBuilder.Configurations.Add(new ProductConfig());
			modelBuilder.Configurations.Add(new PostTranslateConfig());
			modelBuilder.Configurations.Add(new CategoryConfig());
			modelBuilder.Configurations.Add(new OptionConfig());
			modelBuilder.Configurations.Add(new RoleAccessConfig());

			modelBuilder.Entity<User>().ToTable("Users");
			modelBuilder.Entity<CustomRole>().ToTable("Roles");
			modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
			modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
			modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
		}
		#endregion

		#region IUnitOfWork Members
		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
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

			//By Defult Edit Ye Ke Arabi (Always)
			((IObjectContextAdapter)this).ObjectContext.ApplyCorrectYeKe();

			//Save EFSecondLevelCache
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
