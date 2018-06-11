using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface ITagService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(TagAdd tag);
		void Remove(int id);
		void Update(Tag tag);
		Tag Find(int id);
		TagEdit FindForEdit(int id);
        IList<Tag> GetAll();
        Task<IList<Tag>> GetAllAsync();
		Task<DataTableList<TagList>> GetDataTableAsync(PagedQueryViewModel model);
		SelectList DropDownList(List<int> tagSelectedId = null);
	}
}
