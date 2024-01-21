using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using todoitems_api_docker.Models;

namespace todoitems_api_docker.Data
{
    public class TodoContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        public override DbSet<Role> Roles => Set<Role>();

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // required by IdentityDbContext

            modelBuilder.Entity<TodoItem>().HasData(new List<TodoItem>
            {
                new TodoItem
                {
                    Id = 1, Name = "Clean living room", IsComplete = false
                }
            });

            modelBuilder.Entity<Role>().HasData(new List<Role>
            {
                new Role
                {
                    Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN"
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER"
                },
            });
        }
    }
}
