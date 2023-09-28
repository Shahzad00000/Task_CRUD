using Microsoft.EntityFrameworkCore;
using Task_CRUD.Models;

namespace Task_CRUD.Context
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
            
        }
        public DbSet<Employee> Employees { get; set; }  
    }
}
