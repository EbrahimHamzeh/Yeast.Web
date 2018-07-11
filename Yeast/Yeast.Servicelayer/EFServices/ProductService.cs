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

        public ConfiguredTaskAwaitable<int> CountAsync {
            get {
                return _products.Cacheable().CountAsync().ConfigureAwait(false);
            }
        }

        public void Add(ProductAdd Product)
        {
            Product product = new Product();
            product.Name = Product.Name;
            product.Description = Product.Description;
            product.Body = Product.Body;
            product.NameEn = Product.NameEn;
            product.DescriptionEn = Product.DescriptionEn;
            product.BodyEn = Product.BodyEn;
            product.NameAr = Product.NameAr;
            product.DescriptionAr = Product.DescriptionAr;
            product.BodyAr = Product.BodyAr;
            product.NameRu = Product.NameRu;
            product.DescriptionRu = Product.DescriptionRu;
            product.BodyRu = Product.BodyRu;
            product.Price = Product.Price;

            string[] images = Product.ImageName.Split('|');

            if (images.Length >= 1) product.Image1 = images[0];
            if (images.Length >= 2) product.Image2 = images[1];
            if (images.Length >= 3) product.Image3 = images[2];

            _products.Add(product);
        }

        public Product Find(int id)
        {
            return _products.Find(id);
        }

        public ProductEdit FindForEdit(int id)
        {
            Product Product = _products.Find(id);
            return new ProductEdit
            {
                Name = Product.Name,
                Description = Product.Description,
                Body = Product.Body,
                NameEn = Product.NameEn,
                DescriptionEn = Product.DescriptionEn,
                BodyEn = Product.BodyEn,
                NameAr = Product.NameAr,
                DescriptionAr = Product.DescriptionAr,
                BodyAr = Product.BodyAr,
                NameRu = Product.NameRu,
                DescriptionRu = Product.DescriptionRu,
                BodyRu = Product.BodyRu,
                Image1 = Product.Image1,
                Image2 = Product.Image2,
                Image3 = Product.Image3,
                ImageName = string.Format("{0}|{1}|{2}", Product.Image1, Product.Image2, Product.Image3),
                MetaData = Product.MetaData,
                Price = Product.Price
            };
        }

        public async Task<IList<Model.FrontEnd.Product>> GetAllAsync()
        {
            return await _products.AsNoTracking().Select(x => new Model.FrontEnd.Product {
                Id = x.Id,
                Body = x.Body,
                Description = x.Description,
                Image1 = x.Image1,
                Image2 = x.Image2,
                Image3 = x.Image3,
                Name = x.Name,
                Price = x.Price }).Cacheable().ToListAsync();
        }
        public IList<Product> GetAllproduct()
        {
            //_products.OrderByDescending(x => new Product { Id = x.Id, Body = x.Body, Description = x.Description, Image1 = x.Image1, Image2 = x.Image2, Image3 = x.Image3, Name = x.Name, Price = x.Price }).OrderByDescending(m => m.Id).ToList();
            return _products.OrderByDescending(x => x.Id).ToList();
        }
        public List<Model.FrontEnd.Product> GetByCulterProduct()
        {
            string cultur = CultureHelper.GetCurrentNeutralCulture();

            List<Model.FrontEnd.Product> list = _products.AsNoTracking().Select(x => new Model.FrontEnd.Product
            {
                Body = cultur == "fa" ? x.Body : cultur == "en" ? x.BodyEn : cultur == "ar" ? x.BodyAr : cultur == "ru" ? x.BodyRu : "",
                Name = cultur == "fa" ? x.Name : cultur == "en" ? x.NameEn : cultur == "ar" ? x.NameAr : cultur == "ru" ? x.NameRu : "",
                Description = cultur == "fa" ? x.Description : cultur == "en" ? x.DescriptionEn : cultur == "ar" ? x.DescriptionAr : cultur == "ru" ? x.DescriptionRu : "",
                Image1 = x.Image1,
                Image2 = x.Image2,
                Image3 = x.Image3,
                Price = x.Price
            }).ToList();
            return list;
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
