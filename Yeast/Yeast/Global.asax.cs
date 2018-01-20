using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Yeast.Datalayer.Context;
using Yeast.Datalayer.Migrations;

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
			//CaptchaUtils.CaptchaManager.StorageProvider = new CookieStorageProvider();
		}
		private void Application_EndRequest(object sender, EventArgs e)
		{
			ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
		}
	}
}
