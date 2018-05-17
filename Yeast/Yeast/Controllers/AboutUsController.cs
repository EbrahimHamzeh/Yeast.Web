using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public partial class AboutUsController : BaseController
	{

		readonly IOptionService _optionService;
		readonly IUnitOfWork _uow;

		public AboutUsController(IUnitOfWork uow, IOptionService optionService)
		{
			_uow = uow;
            _optionService = optionService;
		}

		// GET: Home
		public virtual ActionResult Index()
		{
            AboutUsViewModel aboutUsViewModel = _optionService.GetByCulter();
            return View(aboutUsViewModel);
		}
	}
}