using System.Collections.Generic;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;

namespace Yeast.Servicelayer.Interfaces
{
	public interface ITagService
	{
		int Count { get; }
		void Add(TagAdd tag);
		void Remove(int id);
		void Update(TagEdit tag);
		Tag Find(int id);
		IList<Tag> GetAll();
		IList<TagList> GetPaging(string order = "asc", int offset = 0, int limit = 10);
		DataTableList<TagList> GetDataTable(string search = "", string sort = "Title", string order = "asc", int offset = 0, int limit = 10);
	}
}
