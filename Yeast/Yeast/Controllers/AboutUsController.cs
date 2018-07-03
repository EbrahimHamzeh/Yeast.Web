using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin.Setting;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public partial class AboutUsController : BaseController
	{

		readonly IOptionService _optionService;
		readonly IUnitOfWork _uow;

		public AboutUsController(IUnitOfWork uow, IOptionService optionService)
		{
			_uow = uow;
            _optionService = optionService;
		}

		// GET: Home
		public virtual ActionResult Index()
		{
            AboutUsModel aboutUsModel = new AboutUsModel();
            aboutUsModel.aboutUsViewModel = _optionService.GetByCulterAboutUsV();
            aboutUsModel.settingViewModelCultur = _optionService.GetAllSettingByCultur();
            return View(aboutUsModel);
		}
	}
}