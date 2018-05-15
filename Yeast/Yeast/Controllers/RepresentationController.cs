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

		//readonly IProductService _productService;
		//readonly IUnitOfWork _uow;

		//public ProductController(IUnitOfWork uow, IProductService productService)
		//{
		//	_uow = uow;
  //          _productService = productService;
		//}

        // GET: /{lang}/Product
        public virtual ActionResult Index()
        {
            return View();
        }

	}
}