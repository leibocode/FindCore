using FindCore.UserAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindCore.UserAPI.Data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //用户配置
            modelBuilder.Entity<AppUser>(b =>
            {
                b.ToTable("Users");
                b.HasKey(a => a.Id);
            });

            //用户属性配置
            modelBuilder.Entity<UserProperty>(b =>
            {
                b.ToTable("UserProperties");
                b.Property(a => a.Key).HasMaxLength(100);
                b.Property(a => a.Value).HasMaxLength(100);
                b.HasKey(a => new {a.AppUserId,a.Key,a.Value });
            });

            //用户标签 
            modelBuilder.Entity<UserTag>(b =>
            {
                b.ToTable("UserTag");
                b.Property(a => a.Tag).HasMaxLength(100);
                b.HasKey(a => new {a.AppUserId,a.Tag });
            });

            modelBuilder.Entity<BPFile>(b =>
            {
                b.ToTable("BPFiles");
                b.HasKey(a => a.Id);
            });

            base.OnModelCreating(modelBuilder);
        }

       public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<UserProperty> AppUserProperties { get; set; }

        public DbSet<UserTag> AppUserTags { get; set; }

        public DbSet<BPFile> BPFiles { get; set; }
    }
}
