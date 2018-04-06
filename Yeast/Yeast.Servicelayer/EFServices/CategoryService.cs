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
	public class CategoryService : ICategoryService
	{
		private readonly IDbSet<Category> _categories;

		public CategoryService(IUnitOfWork uow)
		{
			_categories = uow.Set<Category>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _categories.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(CategoryAdd category)
		{
			_categories.Add(new Category { Title = category.Title, Description = category.Description, Slug = category.Slug, Order = category.Order });
		}

		public Category Find(int id)
		{
			return _categories.Find(id);
		}

		public CategoryEdit FindForEdit(int id)
		{
			Category category = _categories.Find(id);
			return new CategoryEdit
			{
				Title = category.Title,
				Description = category.Description,
				Slug = category.Slug,
				Order = category.Order
			};
		}

		public async Task<IList<Category>> GetAllAsync()
		{
			return await _categories.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<CategoryList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Category> categoryList = _categories.AsNoTracking();
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
			var category = (await categoryList.ToListAsync()).Select((x, index) => new CategoryList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
				Description = x.Description,
				Order = x.Order
			});

			return new DataTableList<CategoryList> { rows = category.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_categories.Remove(_categories.Find(id));
		}

		public void Update(Category category)
		{
			Category selectedcategory = _categories.Find(category.Id);
			selectedcategory.Title = category.Title;
			selectedcategory.Description = category.Description;
			selectedcategory.Slug = category.Slug;
			selectedcategory.Order = category.Order;
		}

		public SelectList DropDownList(int categorySelectedId = 0)
		{
			List<SelectListItem> selectListItemList = _categories.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
			return new SelectList(selectListItemList, "Value", "Text", categorySelectedId.ToString());
		}
	}
}
