using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface ICategoryService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(CategoryAdd category);
		void Remove(int id);
		void Update(Category category);
		Category Find(int id);
		CategoryEdit FindForEdit(int id);
		Task<IList<Category>> GetAllAsync();
		Task<DataTableList<CategoryList>> GetDataTableAsync(PagedQueryViewModel model);
		SelectList DropDownList(List<int> categorySelectedId = null);
	}
}
