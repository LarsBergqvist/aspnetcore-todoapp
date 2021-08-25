using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TodoDbContext: DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        internal DbSet<TodoListEntity> TodoLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
          => options.UseSqlServer("Server=.\\SQLEXPRESS;Database=todos;Trusted_Connection=True;MultipleActiveResultSets=true");

    }

}
