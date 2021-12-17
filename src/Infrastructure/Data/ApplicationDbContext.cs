using Domain.Entity;
using Infrastructure.DataConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        //public DbSet<ApplicationUser> AppUser { get; set; }
        public DbSet<PersonalTask> PersonalTasks { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }
        public DbSet<Stock_NhomNganh> Stock_NhomNganh { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<NBAMatch> NBAMatches { get; set; }
        public DbSet<BlogSerie> BlogSerie { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new PostTagConfiguration());
            builder.ApplyConfiguration(new BlogCategoryConfiguration());
        }
    }
}
