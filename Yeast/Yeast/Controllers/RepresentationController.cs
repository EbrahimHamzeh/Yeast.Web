using System.Collections.Generic;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public class RepresentationController : BaseController
	{

		readonly IRepresentationService _representationService;
		readonly IUnitOfWork _uow;

        public RepresentationController(IUnitOfWork uow, IRepresentationService representationService)
        {
            _uow = uow;
            _representationService = representationService;
        }

        // GET: /{lang}/Product
        public virtual ActionResult Index()
        {
            return View(_representationService.GetAllAsync().Result);
        }

	}
}