using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IProductService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(ProductAdd Product);
		void Remove(int id);
		void Update(Product Product);
		Product Find(int id);
		ProductEdit FindForEdit(int id);
        Task<IList<Model.FrontEnd.Product>> GetAllAsync();
        Task<DataTableList<ProductList>> GetDataTableAsync(string search = "", string sort = "Title", string order = "asc", int offset = 0, int limit = 10);
	}
}
