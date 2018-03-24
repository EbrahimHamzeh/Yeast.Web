using EFSecondLevelCache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;
using Yeast.Model.Admin;
using System.Threading.Tasks;
using Yeast.Utilities.Helpers;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;

namespace Yeast.Servicelayer.EFServices
{
	public class PostService : IPostService
	{
		private readonly IDbSet<Post> _posts;
		public PostService(IUnitOfWork uow)
		{
			_posts = uow.Set<Post>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _posts.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(PostAdd post)
		{
			_posts.Add(new Post { CreatedDate = DateTime.Now ,Description = post.Description });
		}

		public Post Find(int id)
		{
			return _posts.Find(id);
		}

		public PostEdit FindForEdit(int id)
		{
			Post post = _posts.Find(id);
			return new PostEdit {
				Description = post.Description
			};
		}

		public async Task<IList<Post>> GetAllAsync()
		{
			return await _posts.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<PostList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Post> postList = _posts.AsNoTracking();
			int total = 0;

			// Search
			//postList = postList.ApplySearch(model);

			total = await postList.CountAsync().ConfigureAwait(false);

			// Ordering data 
			postList = postList.ApplyOrdering(model);

			// Paging And Save Cach
			postList = postList.ApplyPaging(model).Cacheable();
			model.offset = model.offset - 1;
			// Create List Of viewModel
			var post = (await postList.ToListAsync()).Select((x, index) => new PostList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				//Title = x.PostTranslatTitle,
				Description = x.Description
			});

			return new DataTableList<PostList> { rows = post.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_posts.Remove(_posts.Find(id));
		}

		public void Update(Post post)
		{
			Post selectedpost = _posts.Find(post.Id);
			selectedpost.Description = post.Description;
		}
	}
}
