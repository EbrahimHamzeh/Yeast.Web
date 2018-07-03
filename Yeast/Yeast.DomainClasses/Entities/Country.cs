using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Country
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int ViewCount { get; set; }
    }
}
