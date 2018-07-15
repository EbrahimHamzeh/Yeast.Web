using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class CustomerAdd
	{
        public virtual string Title { get; set; }
        public virtual string Name { get; set; }
        public virtual string Family { get; set; }
        public virtual string Email { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Telephone { get; set; }
        public virtual string Description { get; set; }
    }
}
