using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic;

namespace Yeast.Utilities.BootstrapTable
{
	public static class IQueryableExtensions
	{
		private const string searchContinsterm = ".Contains(@0) ";
		private const string searchOrterm = "or ";

		/// <summary>
		/// Dynamic Paging Up Level Performance () => model.Offset for Sql
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="model">Bootstrap Table Model</param>
		/// <returns></returns>
		public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IPagedQueryModel model)
		{
			if (model.offset < 1) model.offset = 1;
			if (model.limit < 10) model.limit = 10;
			return query
				.Skip(() => model.offset)
				.Take(() => model.limit);
		}

		/// <summary>
		/// Dynamic Ordering 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="model">Bootstrap Table Model</param>
		/// <returns></returns>
		public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query,IPagedQueryModel model)
		{
			if (string.IsNullOrWhiteSpace(model.sort) || model.sort == null)
			{
				return query.OrderBy("Id");
			}

			return query.OrderBy(model.sort + (model.order == "asc" ? string.Empty : " descending"));
		}

		/// <summary>
		/// serch model.search term in Propertis query one by one
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query">IQueryable</param>
		/// <param name="model">Bootstrap Table Model</param>
		/// <returns></returns>
		public static IQueryable<T> ApplySearch<T>(this IQueryable<T> query, IPagedQueryModel model)
		{
			if (string.IsNullOrWhiteSpace(model.search))
			{
				return query;
			}
			var properties = typeof(T).GetProperties();
			string searchQuery = string.Empty;

			foreach (var item in properties)
			{
				// propertie no show or no search
				if (item.Name == "Id" || item.Name == "Guid") continue;
				if (searchQuery != string.Empty) searchQuery += searchOrterm;
				searchQuery +=  item.Name + searchContinsterm;
			}

			if (string.IsNullOrEmpty(searchQuery))
			{
				return query;
			}

			return query.Where(searchQuery,model.search);
		}
	}
}
