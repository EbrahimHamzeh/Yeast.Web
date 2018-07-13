using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IRepresentationService
    {
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(RepresentationAdd category);
		void Remove(int id);
		void Update(Representation category);
        Representation Find(int id);
        RepresentationEdit FindForEdit(int id);
		Task<IList<Representation>> GetAllAsync();
		Task<DataTableList<RepresentationList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}
