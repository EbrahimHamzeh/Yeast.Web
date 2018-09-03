using System;
using System.Collections.Generic;

namespace Yeast.DomainClasses.Entities
{
	public class Email
    {
		public virtual int Id { get; set; }
		public virtual string Title { get; set; }
        public virtual DateTime SendTime { get; set; }
        public virtual string Body { get; set; }
		public virtual string Description { get; set; }
		public virtual string AttachFile { get; set; }
		public virtual string EmailAddress { get; set; }
        public virtual User User { get; set; }
        public virtual EmailTemplate EmailTemplate { get; set; }
    }
}
