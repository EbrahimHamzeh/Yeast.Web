using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Yeast.Utilities.Helpers
{
	public static class Extention
	{
		public static string RemoveHtmlTags(string input)
		{
			var withoutHtml = String.IsNullOrEmpty(input) ?
							String.Empty :
							Regex.Replace(input, @"<(.|\n)*?>", String.Empty, RegexOptions.Compiled);

			withoutHtml = withoutHtml.Replace("&nbsp;", " ");
			withoutHtml = withoutHtml.Replace("&zwnj;", " ");
			withoutHtml = withoutHtml.Replace("&quot;", " ");
			withoutHtml = withoutHtml.Replace("amp;", "");
			withoutHtml = withoutHtml.Replace("&laquo;", "«");
			withoutHtml = withoutHtml.Replace("&raquo;", "»");

			return withoutHtml;
		}

		/// <summary>
		/// برش متن با توجه به کاراکتر خاص یا تعداد کلامات
		/// </summary>
		/// <param name="htmlHelper"></param>
		/// <param name="text"></param>
		/// <returns></returns>
		public static MvcHtmlString Nl2Br(this HtmlHelper htmlHelper, string text)
		{
			if (string.IsNullOrEmpty(text)) return MvcHtmlString.Create(text);
			else
			{
				StringBuilder builder = new StringBuilder();
				string[] lines = Regex.Split(text, " ");
				if (lines.Length <= 22) { return MvcHtmlString.Create(text); }

				for (int i = 0; i <= 22; i++)
				{
					builder.Append(lines[i]);
					builder.Append(" ");
				}
				builder.Append(" ... ");

				return MvcHtmlString.Create(builder.ToString());
			}
		}
	}
}
