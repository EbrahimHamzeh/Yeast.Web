using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Controllers;

namespace Yeast.Controllers
{
    public class ServiceController : BaseController
    {
        // GET: Service
        readonly IServiceService _serviceService;
        readonly ITagService _tagService;
        readonly ICategoryService _categoryService;
        readonly IUnitOfWork _uow;

        public ServiceController(IUnitOfWork uow, IServiceService serviceService, ITagService tagService, ICategoryService categotyService)
        {
            _uow = uow;
            _serviceService = serviceService;
            _tagService = tagService;
            _categoryService = categotyService;
        }

        // GET: Post
        public ActionResult Index()
        {
            List<ServiceModel> list = _serviceService.GetByCulterPost();
            return View(list);

        }
        public ActionResult Details(int id)
        {
            ServiceModel service = _serviceService.Find(id);
            service.TagList = _tagService.DropDownList(service.TagIds);
            service.CategoryList = _categoryService.DropDownList(service.CategoryIds);
            return View(service);

        }
    }
}