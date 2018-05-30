using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yeast.Attribute;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class ProductController : Controller
	{
		readonly IProductService _ProductService;
		readonly IUnitOfWork _uow;

		public ProductController(IUnitOfWork uow, IProductService ProductService)
		{
			_uow = uow;
			_ProductService = ProductService;
		}

		// GET: Admin/Product
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		public virtual async Task<ActionResult> DataList(string search, string sort = "Name", string order = "asc", int offset = 0, int limit = 10)
		{
			return Json(await _ProductService.GetDataTableAsync(search, sort, order, offset, limit).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Product/Add
		public virtual ActionResult Add()
		{
			ProductAdd Product = new ProductAdd();
			return View(Product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Product/Add/id
		public virtual ActionResult Add(ProductAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
            
            _ProductService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/Product/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			ProductEdit Product = _ProductService.FindForEdit(id ?? 0);
			if (Product == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(Product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Product/Edit/id
		public virtual ActionResult Edit(int id)
		{
			Product ProductUpdate = _ProductService.Find(id);
			if (ProductUpdate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (TryUpdateModel(ProductUpdate, "", new string[] { "Name", "Description", "Price", "Body"}))
			{
                string imageText = Request["ImageName"].Replace("||/","|");
                string[] images = imageText.Split('|').Where(x => !string.IsNullOrEmpty(x)).ToArray();

                if (images.Length >= 1) ProductUpdate.Image1 = images[0];
                else ProductUpdate.Image1 = "";
                if (images.Length >= 2) ProductUpdate.Image2 = images[1];
                else ProductUpdate.Image2 = "";
                if (images.Length >= 3) ProductUpdate.Image3 = images[2];
                else ProductUpdate.Image3 = "";

                if (!ModelState.IsValid)
				{
					return View(ProductUpdate);
				}
				_uow.SaveAllChanges();

                string[] ImageNameDeleted = Request["ImageNameDeleted"].Split('|');
                foreach (var item in ImageNameDeleted)
                {
                    string fullPath = Request.MapPath(item);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }
            return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Product/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Product Product = _ProductService.Find(id);
			if (Product == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_ProductService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
        }

        [HttpPost]
        public virtual ActionResult Filepond(HttpPostedFileBase Image1)
        {
            var fileName = Path.GetFileName(Image1.FileName);
            var rootPath = Server.MapPath("~/Content/upload/images/");
            string fileNameFinal = Guid.NewGuid().ToString("N") + fileName;
            Image1.SaveAs(Path.Combine(rootPath, fileNameFinal));
            return Json(new { link = Url.Content("~/Content/upload/images/") + fileNameFinal }, JsonRequestBehavior.AllowGet);
        }
    }
}