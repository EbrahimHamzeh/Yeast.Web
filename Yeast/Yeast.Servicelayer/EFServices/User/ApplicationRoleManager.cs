using EFSecondLevelCache;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.BootstrapTable;
using Yeast.Utilities.Helpers;

namespace Yeast.Servicelayer.EFServices
{
	public class ApplicationRoleManager :
				RoleManager<CustomRole, int>,
				IApplicationRoleManager
	{
		private readonly IUnitOfWork _uow;
		private readonly ICustomRoleStore _roleStore;
		private readonly IDbSet<User> _users;
		public ApplicationRoleManager(
				IUnitOfWork uow,
				ICustomRoleStore roleStore)
				: base((IRoleStore<CustomRole, int>)roleStore)
		{
			_uow = uow;
			_roleStore = roleStore;
			_users = _uow.Set<User>();
		}

		public CustomRole FindRoleByName(string roleName)
		{
			return this.FindByName(roleName); // RoleManagerExtensions
		}

		public IdentityResult CreateRole(CustomRole role)
		{

            return this.Create(role); // RoleManagerExtensions
		}

		public IList<CustomUserRole> GetCustomUsersInRole(string roleName)
		{
			return this.Roles.Where(role => role.Name == roleName)
											 .SelectMany(role => role.Users)
											 .ToList();
			// = this.FindByName(roleName).Users
		}

		public IList<User> GetApplicationUsersInRole(string roleName)
		{
			var roleUserIdsQuery = from role in this.Roles
														 where role.Name == roleName
														 from user in role.Users
														 select user.UserId;
			return _users.Where(applicationUser => roleUserIdsQuery.Contains(applicationUser.Id))
									 .ToList();
		}

		public IList<CustomRole> FindUserRoles(int userId)
		{
			var userRolesQuery = from role in this.Roles
													 from user in role.Users
													 where user.UserId == userId
													 select role;

			return userRolesQuery.OrderBy(x => x.Name).ToList();
		}

		public string[] GetRolesForUser(int userId)
		{
			var roles = FindUserRoles(userId);
			if (roles == null || !roles.Any())
			{
				return new string[] { };
			}

			return roles.Select(x => x.Name).ToArray();
		}

		public bool IsUserInRole(int userId, string roleName)
		{
			var userRolesQuery = from role in this.Roles
													 where role.Name == roleName
													 from user in role.Users
													 where user.UserId == userId
													 select role;
			var userRole = userRolesQuery.FirstOrDefault();
			return userRole != null;
		}

		public Task<List<CustomRole>> GetAllCustomRolesAsync()
		{
			return this.Roles.ToListAsync();
		}

        public async Task<DataTableList<RoleAccessList>> GetDataTableAsync(PagedQueryViewModel model)
        {
            IQueryable<CustomRole> customRoleList = _roleStore.Roles.AsNoTracking();
            int total = 0;

            // Search
            //tagList = tagList.ApplySearch(model);

            total = await customRoleList.CountAsync().ConfigureAwait(false);

            // Ordering data 
            //customRoleList = customRoleList.ApplyOrdering(model);

            // Paging And Save Cach
            //customRoleList = customRoleList.ApplyPaging(model).Cacheable();
            model.offset = model.offset - 1;
            // Create List Of viewModel
            var category = (await customRoleList.ToListAsync()).Select((x, index) => new RoleAccessList
            {
                No = (++index + model.offset).ConvertToPersianString(),
                Id = x.Id,
                Title = x.Title,
                Description = x.Description
            });

            return new DataTableList<RoleAccessList> { rows = category.ToList(), total = total };
        }
    }
}
