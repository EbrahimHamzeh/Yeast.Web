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
using System.Web.Mvc;

namespace Yeast.Servicelayer.EFServices
{
	public class TagService : ITagService
	{
		private readonly IDbSet<Tag> _tags;
		public TagService(IUnitOfWork uow)
		{
			_tags = uow.Set<Tag>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _tags.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(TagAdd tag)
		{
			_tags.Add(new Tag { Title = tag.Title, Description = tag.Description });
		}

		public Tag Find(int id)
		{
			return _tags.Find(id);
		}

		public TagEdit FindForEdit(int id)
		{
			Tag tag = _tags.Find(id);
			return new TagEdit
			{
				Title = tag.Title,
				Description = tag.Description
			};
		}

		public async Task<IList<Tag>> GetAllAsync()
		{
			return await _tags.AsNoTracking().Cacheable().ToListAsync();
		}
        public  IList<Tag> GetAll()
        {
            return _tags.AsNoTracking().Cacheable().ToList();
        }

        public async Task<DataTableList<TagList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Tag> tagList = _tags.AsNoTracking();
			int total = 0;

			// Search
			//tagList = tagList.ApplySearch(model);

			total = await tagList.CountAsync().ConfigureAwait(false);

			// Ordering data 
			tagList = tagList.ApplyOrdering(model);

			// Paging And Save Cach
			tagList = tagList.ApplyPaging(model).Cacheable();
			model.offset = model.offset - 1;
			// Create List Of viewModel
			var tag = (await tagList.ToListAsync()).Select((x, index) => new TagList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
				Description = x.Description
			});

			return new DataTableList<TagList> { rows = tag.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_tags.Remove(_tags.Find(id));
		}

		public void Update(Tag tag)
		{
			Tag selectedTag = _tags.Find(tag.Id);
			selectedTag.Title = tag.Title;
			selectedTag.Description = tag.Description;
		}

		public SelectList DropDownList(List<int> tagSelectedId = null)
		{
			List<SelectListItem> selectListItemList;
			if (tagSelectedId == null)
			{
				selectListItemList = _tags.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
			}
			else
			{
				selectListItemList = _tags.Select(x => new SelectListItem
				{
					Text = x.Title,
					Value = x.Id.ToString(),
					Selected = tagSelectedId.Contains(x.Id)
				}).ToList();
			}
			return new SelectList(selectListItemList, "Value", "Text", tagSelectedId);
		}
	}
}
