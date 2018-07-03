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
	public class StatisticsService : IStatisticsService
    {
		private readonly IDbSet<Statistics> _statistics;
		private readonly IDbSet<Country> _countries;

        public StatisticsService(IUnitOfWork uow)
        {
            _statistics = uow.Set<Statistics>();
            _countries = uow.Set<Country>();
        }

        public ConfiguredTaskAwaitable<int> CountAsync => throw new NotImplementedException();

        public void Update(Statistics Statistics)
        {
            throw new NotImplementedException();
        }

        Statistics IStatisticsService.Find(int id)
        {
            throw new NotImplementedException();
        }

        Task<IList<Statistics>> IStatisticsService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public SelectList DropDownList(List<int> StatisticsSelectedId = null)
        {
            throw new NotImplementedException();
        }

        public StatisticsViewModel StatisticsCount()
        {
            StatisticsViewModel statisticsViewModel = new StatisticsViewModel();
            statisticsViewModel.TodayVisits = _statistics.Where(x => x.DateStamp.Day == DateTime.Now.Day).Count();
            statisticsViewModel.TotallVisits = _statistics.Count();
            statisticsViewModel.UniquVisitors = _statistics.GroupBy(ta => ta.IpAddress).Select(ta => ta.Key).Count();

            statisticsViewModel.CountryList = _countries.Select(x => new CountryList { Id = x.Id, CountryCode = x.CountryCode, CountryName = x.CountryName, ViewCount = x.ViewCount }).ToList();

            return statisticsViewModel;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
