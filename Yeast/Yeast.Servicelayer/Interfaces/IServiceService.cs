using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IServiceService
    {
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(ServiceAdd Post);
		void Remove(int id);
		void Update(ServiceEdit post, int id);
        Service Find(int id);
        ServiceEdit FindForEdit(int id);
		Task<IList<Service>> GetAllAsync();
		Task<DataTableList<ServiceList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}
