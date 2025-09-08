using CoreMVCLibraryProject.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreMVCLibraryProject.Models.Context
{
    public class LibraryContext :DbContext
    {

        public LibraryContext()
        {
            
        }

        public LibraryContext(DbContextOptions<LibraryContext>options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=M204;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }


        public DbSet<Ogrenci> Ogrencis { get; set; }
        public DbSet<Kitap> Kitaps { get; set; }
        public DbSet<Islem> Islems { get; set; }
        public DbSet<Tur> Turs { get; set; }
        public DbSet<Yazar> Yazars { get; set; }

    }
}
