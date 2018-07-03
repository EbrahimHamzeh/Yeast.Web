using System.Collections.Generic;
using System.Threading.Tasks;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using System.Runtime.CompilerServices;
using Yeast.Utilities.BootstrapTable;
using System.Web.Mvc;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IStatisticsService
	{
		ConfiguredTaskAwaitable<int> CountAsync { get; }
		//void Add(StatisticsAdd Statistics);
		void Remove(int id);
		void Update(Statistics Statistics);
        Statistics Find(int id);
        //StatisticsEdit FindForEdit(int id);
		Task<IList<Statistics>> GetAllAsync();
		//Task<DataTableList<StatisticsList>> GetDataTableAsync(PagedQueryViewModel model);
		SelectList DropDownList(List<int> StatisticsSelectedId = null);
        StatisticsViewModel StatisticsCount();
    }
}
