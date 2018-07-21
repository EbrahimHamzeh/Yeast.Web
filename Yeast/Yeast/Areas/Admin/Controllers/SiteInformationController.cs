using System.Web.Mvc;
using Yeast.Attribute;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Areas.Admin.Controllers
{
	[CustomAuthorize]
    public class SiteInformationController : Controller
	{
        readonly IOptionService _optionService;
        readonly IUnitOfWork _uow;

        public SiteInformationController(IUnitOfWork uow, IOptionService optionService)
        {
            _uow = uow;
            _optionService = optionService;
        }

		// GET: Admin/SiteInformation
		public virtual ActionResult Index()
		{
			return View(_optionService.GetAllSetting());
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Post: Admin/SiteInformation
        public virtual ActionResult Index(SettingViewModel model)
        {
            _optionService.Update(model);
            _uow.SaveAllChanges();
            return RedirectToAction("Index");
        }
    }
}