using CodersParadise.DataAccess.Databases.CodersParadise.Models;
using Microsoft.EntityFrameworkCore;

namespace CodersParadise.DataAccess.Databases.CodersParadise
{
    public class CodersParadiseDbContext : DbContext
    {
        public CodersParadiseDbContext(DbContextOptions<CodersParadiseDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Name=CodersParadiseConnection");
        }

        public DbSet<User> Users => Set<User>();


    }
}
