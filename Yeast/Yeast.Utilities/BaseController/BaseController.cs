using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Yeast.Utilities.Controllers
{
	public class BaseController : Controller
	{
		/// <summary>
		/// from http://forums.asp.net/t/1776480.aspx/1?ExecuteCore+in+base+class+not+fired+in+MVC+4+beta
		/// </summary>
		protected override bool DisableAsyncSupport
		{
			get { return true; }
		}
		/// <summary>
		/// مدیریت زبان سایت با توجه به Url
		/// </summary>
		protected override void ExecuteCore()
		{
			var lang = RouteData.Values["lang"];
			if (lang != null && !string.IsNullOrWhiteSpace(lang.ToString()) && SupportLangues(lang.ToString()))
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang.ToString());
			}
			base.ExecuteCore();
		}

		protected override void OnActionExecuted(ActionExecutedContext context)
		{
			var view = context.Result as ViewResultBase;
			if (view == null) return; // not a view
			var viewName = view.ViewName;
			view.ViewName = GetGlobalizationViewName(viewName, context);
			base.OnActionExecuted(context);
		}
		private static string GetGlobalizationViewName(string viewName, ControllerContext context)
		{
			var cultureName = Thread.CurrentThread.CurrentUICulture.Name;
			if (cultureName == "en-US" || cultureName == "ru-RU")
			{
				if (string.IsNullOrEmpty(viewName))
					return "Ltr/" + context.RouteData.Values["action"]; // "Index.fa"
				return "Ltr/" + viewName; // "Index" ==> "Index.fa"
			}
			return viewName; // default culture (Persian and Arabi)
		}

		private bool SupportLangues(string lang)
		{
			if (lang == "fa" || lang == "ar")
			{
				return true;
			}
			if (lang == "en" || lang == "ru")
			{
				ViewBag.Direction = "Ltr";
				return true;
			}
			return false;
		}
	}
}