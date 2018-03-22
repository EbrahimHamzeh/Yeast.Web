using System.Web.Mvc;

namespace Yeast.Attribute
{
	/// <summary>
	/// AjaxOnluy Requst Just Ajax Respons
	/// </summary>
	[System.AttributeUsageAttribute(System.AttributeTargets.All, AllowMultiple = false)]
	public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
	{
		public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
		{
			return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
		}
	}
}
