using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public partial class HomeController : BaseController
	{

		readonly IApplicationUserManager _userManager;
		readonly ICategoryService _categoryService;
		readonly IUnitOfWork _uow;

		public HomeController(IUnitOfWork uow, ICategoryService categoryService, IApplicationUserManager userManager)
		{
			_uow = uow;
			_categoryService = categoryService;
			_userManager = userManager;
		}

		// GET: Home
		public virtual ActionResult Index()
		{
			var test = _categoryService.Count;
			return View();
		}
	}
}