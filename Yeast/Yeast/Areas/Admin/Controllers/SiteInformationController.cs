using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Areas.Admin.Controllers
{
    public partial class SiteInformationController : Controller
	{
        //readonly ISiteInformationService _postService;
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