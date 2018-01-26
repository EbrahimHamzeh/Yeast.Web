using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Servicelayer.EFServices
{
	public class CustomRoleStore :
				RoleStore<CustomRole, int, CustomUserRole>,
				ICustomRoleStore
	{
		private readonly IUnitOfWork _context;

		public CustomRoleStore(IUnitOfWork context)
				: base((DbContext)context)
		{
			_context = context;
		}
	}
}
