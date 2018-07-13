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
	public class RepresentationService : IRepresentationService
	{
		private readonly IDbSet<Representation> _representations;

		public RepresentationService(IUnitOfWork uow)
		{
			_representations = uow.Set<Representation>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _representations.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(RepresentationAdd category)
		{
			_representations.Add(new Representation {
                Title = category.Title,
                Description = category.Description,
                Telephone = category.Telephone,
                Latitude = category.Latitude,
                Longitude = category.Longitude
            });
		}

		public Representation Find(int id)
		{
			return _representations.Find(id);
		}

		public RepresentationEdit FindForEdit(int id)
		{
			Representation category = _representations.Find(id);
			return new RepresentationEdit
			{
				Title = category.Title,
				Description = category.Description,
                Telephone = category.Telephone,
                Latitude = category.Latitude,
                Longitude = category.Longitude
            };
		}

		public async Task<IList<Representation>> GetAllAsync()
		{
			return await _representations.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<RepresentationList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Representation> categoryList = _representations.AsNoTracking();
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
			var category = (await categoryList.ToListAsync()).Select((x, index) => new RepresentationList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
				Description = x.Description,
			});

			return new DataTableList<RepresentationList> { rows = category.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_representations.Remove(_representations.Find(id));
		}

		public void Update(Representation category)
		{
			Representation selectedcategory = _representations.Find(category.Id);
			selectedcategory.Title = category.Title;
			selectedcategory.Description = category.Description;
            selectedcategory.Telephone = category.Telephone;
            selectedcategory.Latitude = category.Latitude;
            selectedcategory.Longitude = category.Longitude;

        }

		public SelectList DropDownList(List<int> categorySelectedId = null)
		{
			List<SelectListItem> selectListItemList;
			if (categorySelectedId == null)
			{
				selectListItemList = _representations.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
			}
			else
			{
				selectListItemList = _representations.Select(x => new SelectListItem { 
					Text = x.Title,
					Value = x.Id.ToString(),
					Selected = categorySelectedId.Contains(x.Id)
				}).ToList();
			}
			return new SelectList(selectListItemList, "Value", "Text", categorySelectedId);
		}
	}
}
