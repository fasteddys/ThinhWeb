using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataConfiguration
{
    public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
            builder.HasKey(x => new { x.PostId, x.TagId });
            builder.HasOne<Post>(x => x.Post)
                .WithMany(y => y.PostTags)
                .HasForeignKey(x => x.PostId);

            builder.HasOne<Tag>(x => x.Tag)
                .WithMany(y => y.PostTags)
                .HasForeignKey(x => x.TagId);
        }
    }
}
