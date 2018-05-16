using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Yeast.DomainClasses.Enums;

namespace Yeast.DomainClasses.Entities
{
	public class Option
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
        [AllowHtml]
		public virtual string Value { get; set; }
        public SettingType Type { get; set; }
    }
}
