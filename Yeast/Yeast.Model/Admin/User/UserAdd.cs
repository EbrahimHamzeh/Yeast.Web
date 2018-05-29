using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yeast.Model.Admin
{
	public class UserAdd
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string AvatarPath { get; set; }
		public DateTime BirthDay { get; set; }
		public string IP { get; set; }
		public bool IsBaned { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string UserName { get; set; }
		public string Description { get; set; }
	}
}
