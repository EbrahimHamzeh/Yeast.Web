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
				Body = post.Body,
				Slug = post.Slug,
				Tags = tags,
				ServiceCategories = categories,
				Keyword = post.Keyword,
				Description = post.Description
			});
		}

		public Service Find(int id)
		{
			return _service.Find(id);
		}

		public ServiceEdit FindForEdit(int id)
		{
			Service post = _service.Find(id);
			return new ServiceEdit {
				Title = post.Title,
				Slug = post.Slug,
				Body = post.Body,
				TagIds = post.Tags.Select(x => x.Id).ToList(),
				CategoryIds = post.ServiceCategories.Select(x => x.Id).ToList(),
				Description = post.Description,
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
			selectedpost.Title = post.Title;
			selectedpost.Body = post.Body;
			selectedpost.Slug = post.Slug;
			selectedpost.Keyword = post.Keyword;
			selectedpost.Description = post.Description;
		}
	}
}
