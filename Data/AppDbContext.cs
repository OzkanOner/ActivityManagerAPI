using ActivityManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;

namespace ActivityManagerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Activity>()
                .HasIndex(a => a.CreatedUserId)
                .HasDatabaseName("IX_Activity_CreatedUserId");

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.CreatedUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActivities)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.Activity)
                .WithMany(a => a.UserActivities)
                .HasForeignKey(ua => ua.ActivityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserActivity>()
                .HasOne(ua => ua.AssignerUser)
                .WithMany(u => u.AssignedActivities)
                .HasForeignKey(ua => ua.AssignerUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}