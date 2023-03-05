using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option):base(option)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
