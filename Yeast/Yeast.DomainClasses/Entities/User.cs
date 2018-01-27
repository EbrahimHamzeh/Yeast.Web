
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
	{
		public User()
		{
			CreatedDate = DateTime.Now;
		}
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual string Description { get; set; }
		public virtual string AvatarPath { get; set; }
		public virtual DateTime? BirthDay { get; set; }
		public virtual string Password { get; set; }
		public virtual string IP { get; set; }
		public virtual bool IsBaned { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? BanedDate { get; set; }
		public virtual DateTime? LastLoginDate { get; set; }
		public virtual DateTime? LastPasswordChange { get; set; }
		public virtual DateTime? LastActivity { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
	}
}
