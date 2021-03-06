﻿using EFSecondLevelCache;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.BootstrapTable;
using Yeast.Utilities.Helpers;

namespace Yeast.Servicelayer.EFServices
{
	public class ApplicationUserManager
				: UserManager<User, int>,
				IApplicationUserManager
	{
		private readonly IDataProtectionProvider _dataProtectionProvider;
		private readonly IApplicationRoleManager _roleManager;
		private readonly ICustomUserStore _store;
		private readonly IUnitOfWork _uow;
		private readonly IDbSet<User> _users;
		private readonly Lazy<Func<IIdentity>> _identity;
		private User _user;

		public ApplicationUserManager(
				ICustomUserStore store,
				IUnitOfWork uow,
				Lazy<Func<IIdentity>> identity, // For lazy loading -> Controller gets constructed before the HttpContext has been set by ASP.NET.
				IApplicationRoleManager roleManager,
				IDataProtectionProvider dataProtectionProvider,
				IIdentityMessageService smsService,
				IIdentityMessageService emailService)
				: base((IUserStore<User, int>)store)
		{
			_store = store;
			_uow = uow;
			_identity = identity;
			_users = _uow.Set<User>();
			_roleManager = roleManager;
			_dataProtectionProvider = dataProtectionProvider;
			this.SmsService = smsService;
			this.EmailService = emailService;

			createApplicationUserManager();
		}

		public User FindById(int userId)
		{
			return _users.Find(userId);
		}
        public bool UpdateAsync(User user)
        {
            User updateuser= _users.Find(user.Id);
            updateuser.FirstName = user.FirstName;
            updateuser.LastName = user.LastName;
            updateuser.Password = user.Password;
            updateuser.AvatarPath = user.AvatarPath;
            updateuser.UserName = user.UserName;
            updateuser.Email = user.Email;
            updateuser.Description = user.Description;
            return true;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(User applicationUser)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);

			// Add custom user claims here
			userIdentity.AddClaim(new Claim("user-email", applicationUser.Email));

			return userIdentity;
		}

		public Task<List<User>> GetAllUsersAsync()
		{
			return this.Users.ToListAsync();
		}

		public User GetCurrentUser()
		{
			return _user ?? (_user = this.FindById(GetCurrentUserId()));
		}

		public async Task<User> GetCurrentUserAsync()
		{
			return _user ?? (_user = await this.FindByIdAsync(GetCurrentUserId()).ConfigureAwait(false));
		}

		public int GetCurrentUserId()
		{
			return _identity.Value().GetUserId<int>();
		}

		public async Task<bool> HasPassword(int userId)
		{
			var user = await FindByIdAsync(userId).ConfigureAwait(false);
			return user != null && user.PasswordHash != null;
		}

		public async Task<bool> HasPhoneNumber(int userId)
		{
			var user = await FindByIdAsync(userId).ConfigureAwait(false);
			return user != null && user.PhoneNumber != null;
		}

		public Func<CookieValidateIdentityContext, Task> OnValidateIdentity()
		{
			return SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User, int>(
									 validateInterval: TimeSpan.FromSeconds(0),
									 regenerateIdentityCallback: (manager, user) => manager.GenerateUserIdentityAsync(user),
									 getUserIdCallback: claimsIdentity => claimsIdentity.GetUserId<int>());
		}

		public void SeedDatabase()
		{
			const string name = "admin@example.com";
			const string password = "Admin@123456";
			const string roleName = "Admin";

			//Create Role Admin if it does not exist
			var role = _roleManager.FindRoleByName(roleName);
			if (role == null)
			{
				role = new CustomRole(roleName);
				var roleResult = _roleManager.CreateRole(role);
				if (!roleResult.Succeeded)
				{
					throw new InvalidOperationException(string.Join(", ", roleResult.Errors));
				}
			}
			try
			{
				var user = this.FindByName(name);
				if (user == null)
				{
					user = new User { UserName = name, Email = name };
					var createResult = this.Create(user, password);
					if (!createResult.Succeeded)
					{
						throw new InvalidOperationException(string.Join(", ", createResult.Errors));
					}

					var setLockoutResult = this.SetLockoutEnabled(user.Id, false);
					if (!setLockoutResult.Succeeded)
					{
						throw new InvalidOperationException(string.Join(", ", setLockoutResult.Errors));
					}
				}

				// Add user admin to Role Admin if not already added
				var rolesForUser = this.GetRoles(user.Id);
				if (!rolesForUser.Contains(role.Name))
				{
					var addToRoleResult = this.AddToRole(user.Id, role.Name);
					if (!addToRoleResult.Succeeded)
					{
						throw new InvalidOperationException(string.Join(", ", addToRoleResult.Errors));
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
			
		}

		private void createApplicationUserManager()
		{
			// Configure validation logic for usernames
			this.UserValidator = new UserValidator<User, int>(this)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configure validation logic for passwords
			this.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = false,
				RequireDigit = false,
				RequireLowercase = false,
				RequireUppercase = false,
			};

			// Configure user lockout defaults
			this.UserLockoutEnabledByDefault = true;
			this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			this.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug in here.
			this.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User, int>
			{
				MessageFormat = "Your security code is: {0}"
			});
			this.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User, int>
			{
				Subject = "SecurityCode",
				BodyFormat = "Your security code is {0}"
			});

			if (_dataProtectionProvider != null)
			{
				var dataProtector = _dataProtectionProvider.Create("ASP.NET Identity");
				this.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtector);
			}
		}

        public async Task<DataTableList<UserList>> GetDataTableAsync(PagedQueryViewModel model)
        {
            IQueryable<User> userList = _users.AsNoTracking();
            int total = 0;

            // Search
            //tagList = tagList.ApplySearch(model);

            total = await userList.CountAsync().ConfigureAwait(false);

            // Ordering data 
            userList = userList.ApplyOrdering(model);

            // Paging And Save Cach
            userList = userList.ApplyPaging(model).Cacheable();
            model.offset = model.offset - 1;
            // Create List Of viewModel
            var users = (await userList.ToListAsync()).Select((x, index) => new UserList
            {
                No = (++index + model.offset).ConvertToPersianString(),
                Id = x.Id,
                Title = x.UserName,
                Description = x.Description,
            });

            return new DataTableList<UserList> { rows = users.ToList(), total = total };
        }

        public SelectList DropDownList()
        {
            List<SelectListItem> selectListItemList;
            selectListItemList = _users.Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = x.Id.ToString() }).ToList();
            return new SelectList(selectListItemList, "Value", "Text");
        }
    }
}
