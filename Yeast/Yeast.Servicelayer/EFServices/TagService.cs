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

namespace Yeast.Servicelayer.EFServices
{
	public class TagService : ITagService
	{
		private readonly IDbSet<Tag> _tags;
		public TagService(IUnitOfWork uow)
		{
			_tags = uow.Set<Tag>();
		}
		public int Count
		{
			get
			{
				return _tags.Cacheable().Count();
			}
		}

		public void Add(TagAdd tag)
		{
			_tags.Add(new Tag { Name = tag.Title, Description = tag.Description });
		}

		public Tag Find(int id)
		{
			return _tags.Find(id);
		}

		public async Task<IList<Tag>> GetAll()
		{
			return await _tags.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<TagList>> GetDataTable(string search = "", string sort = "Title", string order = "asc", int offset = 0, int limit = 10)
		{
			IQueryable<Tag> tagList;
			tagList = _tags.AsNoTracking();

			if (!string.IsNullOrEmpty(search))
			{
				tagList = tagList.Where("Name.Contains(@0) or Description.Contains(@0)", search);
			}
			if (order == "asc")
			{
				tagList = tagList.OrderBy(sort);

			}
			else
			{
				tagList = tagList.OrderBy(sort + " descending");
			}

			tagList.Take(limit).Skip(offset).Cacheable();

			var tag = tagList.AsEnumerable().Select((x, index) => new TagList
			{
				No = (++index).ConvertToPersianString(),
				Title = x.Name,
				Description = x.Description,
				Id = x.Id
			});

			return new DataTableList<TagList> { rows = tag.ToList(), total = await tagList.CountAsync() };
		}

		public void Remove(int id)
		{
			_tags.Remove(_tags.Find(id));
		}

		public void Update(TagEdit tag)
		{
			Tag selectedTag = _tags.Find(tag.Id);
			selectedTag.Name = tag.Title;
			selectedTag.Description = tag.Description;
		}
	}
}
