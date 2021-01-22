using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NETweet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETweet.Data
{
    public class ApplicationDbContext : IdentityDbContext<NETUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tweet>().HasKey(k => k.ID);
            modelBuilder.Entity<Follow>().HasKey(k => new { k.UserID, k.Following });
            modelBuilder.Entity<React>().HasKey(k => new { k.UserID, k.TweetID });
        }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<React> React { get; set; }
    }
}
