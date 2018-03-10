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
	public class ProductService : IProductService
	{
		private readonly IDbSet<Product> _Products;

		public ProductService(IUnitOfWork uow)
		{
			_Products = uow.Set<Product>();
		}

		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _Products.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(ProductAdd Product)
		{
			_Products.Add(new Product { Name = Product.Name, Description = Product.Description });
		}

		public Product Find(int id)
		{
			return _Products.Find(id);
		}

		public ProductEdit FindForEdit(int id)
		{
			Product Product = _Products.Find(id);
			return new ProductEdit {
				Name = Product.Name,
				Description = Product.Description
			};
		}

		public async Task<IList<Product>> GetAllAsync()
		{
			return await _Products.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<ProductList>> GetDataTableAsync(string search = "", string sort = "Name", string order = "asc", int offset = 0, int limit = 10)
		{
			IQueryable<Product> ProductList = _Products.AsNoTracking();
			int total = 0;

			// Search
			if (!string.IsNullOrEmpty(search))
			{
				ProductList = ProductList.Where("Name.Contains(@0) or Description.Contains(@0)", search);
			}

			total = await ProductList.CountAsync();

			// Ordering data 
			ProductList = ProductList.OrderBy(sort + (order == "asc" ? string.Empty : " descending"));

			// Paging
			ProductList.Take(limit).Skip(offset).Cacheable();

			// Create List Of viewModel
			var Product = (await ProductList.ToListAsync()).Select((x, index) => new ProductList
			{
				No = (++index).ConvertToPersianString(),
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			});

			return new DataTableList<ProductList> { rows = Product.ToList(), total = total };
		}

		public void Remove(int id)
		{
			_Products.Remove(_Products.Find(id));
		}

		public void Update(Product Product)
		{
			Product selectedProduct = _Products.Find(Product.Id);
			selectedProduct.Name = Product.Name;
			selectedProduct.Description = Product.Description;
		}
	}
}
