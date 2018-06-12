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
using Yeast.Model.FrontEnd;

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
                TitleEn = post.TitleEn,
                TitleAr = post.TitleAr,
                TitleRu = post.TitleRu,
                Body = post.Body,
                BodyEn = post.BodyEn,
                BodyAr = post.BodyAr,
                BodyRu = post.BodyRu,
                Slug = post.Slug,
                ImageTitle=post.ImageTitle,
				Tags = tags,
				Categories = categories,
				Keyword = post.Keyword,
				Description = post.Description,
                DescriptionEn = post.DescriptionEn,
                DescriptionAr = post.DescriptionAr,
                DescriptionRu = post.DescriptionRu
            });
		}

		public PostModel Find(int id)
		{
            string cultur = CultureHelper.GetCurrentNeutralCulture();
            PostModel postModel = new PostModel();
            Post post= _posts.Find(id);
            switch (cultur)
            {
                case "fa":
                    postModel.Title = post.Title;
                    postModel.Body = post.Body;
                    postModel.Description = post.Description;
                    postModel.CreatedDate = post.CreatedDate.ToString();
                    postModel.Slug = post.Slug;
                    postModel.TitleImg = post.ImageTitle;
                    postModel.TagIds = post.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = post.Categories.Select(x => x.Id).ToList();
                    break;
                case "en":
                    postModel.Title = post.TitleEn;
                    postModel.Body = post.BodyEn;
                    postModel.Description = post.DescriptionEn;
                    postModel.CreatedDate = post.CreatedDate.ToString();
                    postModel.Slug = post.Slug;
                    postModel.TitleImg = post.ImageTitle;
                    postModel.TagIds = post.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = post.Categories.Select(x => x.Id).ToList();
                    break;
                case "ar":
                    postModel.Title = post.TitleAr;
                    postModel.Body = post.BodyAr;
                    postModel.Description = post.DescriptionAr;
                    postModel.CreatedDate = post.CreatedDate.ToString();
                    postModel.Slug = post.Slug;
                    postModel.TitleImg = post.ImageTitle;
                    postModel.TagIds = post.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = post.Categories.Select(x => x.Id).ToList();
                    break;
                case "ru":
                    postModel.Title = post.TitleRu;
                    postModel.Body = post.BodyRu;
                    postModel.Description = post.DescriptionRu;
                    postModel.CreatedDate = post.CreatedDate.ToString();
                    postModel.Slug = post.Slug;
                    postModel.TitleImg = post.ImageTitle;
                    postModel.TagIds = post.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = post.Categories.Select(x => x.Id).ToList();
                    break;
                default:
                    break;
            }
            return postModel;

        }

		public PostEdit FindForEdit(int id)
		{
			Post post = _posts.Find(id);
			return new PostEdit {
				Title = post.Title,
                TitleEn = post.TitleEn,
                TitleAr = post.TitleAr,
                TitleRu = post.TitleRu,
                TitleImg = post.ImageTitle,
                Slug = post.Slug,
				Body = post.Body,
                BodyEn = post.BodyEn,
                BodyAr = post.BodyAr,
                BodyRu = post.BodyRu,
                TagIds = post.Tags.Select(x => x.Id).ToList(),
				CategoryIds = post.Categories.Select(x => x.Id).ToList(),
				Description = post.Description,
                DescriptionEn = post.DescriptionEn,
                DescriptionAr = post.DescriptionAr,
                DescriptionRu = post.DescriptionRu,
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


        public List<PostModel> GetByCulterPost()
        {
            string cultur = CultureHelper.GetCurrentNeutralCulture();

            List <PostModel> list =_posts.AsNoTracking().Cacheable().Select(x => new PostModel {
                Body = cultur == "fa" ? x.Body : cultur == "en" ? x.BodyEn : cultur == "ar" ? x.BodyAr : cultur == "ru" ? x.BodyRu : "",
                Title = cultur == "fa" ? x.Title : cultur == "en" ? x.TitleEn : cultur == "ar" ? x.TitleAr : cultur == "ru" ? x.TitleRu : "",
                Description = cultur == "fa" ? x.Description : cultur == "en" ? x.DescriptionEn : cultur == "ar" ? x.DescriptionAr : cultur == "ru" ? x.DescriptionRu : "",
                Slug = x.Slug,
                Id = x.Id,
                CreatedDate=x.CreatedDate.ToString(),
                TitleImg = x.ImageTitle
            }).ToList();
            return list;
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
            selectedpost.ImageTitle = post.TitleImg;
            selectedpost.Title = post.Title;
            selectedpost.TitleEn = post.TitleEn;
            selectedpost.TitleAr = post.TitleAr;
            selectedpost.TitleRu = post.TitleRu;
            selectedpost.Body = post.Body;
            selectedpost.BodyEn = post.BodyEn;
            selectedpost.BodyAr = post.BodyAr;
            selectedpost.BodyRu = post.BodyRu;
            selectedpost.Slug = post.Slug;
			selectedpost.Keyword = post.Keyword;
			selectedpost.Description = post.Description;
            selectedpost.DescriptionEn = post.DescriptionEn;
            selectedpost.DescriptionAr = post.DescriptionAr;
            selectedpost.DescriptionRu = post.DescriptionRu;
        }
	}
}
