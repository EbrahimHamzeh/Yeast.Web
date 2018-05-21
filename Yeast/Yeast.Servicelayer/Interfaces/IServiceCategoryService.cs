using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IServiceCategoryService
    {
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(ServiceCategoryAdd category);
		void Remove(int id);
		void Update(ServiceCategory category);
		ServiceCategory Find(int id);
        ServiceCategoryEdit FindForEdit(int id);
		Task<IList<ServiceCategory>> GetAllAsync();
		Task<DataTableList<ServiceCategoryList>> GetDataTableAsync(PagedQueryViewModel model);
		SelectList DropDownList(List<int> categorySelectedId = null);
	}
}
