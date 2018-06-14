using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public partial class AccountController : BaseController
	{

		readonly IOptionService _optionService;
		readonly IUnitOfWork _uow;

		public AccountController(IUnitOfWork uow, IOptionService optionService)
		{
			_uow = uow;
            _optionService = optionService;
		}

        // GET: Account
        public virtual ActionResult Index()
		{
            return View();
		}

        // GET: Account/Login
        public virtual ActionResult Login()
		{
            return View();
		}
	}
}