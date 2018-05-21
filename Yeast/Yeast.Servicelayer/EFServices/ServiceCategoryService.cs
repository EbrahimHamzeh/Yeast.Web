using EFSecondLevelCache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yeast.Model.Admin;
using Yeast.Utilities.BootstrapTable;
using Yeast.Utilities.Helpers;

namespace Yeast.Servicelayer.EFServices
{
	public class ServiceCategoryService : IServiceCategoryService
	{
		private readonly IDbSet<ServiceCategory> _serviceCategories;

		public ServiceCategoryService(IUnitOfWork uow)
		{
            _serviceCategories = uow.Set<ServiceCategory>();
		}

        public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _serviceCategories.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(ServiceCategoryAdd category)
		{
            _serviceCategories.Add(new ServiceCategory { Title = category.Title, Description = category.Description, Slug = category.Slug, Order = category.Order });
		}

		public ServiceCategory Find(int id)
		{
			return _serviceCategories.Find(id);
		}

		public ServiceCategoryEdit FindForEdit(int id)
		{
			ServiceCategory category = _serviceCategories.Find(id);
			return new ServiceCategoryEdit
			{
				Title = category.Title,
				Description = category.Description,
				Slug = category.Slug,
				Order = category.Order
			};
		}

		public async Task<IList<ServiceCategory>> GetAllAsync()
		{
			return await _serviceCategories.AsNoTracking().Cacheable().ToListAsync();
		}

        public async Task<DataTableList<ServiceCategoryList>> GetDataTableAsync(PagedQueryViewModel model)
        {
            IQueryable<ServiceCategory> categoryList = _serviceCategories.AsNoTracking();
            int total = 0;

            // Search
            //tagList = tagList.ApplySearch(model);

            total = await categoryList.CountAsync().ConfigureAwait(false);

            // Ordering data 
            categoryList = categoryList.ApplyOrdering(model);

            // Paging And Save Cach
            categoryList = categoryList.ApplyPaging(model).Cacheable();
            model.offset = model.offset - 1;
            // Create List Of viewModel
            var category = (await categoryList.ToListAsync()).Select((x, index) => new ServiceCategoryList
            {
                No = (++index + model.offset).ConvertToPersianString(),
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Order = x.Order
            });

            return new DataTableList<ServiceCategoryList> { rows = category.ToList(), total = total };
        }

		public void Remove(int id)
		{
            _serviceCategories.Remove(_serviceCategories.Find(id));
		}

		public void Update(ServiceCategory category)
		{
			ServiceCategory selectedcategory = _serviceCategories.Find(category.Id);
			selectedcategory.Title = category.Title;
			selectedcategory.Description = category.Description;
			selectedcategory.Slug = category.Slug;
			selectedcategory.Order = category.Order;
		}

		public SelectList DropDownList(List<int> categorySelectedId = null)
		{
			List<SelectListItem> selectListItemList;
			if (categorySelectedId == null)
			{
				selectListItemList = _serviceCategories.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
			}
			else
			{
				selectListItemList = _serviceCategories.Select(x => new SelectListItem { 
					Text = x.Title,
					Value = x.Id.ToString(),
					Selected = categorySelectedId.Contains(x.Id)
				}).ToList();
			}
			return new SelectList(selectListItemList, "Value", "Text", categorySelectedId);
		}
	}
}
