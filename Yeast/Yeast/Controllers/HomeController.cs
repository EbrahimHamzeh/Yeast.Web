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
	
	public class HomeController : MyBaseController
	{

    private readonly IUnitOfWork _uow;
		private readonly ICategoryService _CategoryService;

		public HomeController(IUnitOfWork uow, ICategoryService categoryService)
		{
      _uow = uow;
			_CategoryService = categoryService;
		}

		// GET: Home
		public ActionResult Index()
		{
			var test = _CategoryService.Count;
			return View();
		}
	}
}