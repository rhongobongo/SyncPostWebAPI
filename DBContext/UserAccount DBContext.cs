using SyncPostWebAPI.Model;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace SyncPostWebAPI.DBContext
{
    public class SyncPostDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Templates> Templates { get; set; }

        public SyncPostDbContext(DbContextOptions<SyncPostDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Templates>()
                .Property(t => t.template_id)
                .ValueGeneratedOnAdd(); // Assuming template_id is the primary key
        }
    }
}
