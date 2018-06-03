using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using System.Web;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

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
			return View(_optionService.GetAllSliderImage());
		}

        // GET: Admin/AboutUs
        [HttpPost]
		[ValidateAntiForgeryToken]
        public virtual ActionResult Index(SliderViewModel model)
		{
            string imageText = Request["ImageName"].Replace("||/", "|/");
            string[] images = imageText.Split('|').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            string[] ImageNameDeleted = Request["ImageNameDeleted"].Split('|');

            if (images.Length >= 1) model.Image1 = images[0];
            else model.Image1 = "";
            if (images.Length >= 2) model.Image2 = images[1];
            else model.Image2 = "";
            if (images.Length >= 3) model.Image3 = images[2];
            else model.Image3 = "";

            _optionService.Update(model);
            _uow.SaveAllChanges();
            foreach (var item in ImageNameDeleted)
            {
                string fullPath = Request.MapPath(item);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            
            return RedirectToAction("Index");
		}

        [HttpPost]
        public virtual ActionResult FroalaUploadImage(HttpPostedFileBase file)
        {
                file = Request.Files["file[]"];
            if (file == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            var fileName = Path.GetFileName(file.FileName);
            var rootPath = Server.MapPath("~/Content/upload/images/");
            string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
            file.SaveAs(Path.Combine(rootPath, fileNameFinal));
            return Json(new
            { 
                initialPreview = new List<string> { "<img src='" + Url.Content("~/Content/upload/images/") + fileNameFinal + "' class='file-preview-image kv-preview-data' style='width: 210px;'>" },
                initialPreviewConfig = new List<image> { new image { key = fileNameFinal } }
                }, JsonRequestBehavior.AllowGet);
        }


    }
    public class image
    {
        public string key { get; set; }
    }
}