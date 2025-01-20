using Microsoft.EntityFrameworkCore;
using SimpleAPI.Models;

namespace SimpleAPI.Data
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext() : base(new DbContextOptionsBuilder<SQLiteContext>().Options) { }
        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }




    }
}
