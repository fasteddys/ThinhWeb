using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataConfiguration
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<Blog_BlogCategory>
    {
        public void Configure(EntityTypeBuilder<Blog_BlogCategory> builder)
        {
            builder.HasKey(x => new { x.PostId, x.CategoryId });
            builder.HasOne<Post>(x => x.Post)
                .WithMany(y => y.Categories)
                .HasForeignKey(x => x.PostId);

            builder.HasOne<BlogCategory>(x => x.Category)
                .WithMany(y => y.Posts)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
