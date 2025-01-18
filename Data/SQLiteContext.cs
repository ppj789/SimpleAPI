using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class SQLiteContext : DbContext
    {

        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
