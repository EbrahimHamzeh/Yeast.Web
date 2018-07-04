using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yeast.DomainClasses.Entities
{
	public class Statistics
    {
        [Key]
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string UserOs { get; set; }
        public string Referer { get; set; }
        public string PageViewed { get; set; }
        public DateTime DateStamp { get; set; }
    }
}
