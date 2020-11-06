using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebApi.Contexts
{
    public class SQLiteContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = WebAPI.db;");
        }
        public DbSet<User> User { get; set; }
    }
}