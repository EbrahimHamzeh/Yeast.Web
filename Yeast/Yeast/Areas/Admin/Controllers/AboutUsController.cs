using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Attribute;
using Yeast.Utilities.BootstrapTable;
using System.Web;
using System.IO;
using System.Linq;
using System;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class AboutUsController : Controller
	{
		readonly IOptionService _optionService;
		readonly IUnitOfWork _uow;

		public AboutUsController(IUnitOfWork uow, IOptionService optionService, ITagService tagService, ICategoryService categotyService)
		{
			_uow = uow;
            _optionService = optionService;
		}

        // GET: Admin/AboutUs
        public virtual ActionResult Index()
		{
			return View(_optionService.GetAllAboutUs());
		}

        // GET: Admin/AboutUs
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