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

            });

            base.OnModelCreating(modelBuilder);
        }

       public DbSet<AppUser> Users { get; set; }

        public DbSet<UserProperty> UserProperties { get; set; }

        public DbSet<UserTag> UserTags { get; set; }
    }
}
