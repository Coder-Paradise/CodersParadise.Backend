using CodersParadise.DataAccess.Databases.CodersParadise.Models;
using Microsoft.EntityFrameworkCore;

namespace CodersParadise.DataAccess.Databases.CodersParadise
{
    public class CodersParadiseDbContext : DbContext
    {
        public CodersParadiseDbContext() { }

        public CodersParadiseDbContext(DbContextOptions<CodersParadiseDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Name=CodersParadiseConnection");
        //    //optionsBuilder.UseSqlServer("Server=BABABOOEY\\ARETSQL;Integrated Security=SSPI;database=CodersParadise;Trusted_Connection=true;");
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=CodersParadiseConnection");
                //optionsBuilder.UseSqlServer("Name=CodersParadiseConnection", b => b.MigrationsAssembly("CodersParadise.Api"));
            }
        }

        public DbSet<User> Users => Set<User>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.JwtId);

                entity.Property(e => e.Token).HasMaxLength(500);       
            });

        }
    }
}
