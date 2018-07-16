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
	public class CustomerService : ICustomerService
    {
		private readonly IDbSet<Customer> _categories;

		public CustomerService(IUnitOfWork uow)
		{
			_categories = uow.Set<Customer>();
		}
		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _categories.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(CustomerAdd customer)
		{
			_categories.Add(new Customer {
                Title = customer.Title,
                Description = customer.Description,
                Name = customer.Name,
                Family = customer.Family,
                Email = customer.Email,
                Mobile = customer.Mobile,
                Telephone = customer.Telephone,
            });
		}

        public Customer Find(int id)
		{
			return _categories.Find(id);
		}

		public CustomerEdit FindForEdit(int id)
		{
			Customer customer = _categories.Find(id);
			return new CustomerEdit
			{
				Title = customer.Title,
				Description = customer.Description,
                Name = customer.Name,
                Family = customer.Family,
                Email = customer.Email,
                Mobile = customer.Mobile,
                Telephone = customer.Telephone,
            };
		}

		public async Task<IList<Customer>> GetAllAsync()
		{
			return await _categories.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<CustomerList>> GetDataTableAsync(PagedQueryViewModel model)
		{
			IQueryable<Customer> customerList = _categories.AsNoTracking();
			int total = 0;

			// Search
			//tagList = tagList.ApplySearch(model);

			total = await customerList.CountAsync().ConfigureAwait(false);

			// Ordering data 
			customerList = customerList.ApplyOrdering(model);

			// Paging And Save Cach
			customerList = customerList.ApplyPaging(model).Cacheable();
			model.offset = model.offset - 1;
			// Create List Of viewModel
			var customer = (await customerList.ToListAsync()).Select((x, index) => new CustomerList
			{
				No = (++index + model.offset).ConvertToPersianString(),
				Id = x.Id,
				Title = x.Title,
				Description = x.Description,
                Name = x.Name,
                Family = x.Family,
                Email = x.Email,
                Mobile = x.Mobile,
                Telephone = x.Telephone,
            });

			return new DataTableList<CustomerList> { rows = customer.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_categories.Remove(_categories.Find(id));
		}

		public void Update(Customer customer)
		{
			Customer selectedcustomer = _categories.Find(customer.Id);
			selectedcustomer.Title = customer.Title;
			selectedcustomer.Description = customer.Description;
            selectedcustomer.Name = customer.Name;
            selectedcustomer.Family = customer.Family;
            selectedcustomer.Email = customer.Email;
            selectedcustomer.Mobile = customer.Mobile;
            selectedcustomer.Telephone = customer.Telephone;
		}
	}
}
