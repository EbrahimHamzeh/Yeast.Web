﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class CategoryList
	{
		public string No { get; set; }
		public int Id { get; set; }
		public string Title { get; set; }
		public int? Order { get; set; }
		public string  Description { get; set; }
	}
}