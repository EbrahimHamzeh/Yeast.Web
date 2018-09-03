using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IEmailService
    {
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(Email email);
		void Remove(int id);
		Task<IList<Email>> GetAllAsync();
        Email Find(int id);
        Task<DataTableList<EmailList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}
