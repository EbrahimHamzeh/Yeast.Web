using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

    public partial class AccountController : Controller
    {

        readonly IOptionService _optionService;
        readonly IUnitOfWork _uow;
        readonly IApplicationSignInManager _applicationSignInManager;

        public AccountController(IUnitOfWork uow, IOptionService optionService, IApplicationSignInManager applicationSignInManager)
        {
            _uow = uow;
            _optionService = optionService;
            _applicationSignInManager = applicationSignInManager;
        }

        [HttpGet]
        // GET: Account
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // GET: Account/Login
        public virtual ActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        // GET: Account/Login
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _applicationSignInManager.PasswordSignInAsync(model.Username, model.Password, model.RemmemberMe, false).Result;

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
                    return View(model);
            }
        }

        [HttpGet]
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}