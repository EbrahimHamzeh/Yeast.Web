using EFSecondLevelCache;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Yeast.Utilities;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Servicelayer.Interfaces;
using Yeast.Utilities.Helpers;

namespace Yeast.Servicelayer.EFServices
{
	public class ProductService : IProductService
	{
		private readonly IDbSet<Product> _products;

		public ProductService(IUnitOfWork uow)
		{
			_products = uow.Set<Product>();
		}

		public ConfiguredTaskAwaitable<int> CountAsync
		{
			get
			{
				return _products.Cacheable().CountAsync().ConfigureAwait(false);
			}
		}

		public void Add(ProductAdd Product)
		{
            _products.Add(new Product { Name = Product.Name, Description = Product.Description, Body = Product.Body, Price = Product.Price, Images = Filepond.SaveArrayImageToJsonString(Product.Image1) });
		}

		public Product Find(int id)
		{
			return _products.Find(id);
		}

		public ProductEdit FindForEdit(int id)
		{
			Product Product = _products.Find(id);
			return new ProductEdit {
				Name = Product.Name,
				Description = Product.Description,
				Body = Product.Body,
                ImageJSONs = Filepond.DeserializeImageUpload(Product.Images),
                MetaData = Product.MetaData,
                Price = Product.Price
            };
		}

		public async Task<IList<Product>> GetAllAsync()
		{
			return await _products.AsNoTracking().Cacheable().ToListAsync();
		}

		public async Task<DataTableList<ProductList>> GetDataTableAsync(string search = "", string sort = "Name", string order = "asc", int offset = 0, int limit = 10)
		{
			IQueryable<Product> ProductList = _products.AsNoTracking();
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
			_products.Remove(_products.Find(id));
		}

		public void Update(Product Product)
		{
			Product selectedProduct = _products.Find(Product.Id);
			selectedProduct.Name = Product.Name;
			selectedProduct.Description = Product.Description;
		}
	}
}
