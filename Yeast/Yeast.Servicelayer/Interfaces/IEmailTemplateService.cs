using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IEmailTemplateService
    {
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(EmailTemplateAdd tag);
		void Remove(int id);
        void Update(EmailTemplateEdit model, int id);
        EmailTemplate Find(int id);
		EmailTemplateEdit FindForEdit(int id);
		Task<IList<EmailTemplate>> GetAllAsync();
		Task<DataTableList<EmailTemplateList>> GetDataTableAsync(PagedQueryViewModel model);
		SelectList DropDownList(List<int> tagSelectedId = null);
	}
}
