using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Yeast.Datalayer.Context;
using Yeast.Datalayer.Migrations;
using StructureMap.Web.Pipeline;
using Yeast.IocConfig;

namespace Yeast
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<YeastDbContext, Configuration>());

			AreaRegistration.RegisterAllAreas();
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// Remove Extra ViewEngins
			ViewEngines.Engines.Clear();
			ViewEngines.Engines.Add(new RazorViewEngine());
			MvcHandler.DisableMvcResponseHeader = true;

			initStructureMap();

			//CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
		}
		private void Application_EndRequest(object sender, EventArgs e)
		{
			HttpContextLifecycle.DisposeAndClearAll();
		}

		private static void initStructureMap()
		{
			//Set current Controller factory as StructureMapControllerFactory
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
		}
	}

	public class StructureMapControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			if (controllerType == null)
				throw new InvalidOperationException(string.Format("Page not found: {0}", requestContext.HttpContext.Request.RawUrl));
			return SmObjectFactory.Container.GetInstance(controllerType) as Controller;
		}
	}
}
