using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public partial class HomeController : BaseController
	{

		readonly IApplicationUserManager _userManager;
        readonly IProductService _productService;
        readonly IUnitOfWork _uow;

		public HomeController(IUnitOfWork uow, IProductService productService, IApplicationUserManager userManager)
		{
			_uow = uow;
            _productService = productService;
			_userManager = userManager;
		}

		// GET: Home
		public ActionResult Index()
		{
            var list=_productService.GetAllproduct();
            return View(list);
		}
	}
}