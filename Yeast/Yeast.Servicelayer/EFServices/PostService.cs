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
		private readonly IDbSet<Tag> _tags;
		private readonly IDbSet<Category> _categories;

		public PostService(IUnitOfWork uow)
		{
			_posts = uow.Set<Post>();
			_tags = uow.Set<Tag>();
			_categories = uow.Set<Category>();
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
			List<Tag> tags = new List<Tag>();
			foreach (var item in post.TagIds)
			{
				tags.Add(_tags.Find(item));
			}

			List<Category> categories = new List<Category>();
			foreach (var item in post.CategoryIds)
			{
				categories.Add(_categories.Find(item));
			}

			_posts.Add(new Post {
				CreatedDate = DateTime.Now,
				Title = post.Title,
				Body = post.Body,
				Slug = post.Slug,
                ImageTitle=post.ImageTitle,
				Tags = tags,
				Categories = categories,
				Keyword = post.Keyword,
				Description = post.Description
			});
		}

		public Post Find(int id)
		{
			return _posts.Find(id);
		}

		public PostEdit FindForEdit(int id)
		{
			Post post = _posts.Find(id);
			return new PostEdit {
				Title = post.Title,
				Slug = post.Slug,
				Body = post.Body,
				TagIds = post.Tags.Select(x => x.Id).ToList(),
				CategoryIds = post.Categories.Select(x => x.Id).ToList(),
				Description = post.Description,
				Keyword = post.Keyword
			};
		}

		public async Task<IList<Post>> GetAllAsync()
		{
			return await _posts.AsNoTracking().Cacheable().ToListAsync();
		}
        public List<Post> GetAll()
        {
            return _posts.AsNoTracking().Cacheable().ToList();
        }

        public async Task<DataTableList<PostList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Post> postList = _posts.AsNoTracking();
			int total = 0;

			// Search
			postList = postList.ApplySearch(model);

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

		public void Update(PostEdit post, int id)
		{
			Post selectedpost = _posts.Find(id);
			foreach (var item in selectedpost.Tags.Where(x => !post.TagIds.Contains(x.Id)).ToList())
			{
				selectedpost.Tags.Remove(item);
			}
			
			foreach (var item in post.TagIds)
			{
				selectedpost.Tags.Add(_tags.Find(item));
			}

			foreach (var item in selectedpost.Categories.Where(x => !post.CategoryIds.Contains(x.Id)).ToList())
			{
				selectedpost.Categories.Remove(item);
			}

			foreach (var item in post.CategoryIds)
			{
				selectedpost.Categories.Add(_categories.Find(item));
			}

			selectedpost.CreatedDate = DateTime.Now;
			selectedpost.Title = post.Title;
			selectedpost.Body = post.Body;
			selectedpost.Slug = post.Slug;
			selectedpost.Keyword = post.Keyword;
			selectedpost.Description = post.Description;
		}
	}
}
