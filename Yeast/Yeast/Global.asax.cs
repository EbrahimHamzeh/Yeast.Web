using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Yeast.Datalayer.Context;
using Yeast.Datalayer.Migrations;
using StructureMap.Web.Pipeline;
using Yeast.IocConfig;
using Yeast.Utilities.ModelBinder;

namespace Yeast
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize(); // EF Profiler For Debug When Publish Project this line remove and web.config Entitylog File  
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<YeastDbContext, Configuration>());

			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// Remove Extra ViewEngins
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
			MvcHandler.DisableMvcResponseHeader = true;

			// Automatic When Model Binding Edit YeKe Arabi Always
			ModelBinders.Binders.Add(typeof(string), new YeKeModelBinder());
			// Automatic When Model Binding Edit DateTime Always
			ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());

			initStructureMap();

			//CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
		}

		private void Application_EndRequest(object sender, EventArgs e)
		{
			HttpContextLifecycle.DisposeAndClearAll();
		}

		private static void initStructureMap()
		{
			SmObjectFactory.Container.GetInstance<IUnitOfWork>().ForceDatabaseInitialize();
			//Set current Controller factory as StructureMapControllerFactory
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
		}
	}

	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
			{
				//throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
				return null;
			}
			return SmObjectFactory.Container.GetInstance(controllerType) as Controller;
		}
	}
}
