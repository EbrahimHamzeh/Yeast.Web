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
	public class EmailsService : IEmailService
	{
		private readonly IDbSet<Email> _email;

		public EmailsService(IUnitOfWork uow)
		{
            _email = uow.Set<Email>();
		}

		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _email.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(EmailAdd tag)
		{
            _email.Add(new Email { Title = tag.Title, Description = tag.Description, SendTime = DateTime.Now });
		}

		public async Task<IList<Email>> GetAllAsync()
		{
			return await _email.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<EmailList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Email> tagList = _email.AsNoTracking();
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
			var tag = (await tagList.ToListAsync()).Select((x, index) => new EmailList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
				Description = x.Description
			});

			return new DataTableList<EmailList> { rows = tag.ToList(), total = total };
		}

		public void Remove(int id)
		{
            _email.Remove(_email.Find(id));
		}

        public Email Find(int id)
        {
            return _email.Find(id);
        }
    }
}
