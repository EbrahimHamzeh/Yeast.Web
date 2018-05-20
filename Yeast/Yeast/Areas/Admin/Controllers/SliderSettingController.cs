using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using System.Web;
using System.IO;
using System;
using System.Collections.Generic;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class SliderSettingController : Controller
	{
		readonly IOptionService _optionService;
		readonly IUnitOfWork _uow;

		public SliderSettingController(IUnitOfWork uow, IOptionService optionService, ITagService tagService, ICategoryService categotyService)
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

        [HttpPost]
        public virtual ActionResult FroalaUploadImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                file = Request.Files["file[]"];
            }
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath("~/Content/upload/images/");
            string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
            file.SaveAs(Path.Combine(rootPath, fileNameFinal));
            return Json(new { error="", initialPreview = new List<string> { "<img src='" + Url.Content("~/Content/upload/images/") + fileNameFinal + "' class='file-preview-image' alt='Desert' title='Desert'>" },
                initialPreviewConfig = new { caption = "desert.jpg", width = "120px", url = "http://localhost/avatar/delete", key = "100", extra = new { id = 100}} }, JsonRequestBehavior.AllowGet);
        }
    }
}