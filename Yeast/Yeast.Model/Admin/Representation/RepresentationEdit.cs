using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class RepresentationEdit
	{
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
    }
}
