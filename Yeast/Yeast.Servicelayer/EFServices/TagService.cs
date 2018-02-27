using EFSecondLevelCache;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;
using Yeast.Model.Admin;

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

		public IList<Tag> GetAll()
		{
			return _tags.AsNoTracking().Cacheable().ToList();
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
