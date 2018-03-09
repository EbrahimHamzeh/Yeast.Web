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
			_tags.Add(new Tag { Name = tag.Name, Description = tag.Description });
		}

		public Tag Find(int id)
		{
			return _tags.Find(id);
		}

		public TagEdit FindForEdit(int id)
		{
			Tag tag = _tags.Find(id);
			return new TagEdit {
				Name = tag.Name,
				Description = tag.Description
			};
		}

		public async Task<IList<Tag>> GetAllAsync()
		{
			return await _tags.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<TagList>> GetDataTableAsync(string search = "", string sort = "Name", string order = "asc", int offset = 0, int limit = 10)
		{
			IQueryable<Tag> tagList = _tags.AsNoTracking();
			int total = 0;

			// Search
			if (!string.IsNullOrEmpty(search))
			{
				tagList = tagList.Where("Name.Contains(@0) or Description.Contains(@0)", search);
			}

			total = await tagList.CountAsync();

			// Ordering data 
			tagList = tagList.OrderBy(sort + (order == "asc" ? string.Empty : " descending"));

			// Paging
			tagList.Take(limit).Skip(offset).Cacheable();

			// Create List Of viewModel
			var tag = (await tagList.ToListAsync()).Select((x, index) => new TagList
			{
				No = (++index).ConvertToPersianString(),
				Id = x.Id,
				Name = x.Name,
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
			selectedTag.Name = tag.Name;
			selectedTag.Description = tag.Description;
		}
	}
}
