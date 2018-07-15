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
	public partial class CustomerController : Controller
	{
		readonly ICustomerService _customerService;
		readonly IUnitOfWork _uow;

		public CustomerController(IUnitOfWork uow, ICustomerService customerService)
		{
			_uow = uow;
			_customerService = customerService;
		}

		// GET: Admin/Customer
		public virtual ActionResult Index()
		{
			return View();
		}

		// Get: Ajax Admin/DataList
		[HttpGet, AjaxOnly, NoOutputCache]
		public virtual async Task<ActionResult> DataList(PagedQueryViewModel model)
		{
			return Json(await _customerService.GetDataTableAsync(model).ConfigureAwait(false), JsonRequestBehavior.AllowGet);
		}

		// GET: Admin/Customer/Add
		public virtual ActionResult Add()
		{
			CustomerAdd Customer = new CustomerAdd();
			return View(Customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Customer/Add/id
		public virtual ActionResult Add(CustomerAdd model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			_customerService.Add(model);
			_uow.SaveAllChanges();
			return RedirectToAction("Index");
		}

		// GET: Admin/Customer/Edit
		public virtual ActionResult Edit(int? id)
		{
			if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			CustomerEdit customer = _customerService.FindForEdit(id ?? 0);
			if (customer == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Customer/Edit/id
		public virtual ActionResult Edit(int id)
		{
			Customer customerUpdate = _customerService.Find(id);
			if (customerUpdate == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (TryUpdateModel(customerUpdate, "", new string[] { "Title", "Description", "Name", "Family", "Email", "Mobile", "Telephone" }))
			{
				if (!ModelState.IsValid)
				{
					return View(customerUpdate);
				}
				_uow.SaveAllChanges();
			}
			return RedirectToAction("Index"); ;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// Post: Admin/Customer/Delete/id
		public virtual ActionResult Delete(int id)
		{
			Customer customer = _customerService.Find(id);
			if (customer == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			_customerService.Remove(id);
			_uow.SaveAllChanges();
			return Json(new { success = true });
		}
	}
}