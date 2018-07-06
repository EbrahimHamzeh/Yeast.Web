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
using Yeast.Model.FrontEnd;

namespace Yeast.Servicelayer.EFServices
{
	public class ContactUsService : IContactUsService
    {
		private readonly IDbSet<ContactUs> _contactUs;
		public ContactUsService(IUnitOfWork uow)
		{
            _contactUs = uow.Set<ContactUs>();
		}

		public ContactUsViewModel View(int id)
		{
            ContactUs contactUs = _contactUs.Find(id);
            return new ContactUsViewModel
            {
                Title = contactUs.Title,
                Mobile = contactUs.Mobile,
                Message = contactUs.Message,
                FullName = contactUs.FullName,
                Email = contactUs.Email
            };
		}

		public async Task<DataTableList<ContactUsList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<ContactUs> tagList = _contactUs.AsNoTracking();
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
			var tag = (await tagList.ToListAsync()).Select((x, index) => new ContactUsList
            {
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
                FullName = x.FullName,
                Mobile = x.Mobile,
                Email = x.Email,
                Message = x.Message
            });

			return new DataTableList<ContactUsList> { rows = tag.ToList(), total = total };
		}

		public void Remove(int id)
		{
            _contactUs.Remove(_contactUs.Find(id));
		}

        public void Add(ContactUsViewModel model)
        {
            _contactUs.Add(new ContactUs
            {
                Email = model.Email,
                FullName = model.FullName,
                Message = model.Message,
                Mobile = model.Mobile,
                Title = model.Title
            });
        }

        public ContactUs Find(int id)
        {
            return _contactUs.Find(id);
        }
    }
}
