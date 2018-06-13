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
	public class ServiceService : IServiceService
	{
		private readonly IDbSet<Service> _service;
		private readonly IDbSet<Tag> _tags;
		private readonly IDbSet<ServiceCategory> _serviceCategory;

		public ServiceService(IUnitOfWork uow)
		{
			_service = uow.Set<Service>();
			_tags = uow.Set<Tag>();
			_serviceCategory = uow.Set<ServiceCategory>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _service.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(ServiceAdd post)
		{
			List<Tag> tags = new List<Tag>();
			foreach (var item in post.TagIds)
			{
				tags.Add(_tags.Find(item));
			}

			List<ServiceCategory> categories = new List<ServiceCategory>();
			foreach (var item in post.CategoryIds)
			{
				categories.Add(_serviceCategory.Find(item));
			}

			_service.Add(new Service {
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
                ImageTitle = post.ImageTitle,
                Tags = tags,
                ServiceCategories = categories,
                Keyword = post.Keyword,
                Description = post.Description,
                DescriptionEn = post.DescriptionEn,
                DescriptionAr = post.DescriptionAr,
                DescriptionRu = post.DescriptionRu
            });
		}

        public ServiceModel Find(int id)
        {
            string cultur = CultureHelper.GetCurrentNeutralCulture();
            ServiceModel postModel = new ServiceModel();
            Service service = _service.Find(id);
            switch (cultur)
            {
                case "fa":
                    postModel.Title = service.Title;
                    postModel.Body = service.Body;
                    postModel.Description = service.Description;
                    postModel.CreatedDate = service.CreatedDate.ToString();
                    postModel.Slug = service.Slug;
                    postModel.TitleImg = service.ImageTitle;
                    postModel.TagIds = service.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = service.ServiceCategories.Select(x => x.Id).ToList();
                    break;
                case "en":
                    postModel.Title = service.TitleEn;
                    postModel.Body = service.BodyEn;
                    postModel.Description = service.DescriptionEn;
                    postModel.CreatedDate = service.CreatedDate.ToString();
                    postModel.Slug = service.Slug;
                    postModel.TitleImg = service.ImageTitle;
                    postModel.TagIds = service.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = service.ServiceCategories.Select(x => x.Id).ToList();
                    break;
                case "ar":
                    postModel.Title = service.TitleAr;
                    postModel.Body = service.BodyAr;
                    postModel.Description = service.DescriptionAr;
                    postModel.CreatedDate = service.CreatedDate.ToString();
                    postModel.Slug = service.Slug;
                    postModel.TitleImg = service.ImageTitle;
                    postModel.TagIds = service.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = service.ServiceCategories.Select(x => x.Id).ToList();
                    break;
                case "ru":
                    postModel.Title = service.TitleRu;
                    postModel.Body = service.BodyRu;
                    postModel.Description = service.DescriptionRu;
                    postModel.CreatedDate = service.CreatedDate.ToString();
                    postModel.Slug = service.Slug;
                    postModel.TitleImg = service.ImageTitle;
                    postModel.TagIds = service.Tags.Select(x => x.Id).ToList();
                    postModel.CategoryIds = service.ServiceCategories.Select(x => x.Id).ToList();
                    break;
                default:
                    break;
            }
            return postModel;

        }

        public ServiceEdit FindForEdit(int id)
		{
			Service post = _service.Find(id);
			return new ServiceEdit {
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
				CategoryIds = post.ServiceCategories.Select(x => x.Id).ToList(),
				Description = post.Description,
                DescriptionEn = post.DescriptionEn,
                DescriptionAr = post.DescriptionAr,
                DescriptionRu = post.DescriptionRu,
                Keyword = post.Keyword
			};
		}

		public async Task<IList<Service>> GetAllAsync()
		{
			return await _service.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<ServiceList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Service> postList = _service.AsNoTracking();
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
			var post = (await postList.ToListAsync()).Select((x, index) => new ServiceList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				//Title = x.ServiceTranslatTitle,
				Description = x.Description
			});

			return new DataTableList<ServiceList> { rows = post.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_service.Remove(_service.Find(id));
		}
        public List<ServiceModel> GetByCulterPost()
        {
            string cultur = CultureHelper.GetCurrentNeutralCulture();

            List<ServiceModel> list = _service.AsNoTracking().Cacheable().Select(x => new ServiceModel
            {
                Body = cultur == "fa" ? x.Body : cultur == "en" ? x.BodyEn : cultur == "ar" ? x.BodyAr : cultur == "ru" ? x.BodyRu : "",
                Title = cultur == "fa" ? x.Title : cultur == "en" ? x.TitleEn : cultur == "ar" ? x.TitleAr : cultur == "ru" ? x.TitleRu : "",
                Description = cultur == "fa" ? x.Description : cultur == "en" ? x.DescriptionEn : cultur == "ar" ? x.DescriptionAr : cultur == "ru" ? x.DescriptionRu : "",
                Slug = x.Slug,
                Id = x.Id,
                CreatedDate = x.CreatedDate.ToString(),
                TitleImg = x.ImageTitle
            }).ToList();
            return list;
        }
        public void Update(ServiceEdit post, int id)
		{
			Service selectedpost = _service.Find(id);
			foreach (var item in selectedpost.Tags.Where(x => !post.TagIds.Contains(x.Id)).ToList())
			{
				selectedpost.Tags.Remove(item);
			}
			
			foreach (var item in post.TagIds)
			{
				selectedpost.Tags.Add(_tags.Find(item));
			}

			foreach (var item in selectedpost.ServiceCategories.Where(x => !post.CategoryIds.Contains(x.Id)).ToList())
			{
				selectedpost.ServiceCategories.Remove(item);
			}

			foreach (var item in post.CategoryIds)
			{
				selectedpost.ServiceCategories.Add(_serviceCategory.Find(item));
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
