using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class AboutUsController : Controller
	{
		readonly IOptionService _optionService;
		readonly IUnitOfWork _uow;

		public AboutUsController(IUnitOfWork uow, IOptionService optionService)
		{
			_uow = uow;
            _optionService = optionService;
		}

        // GET: Admin/AboutUs
        public virtual ActionResult Index()
		{
			return View(_optionService.GetAllAboutUs());
		}

        // Post: Admin/AboutUs
        [HttpPost]
		[ValidateAntiForgeryToken]
        public virtual ActionResult Index(AboutUs model)
		{
            _optionService.Update(model);
            _uow.SaveAllChanges();
			return RedirectToAction("Index");
		}
	}
}