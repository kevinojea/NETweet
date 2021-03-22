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
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Follow>()
                .HasKey(k => new { k.FollowingId, k.FollowerId });

            modelBuilder.Entity<Follow>()
                .HasOne(u => u.Following)
                .WithMany(u => u.Follower)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
                .HasOne(u => u.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(u => u.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<React>()
                .HasKey(k => new { k.UserID, k.TweetRefID });

        }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<React> React { get; set; }
    }
}

