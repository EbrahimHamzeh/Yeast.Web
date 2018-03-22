using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Utilities.BootstrapTable
{
	public class PagedQueryViewModel : IPagedQueryModel
	{
		public string search { get; set; }

		public string sort { get; set; }

		public string order { get; set; }

		public int offset { get; set; }

		public int limit { get; set; }

		public string filter { get; set; }
	}
}
