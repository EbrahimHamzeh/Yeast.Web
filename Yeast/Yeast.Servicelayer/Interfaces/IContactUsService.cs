using System.Threading.Tasks;
using Yeast.Model.Admin;
using Yeast.Utilities.BootstrapTable;
using Yeast.Model.FrontEnd;
using Yeast.DomainClasses.Entities;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IContactUsService
    {
        void Add(ContactUsViewModel model);
        void Remove(int id);
        ContactUsViewModel View(int id);
        ContactUs Find(int id);
		Task<DataTableList<ContactUsList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}
