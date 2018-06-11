﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Controllers
{
    public class PostController : Controller
    {
        readonly IPostService _postService;
        readonly ITagService _tagService;
        readonly ICategoryService _categoryService;
        readonly IUnitOfWork _uow;

        public PostController(IUnitOfWork uow, IPostService postService, ITagService tagService, ICategoryService categotyService)
        {
            _uow = uow;
            _postService = postService;
            _tagService = tagService;
            _categoryService = categotyService;
        }

        // GET: Post
        public ActionResult Index()
        {
            List<PostModel> list =_postService.GetByCulterPost();
            return View(list);

        }
        public ActionResult Details(int id)
        {
            PostModel blog = _postService.Find(id);
            
            return View(blog);

        }
    }
}