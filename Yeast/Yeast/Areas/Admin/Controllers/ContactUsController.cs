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
	public partial class ContactUsController : Controller
	{
		readonly IContactUsService _contactUservice;
		readonly IUnitOfWork _uow;

		public ContactUsController(IUnitOfWork uow, IContactUsService contactUservice)
		{
			_uow = uow;
            _contactUservice = contactUservice;
		}

        // GET: Admin/ContacyUsC
        public virtual ActionResult Index()
		{
			return View();
		}

        // Get: Ajax Admin/ContacyUsC/DataList
        [HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _contactUservice.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/ContacyUsC/Add
		public virtual ActionResult Add()
		{
			TagAdd Tag = new TagAdd();
			return View(Tag);
		}


		// GET: Admin/ContacyUsC/View
		public virtual ActionResult View(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			ContactUs customer = _contactUservice.Find(id ?? 0);
			if (customer == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Tag/Delete/id
		public virtual ActionResult Delete(int id)
		{
            ContactUs contactUs = _contactUservice.Find(id);
			if (contactUs == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _contactUservice.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}