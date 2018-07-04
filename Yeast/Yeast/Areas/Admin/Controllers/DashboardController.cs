using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Areas.Admin.Controllers
{
	public partial class DashboardController : Controller
	{
        readonly IStatisticsService _statisticsService;
        readonly IUnitOfWork _uow;

        public DashboardController(IUnitOfWork uow, IStatisticsService statisticsService)
        {
            _uow = uow;
            _statisticsService = statisticsService;
        }
        // GET: Admin/Dashboard
        public virtual ActionResult Index()
		{
            return View(_statisticsService.StatisticsCount());
		}
	}
}