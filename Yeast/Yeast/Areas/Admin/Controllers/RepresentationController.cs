using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Attribute;
using Yeast.Utilities.BootstrapTable;

namespace Yeast.Areas.Admin.Controllers
{
	public class RepresentationController : Controller
	{
		readonly IRepresentationService _representationService;
		readonly IUnitOfWork _uow;

		public RepresentationController(IUnitOfWork uow, IRepresentationService representationService)
		{
			_uow = uow;
			_representationService = representationService;
		}

		// GET: Admin/Category
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _representationService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Category/Add
		public virtual ActionResult Add()
		{
            RepresentationAdd model = new RepresentationAdd();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Category/Add/id
		public virtual ActionResult Add(RepresentationAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_representationService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/Category/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            RepresentationEdit representation = _representationService.FindForEdit(id ?? 0);
			if (representation == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(representation);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Category/Edit/id
		public virtual ActionResult Edit(int id)
		{
            Representation representationUpdate = _representationService.Find(id);
			if (representationUpdate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (TryUpdateModel(representationUpdate, "", new string[] { "Title", "Description", "Latitude", "Longitude", "Telephone" }))
			{
				if (!ModelState.IsValid)
				{
					return View(representationUpdate);
				}
				_uow.SaveAllChanges();
			}
			return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Category/Delete/id
		public virtual ActionResult Delete(int id)
		{
            Representation representation = _representationService.Find(id);
			if (representation == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_representationService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}