using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin.Setting;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

    public partial class ContactUsController : BaseController
    {

        readonly IContactUsService _contactUsService;
        readonly IUnitOfWork _uow;

        public ContactUsController(IUnitOfWork uow, IContactUsService contactUsService)
        {
            _uow = uow;
            _contactUsService = contactUsService;
        }

        // GET: ContactUs
        public virtual ActionResult Index()
        {
            ContactUsViewModel contactUsViewModel = new ContactUsViewModel();
            ViewBag.State = false;
            return View(contactUsViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        // Post: ContactUs
        public virtual ActionResult Index(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.State = false;
                return View(model);
            }

            _contactUsService.Add(model);
            _uow.SaveAllChanges();
            ViewBag.State = true;
            return View(model);
        }
    }
}