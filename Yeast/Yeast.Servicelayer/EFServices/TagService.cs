using EFSecondLevelCache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;
using Yeast.Model.Admin;
using System.Threading.Tasks;

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
			IQueryable<TagList> tagList;
			tagList = _tags.AsNoTracking().Select(x => new TagList { No = 1, Title = x.Name, Description = x.Description, Id = x.Id });

			if (!string.IsNullOrEmpty(search))
			{
				tagList = tagList.Where(x => x.Title.Contains(search) || x.Description.Contains(search) || x.Id.ToString().Contains(search));
			}

			if (order == "asc")
			{
				tagList = tagList.OrderBy(x => x.Title);
			}
			else
			{
				tagList = tagList.OrderByDescending(x => x.Title);
			}

			tagList.Take(limit).Skip(offset).Cacheable();

			return new DataTableList<TagList> { rows = await tagList.ToListAsync(), total = await tagList.CountAsync() };
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
