using Microsoft.EntityFrameworkCore;

namespace GestActives
{
    public class GestActivesContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Person> Persons { get; set; }

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

                entity.HasData(new Company { IdCompany = 1, Name = "Monbake", External = false, Telephone = "948235150", Email = "MantenimientoAlican@monbake.com" });
                entity.HasData(new Company { IdCompany = 2, Name = "Sin Nombre", External = false, Telephone = "", Email = "" });
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson);
                entity.Property(e => e.IdPerson).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Surname).IsRequired();

                entity.HasOne(e => e.Enterprise)
                      .WithMany()
                      .HasForeignKey("CompanyId")
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Telephone).IsRequired(false);
                entity.Property(e => e.Email).IsRequired(false);

                entity.HasDiscriminator<string>("Discriminator")
                      .HasValue<InternalAssembler>("Montador Propio")
                      .HasValue<ExternalAssembler>("Montador Externo")
                      .HasValue<CommercialAgent>("Comercial");

                entity.HasData(new
                {
                    IdPerson = 1,
                    Name = "Jorge Juan",
                    Surname = "Guijarro Rabasco",
                    CompanyId = 1,
                    Telephone = (string)null,
                    Email = (string)null,
                    Discriminator = "Montador Propio"
                });
            });
        }
    }
}