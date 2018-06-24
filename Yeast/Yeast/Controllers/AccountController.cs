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

        [HttpPost]
        // GET: Account/Login
        public virtual ActionResult Login()
		{
            if (!ModelState.IsValid)
            {
                return View(User);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(User.UserName, User.Password, true, shouldLockout: false); // TODO: RememberMe
            switch (result)
            {
                case SignInStatus.Success:
                    return returnUrl == null ? RedirectToAction("Index", "Dashboard", new { area = "Admin" }) : RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false }); // TODO: User.RememberMe
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(User);
            }
		}
	}
}