using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // CreatedByUserId əlaqəsi
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.CreatedByUser)
                .WithMany(u => u.Assignments)
                .HasForeignKey(a => a.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // AssigneUserId əlaqəsi
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.AssigneUser)
                .WithMany()
                .HasForeignKey(a => a.AssigneUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}