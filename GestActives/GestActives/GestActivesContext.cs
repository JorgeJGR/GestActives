using Microsoft.EntityFrameworkCore;

namespace GestActives
{
    public class GestActivesContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LENOVO_DAM;Database=GestActivesDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.IdCompany);
                entity.Property(e => e.IdCompany).ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Name).IsUnique();

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.External).IsRequired();
                entity.Property(e => e.Telephone).IsRequired(false);
                entity.Property(e => e.Email).IsRequired(false);

                entity.HasData(new Company { IdCompany = 1, Name = "Monbake", External = false });
            });
        }
    }
}