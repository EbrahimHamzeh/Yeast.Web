using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class DataTableList<T>
	{
		public IList<T> rows { get; set; }
		public int total { get; set; }
	}
}
