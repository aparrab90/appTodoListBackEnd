using apiTasks.Modelos;
using Microsoft.EntityFrameworkCore;

namespace apiTasks.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        //Agregar los modelos aquí
        public DbSet<Category> Category { get; set; }
        public DbSet<StepTask> StepTask { get; set; }
        public DbSet<Modelos.Task> Task { get; set; }
        public DbSet<User> User { get; set; }
    }
}