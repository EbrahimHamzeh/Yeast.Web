using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class StatisticsViewModel
    {
        public List<CountryList> CountryList { get; set; }
        public int TodayVisits { get; set; }
        public int TotallVisits { get; set; }
        public int UniquVisitors { get; set; }
    }

    public class CountryList
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int ViewCount { get; set; }
    }
}
