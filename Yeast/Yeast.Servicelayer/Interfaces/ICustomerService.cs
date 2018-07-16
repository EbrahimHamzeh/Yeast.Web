using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface ICustomerService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(CustomerAdd customer);
		void Remove(int id);
		void Update(Customer customer);
        Customer Find(int id);
        CustomerEdit FindForEdit(int id);
		Task<IList<Customer>> GetAllAsync();
		Task<DataTableList<CustomerList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}
