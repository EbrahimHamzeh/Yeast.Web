﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;

namespace Yeast.Servicelayer.Interfaces
{
	public interface ITagService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		void Add(TagAdd tag);
		void Remove(int id);
		void Update(Tag tag);
		Tag Find(int id);
		TagEdit FindForEdit(int id);
		Task<IList<Tag>> GetAllAsync();
		Task<DataTableList<TagList>> GetDataTableAsync(PagedQueryViewModel model);
	}
}