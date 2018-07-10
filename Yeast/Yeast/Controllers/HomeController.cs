using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;
using Yeast.Model.FrontEnd;
using Yeast.Model.Admin;
using Yeast.Model.Admin.Setting;
using System.Collections.Generic;

namespace Yeast.Controllers
{

	public partial class HomeController : BaseController
	{

		readonly IApplicationUserManager _userManager;
        readonly IProductService _productService;
        readonly IOptionService _optionService;
        readonly IPostService _postService;
        readonly IServiceService _serviceService;
        readonly IUnitOfWork _uow;

		public HomeController(IUnitOfWork uow, IProductService productService, IApplicationUserManager userManager, IOptionService optionService, IPostService postService,IServiceService serviceService)
		{
			_uow = uow;
            _productService = productService;
			_userManager = userManager;
            _optionService = optionService;
            _postService = postService;
            _serviceService = serviceService;
        }

		// GET: Home
		public ActionResult Index()
		{
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ProductList= _productService.GetAllproduct();
            homeViewModel.postlist = _postService.GetByCulterPost();
            homeViewModel.homeModel = _optionService.GetHome();
            return View(homeViewModel);
		}
        [ChildActionOnly]
        public virtual ActionResult footer()
        {
            FooterModel footerModel = new FooterModel();
            footerModel.aboutUsViewModel= _optionService.GetByCulterAboutUsV();
            footerModel.settingViewModelCultur = _optionService.GetAllSettingByCultur();
            footerModel.postModeList = _postService.GetByCulterPost();
            footerModel.serviceModelList = _serviceService.GetByCulterPost();

            return PartialView("footer", footerModel);
        }

    }
}