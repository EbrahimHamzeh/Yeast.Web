using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;
using Yeast.Model.FrontEnd;

namespace Yeast.Controllers
{

	public partial class HomeController : BaseController
	{

		readonly IApplicationUserManager _userManager;
        readonly IProductService _productService;
        readonly IOptionService _optionService;
        readonly IUnitOfWork _uow;

		public HomeController(IUnitOfWork uow, IProductService productService, IApplicationUserManager userManager, IOptionService optionService)
		{
			_uow = uow;
            _productService = productService;
			_userManager = userManager;
            _optionService = optionService;
        }

		// GET: Home
		public ActionResult Index()
		{
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ProductList= _productService.GetAllproduct();
            homeViewModel.homeModel = _optionService.GetHome();
            return View(homeViewModel);
		}
	}
}