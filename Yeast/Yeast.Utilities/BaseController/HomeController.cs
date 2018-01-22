using System.Web.Mvc;

namespace Yeast.Utilities.Controllers
{
	public class MyBaseController : Controller
	{
		/// <summary>
		/// from http://forums.asp.net/t/1776480.aspx/1?ExecuteCore+in+base+class+not+fired+in+MVC+4+beta
		/// </summary>
		protected override bool DisableAsyncSupport
		{
			get { return true; }
		}

		protected override void ExecuteCore()
		{
			base.ExecuteCore();
		}
	}
}