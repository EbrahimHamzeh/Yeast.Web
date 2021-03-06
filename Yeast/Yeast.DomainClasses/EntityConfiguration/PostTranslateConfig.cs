﻿using System.Data.Entity.ModelConfiguration;
using Yeast.DomainClasses.Entities;

namespace Yeast.DomainClasses.EntityConfiguration
{
	public class PostTranslateConfig : EntityTypeConfiguration<PostTranslate>
	{
		public PostTranslateConfig()
		{
			Property(x => x.Body).IsMaxLength().IsOptional();
			Property(x => x.Title).HasMaxLength(400).IsOptional();
			Property(x => x.Keyword).HasMaxLength(500).IsOptional();
			Property(x => x.Title).HasMaxLength(200);
		}
	}
}
