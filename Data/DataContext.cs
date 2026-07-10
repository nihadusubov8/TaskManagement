using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Models;

namespace TaskManagementApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Sadece bu iki setir olmalidir:
        public DbSet<User> Users { get; set; }
        public DbSet<TodoTasks> TodoTasks { get; set; }
    }
}