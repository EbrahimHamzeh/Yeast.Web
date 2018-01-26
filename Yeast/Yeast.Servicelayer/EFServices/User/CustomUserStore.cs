using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Servicelayer.EFServices
{
	public class CustomUserStore :
				UserStore<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>,
				ICustomUserStore
	{
		private readonly IUnitOfWork _context;

		public CustomUserStore(IUnitOfWork context)
				: base((YeastDbContext)context)
		{
			_context = context;
		}

	}
}
