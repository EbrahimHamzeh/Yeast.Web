using EFSecondLevelCache;
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
	public class EmailTemplateService : IEmailTemplateService
	{
		private readonly IDbSet<EmailTemplate> _emailTemplate;
		public EmailTemplateService(IUnitOfWork uow)
		{
            _emailTemplate = uow.Set<EmailTemplate>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _emailTemplate.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(EmailTemplateAdd emailTemplate)
		{
            _emailTemplate.Add(new EmailTemplate { Title = emailTemplate.Title, Description = emailTemplate.Description, Body = emailTemplate.Body });
		}

		public EmailTemplate Find(int id)
		{
			return _emailTemplate.Find(id);
		}

		public EmailTemplateEdit FindForEdit(int id)
		{
			EmailTemplate tag = _emailTemplate.Find(id);
			return new EmailTemplateEdit
            {
				Title = tag.Title,
				Description = tag.Description,
                Body = tag.Body
			};
		}

		public async Task<IList<EmailTemplate>> GetAllAsync()
		{
			return await _emailTemplate.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<EmailTemplateList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<EmailTemplate> emailTemplateList = _emailTemplate.AsNoTracking();
			int total = 0;

			// Search
			//tagList = tagList.ApplySearch(model);

			total = await emailTemplateList.CountAsync().ConfigureAwait(false);

            // Ordering data 
            emailTemplateList = emailTemplateList.ApplyOrdering(model);

            // Paging And Save Cach
            emailTemplateList = emailTemplateList.ApplyPaging(model).Cacheable();
			model.offset = model.offset - 1;
			// Create List Of viewModel
			var tag = (await emailTemplateList.ToListAsync()).Select((x, index) => new EmailTemplateList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
				Description = x.Description
			});

			return new DataTableList<EmailTemplateList> { rows = tag.ToList(), total = total };
		}

		public void Remove(int id)
		{
            _emailTemplate.Remove(_emailTemplate.Find(id));
		}

		public void Update(EmailTemplateEdit model, int id)
		{
            EmailTemplate emailTemplate = _emailTemplate.Find(id);
            emailTemplate.Title = model.Title;
            emailTemplate.Description = model.Description;
            emailTemplate.Body = model.Body;
        }

		public SelectList DropDownList(List<int> tagSelectedId = null)
		{
			List<SelectListItem> selectListItemList;
			if (tagSelectedId == null)
			{
				selectListItemList = _emailTemplate.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();
			}
			else
			{
				selectListItemList = _emailTemplate.Select(x => new SelectListItem
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
