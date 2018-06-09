using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IPostService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(PostAdd Post);
		void Remove(int id);
		void Update(PostEdit post, int id);
		Post Find(int id);
		PostEdit FindForEdit(int id);
		Task<IList<Post>> GetAllAsync();
        List<Post> GetAll();
        Task<DataTableList<PostList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}
