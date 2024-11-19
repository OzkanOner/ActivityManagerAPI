using ActivityManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ActivityManagerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}