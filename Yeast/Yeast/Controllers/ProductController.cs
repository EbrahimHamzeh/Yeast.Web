﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{

	public class ProductController : BaseController
	{

		readonly IProductService _productService;
		readonly IUnitOfWork _uow;

		public ProductController(IUnitOfWork uow, IProductService productService)
		{
			_uow = uow;
            _productService = productService;
		}

        // GET: /{lang}/Product
        public virtual ActionResult Index()
        {
            IList<Model.FrontEnd.Product> products = _productService.GetAllAsync().Result;

            return View(products);
        }

        // GET: /{lang}/Product/5
        public virtual ActionResult Detial(int id)
		{
            ProductEdit product = _productService.FindForEdit(id);

            return View(product);
		}
	}
}