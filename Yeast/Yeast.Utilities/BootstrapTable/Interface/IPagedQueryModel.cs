using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Utilities.BootstrapTable
{
	public interface IPagedQueryModel
	{
		string search { get; set; }
		string sort { get; set; }
		string order { get; set; }
		int offset { get; set; }
		int limit { get; set; }
		string filter { get; set; }
	}
}
