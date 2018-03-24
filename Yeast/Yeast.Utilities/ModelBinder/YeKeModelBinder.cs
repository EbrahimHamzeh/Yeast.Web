using System.Web.Mvc;

namespace Yeast.Utilities.ModelBinder
{
	public class YeKeModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			object value = base.BindModel(controllerContext, bindingContext);

			if (value == null)
			{
				return value;
			}

			return value.ToString().Replace((char)1610, (char)1740).Replace((char)1603, (char)1705);
		}
	}
}
