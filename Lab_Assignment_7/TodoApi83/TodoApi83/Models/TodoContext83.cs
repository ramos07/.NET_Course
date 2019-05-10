using Microsoft.EntityFrameworkCore;

namespace TodoApi83.Models
{
    public class TodoContext83 : DbContext
    {
        public TodoContext83(DbContextOptions<TodoContext83> options)
            : base(options)
        {
        }

        public DbSet<TodoItem83> TodoItems { get; set; }
    }
}