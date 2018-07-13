using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Representation
    {
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual string Telephone { get; set; }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
		public virtual string Latitude { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
		public virtual string Longitude { get; set; }
	}
}
