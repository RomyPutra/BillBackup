using System;
using Microsoft.EntityFrameworkCore;
using Billboard360.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billboard360.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        //public DbSet<Position> Position { get; set; }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<LogForgotPassword> LogForgotPassword { get; set; }
        public DbSet<LogActivity> LogActivity { get; set; }
        public DbSet<UserBank> UserBank { get; set; }
        public DbSet<TitikLokasi> TitikLokasi { get; set; }
        public DbSet<TitikLokasiDetail> TitikLokasiDetail { get; set; }
        public DbSet<TitikLokasiPrice> TitikLokasiPrice { get; set; }
        public DbSet<TitikLokasiImage> TitikLokasiImage { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<BillboardType> BillboardType { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookDetail> BookDetail { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportDocument> ReportImage { get; set; }
        public DbSet<LogRateSite> LogRateSite { get; set; }

        
        public DbSet<SP_GetTitikLokasiWithDistance> SP_GetTitikLokasiWithDistance { get; set; } 

        public DbSet<Compare> Compare { get; set; }

        public DbSet<City> City { get; set; }
        public DbSet<Province> Province { get; set; }

        public DbSet<MidTransLog> MidTransLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            bool skipSeeder = false;

            if (!skipSeeder)
            {
                modelBuilder.Seed();
            }

        }
    }
}
