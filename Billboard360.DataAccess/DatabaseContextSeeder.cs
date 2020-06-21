using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Billboard360.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Billboard360.Helper;

namespace Billboard360.DataAccess
{
    public static class DatabaseContextSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            string passwordStandard = "1234";

            string roleAdmin = "B9C78BA1-F5E7-48CF-AE17-D0C859365905";
            string roleSPV = "A7E4D5E0-E709-4612-89DF-88FD2146CD75";
            string roleMediaBuyer = "3D526BF5-93BB-4C16-B2B9-39E1BE9E5207";
            string roleMediaOwner = "86D43CC3-F6E0-4312-86C6-B6879B9EAE87";

            string userIDAdmin = "3E6B87EF-6BC6-4A96-A105-7A2FEC6EC3A1";

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    ID = Guid.Parse(roleAdmin),
                    Name = "ADM",
                    NormalizedName = "Admin",
                    ConcurrencyStamp = ""
                },

                new Role
                {
                    ID = Guid.Parse(roleSPV),
                    Name = "SPV",
                    NormalizedName = "Supervisor",
                    ConcurrencyStamp = ""
                },
                new Role
                {
                    ID = Guid.Parse(roleMediaBuyer),
                    Name = "MDB",
                    NormalizedName = "Media Buyer",
                    ConcurrencyStamp = ""
                },
                new Role
                {
                    ID = Guid.Parse(roleMediaOwner),
                    Name = "MDO",
                    NormalizedName = "Media Owner",
                    ConcurrencyStamp = ""
                }

                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = Guid.Parse(userIDAdmin),
                    FirstName = "Admin",
                    LastName = "Admin",
                    PhotoUrl = "",
                    UserName = "admin@admin.com",
                    PasswordHash = passwordStandard.ConvertToMD5(),
                    EmailConfirmed = true,
                    SecurityStamp = "",
                    ConcurrencyStamp = "",
                    PhoneNumber = "081123456789",
                    PhoneNumberConfirmed = "081123456789",
                    TwoFactorEnabled = true,
                    LockOutEnabled = true,
                    LockOutEnd = DateTime.Now,
                    AccessFailedCount = 0,
                    SignInToMobile = true,
                    IsActive = true
                }
            );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole
                {
                    ID = Guid.NewGuid(),
                    UserID = Guid.Parse(userIDAdmin),
                    RoleID = Guid.Parse(roleAdmin),
                });

            modelBuilder.Entity<Bank>().HasData(

                 new Bank
                 {
                     ID = Guid.NewGuid(),
                     Kode = "BCA",
                     BankName = "BCA",
                     IsActive = true,
                     LogoBank = "",
                 },

                  new Bank
                  {
                      ID = Guid.NewGuid(),
                      Kode = "MNDR",
                      BankName = "MANDIRI",
                      IsActive = true,
                      LogoBank = "",
                  },

                  new Bank
                  {
                      ID = Guid.NewGuid(),
                      Kode = "CMB",
                      BankName = "CIMB NIAGA",
                      IsActive = true,
                      LogoBank = "",
                  },

                  new Bank
                  {
                      ID = Guid.NewGuid(),
                      Kode = "BRI",
                      BankName = "BRI",
                      IsActive = true,
                      LogoBank = "",
                  },

                  new Bank
                  {
                      ID = Guid.NewGuid(),
                      Kode = "PNN",
                      BankName = "PANIN",
                      IsActive = true,
                      LogoBank = "",
                  },

                  new Bank
                  {
                      ID = Guid.NewGuid(),
                      Kode = "BNI",
                      BankName = "BNI",
                      IsActive = true,
                      LogoBank = "",
                  },

                  new Bank
                  {
                      ID = Guid.NewGuid(),
                      Kode = "JNS",
                      BankName = "JENIUS",
                      IsActive = true,
                      LogoBank = "",
                  }
            );

            modelBuilder.Entity<BillboardType>().HasData(

                new BillboardType
                {
                    ID = Guid.NewGuid(),
                    Kode = "BBD",
                    Type = "Billboard"
                },
                new BillboardType
                {
                    ID = Guid.NewGuid(),
                    Kode = "MGT",
                    Type = "MEGATRON"
                },
                new BillboardType
                {
                    ID = Guid.NewGuid(),
                    Kode = "NBX",
                    Type = "NEON BOX"
                },
                new BillboardType
                {
                    ID = Guid.NewGuid(),
                    Kode = "NSG",
                    Type = "NEON SIGN, LETTER & KIOS DISPLAY"
                },
                new BillboardType
                {
                    ID = Guid.NewGuid(),
                    Kode = "MCD",
                    Type = "MERCHANDISE"
                },
                new BillboardType
                {
                    ID = Guid.NewGuid(),
                    Kode = "ALM",
                    Type = "ALTERNATIVE MEDIA"
                }

                );

            //Guid idJatim = Guid.NewGuid();
            //modelBuilder.Entity<Province>().HasData(
            //    new Province { ID = idJatim, Provinsi = "JAWA TIMUR", Kode = 1 }
            //);

            //modelBuilder.Entity<City>().HasData(
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "SURABAYA", Kode = 1, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "GRESIK", Kode = 3, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "BANGKALAN", Kode = 4, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "MALANG", Kode = 9, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "BANYUWANGI", Kode = 119, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "BLITAR", Kode = 120, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "BOJONEGORO", Kode = 121, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "BONDOWOSO", Kode = 122, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "JEMBER", Kode = 123, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "JOMBANG", Kode = 124, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "KEDIRI", Kode = 125, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "LAMONGAN", Kode = 126, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "LUMAJANG", Kode = 127, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "MADIUN", Kode = 128, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "MAGETAN", Kode = 129, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "MOJOKERTO", Kode = 130, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "NGANJUK", Kode = 131, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "NGAWI", Kode = 132, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "PACITAN", Kode = 134, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "PASURUAN", Kode = 135, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "PONOROGO", Kode = 136, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "PROBOLINGGO", Kode = 137, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "SAMPANG", Kode = 138, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "SIDOARJO", Kode = 139, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "SITUBONDO", Kode = 140, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "SUMENEP", Kode = 141, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "TRENGGALEK", Kode = 142, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "TUBAN", Kode = 143, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "TULUNGAGUNG", Kode = 144, KodeProvinsi = 1 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJatim, CityName = "BATU", Kode = 145, KodeProvinsi = 1 }
            //);

            //Guid idJakarta = Guid.NewGuid();
            //modelBuilder.Entity<Province>().HasData(
            //    new Province { ID = idJakarta, Provinsi = "DKI JAKARTA", Kode = 2 }
            //);

            //modelBuilder.Entity<City>().HasData(
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJakarta, CityName = "JAKARTA", Kode = 2, KodeProvinsi = 2 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJakarta, CityName = "KEPULAUAN SERIBU", Kode = 46, KodeProvinsi = 2 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJakarta, CityName = "JAKARTA BARAT", Kode = 47, KodeProvinsi = 2 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJakarta, CityName = "JAKARTA SELATAN", Kode = 49, KodeProvinsi = 2 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJakarta, CityName = "JAKARTA TIMUR", Kode = 50, KodeProvinsi = 2 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJakarta, CityName = "JAKARTA UTARA", Kode = 51, KodeProvinsi = 2 }
            //);


            //Guid idJabar = Guid.NewGuid();
            //modelBuilder.Entity<Province>().HasData(
            //    new Province { ID = idJabar, Provinsi = "JAWA BARAT", Kode = 3 }
            //);

            //modelBuilder.Entity<City>().HasData(
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "BANDUNG", Kode = 4, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "BANDUNG BARAT", Kode = 68, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "BEKASI", Kode = 69, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "BOGOR", Kode = 70, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "CIAMIS", Kode = 71, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "CIANJUR", Kode = 72, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "CIREBON", Kode = 73, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "GARUT", Kode = 74, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "INDRAMAYU", Kode = 75, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "KARAWANG", Kode = 76, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "KUNINGAN", Kode = 77, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "MAJALENGKA", Kode = 78, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "PANGANDARAN", Kode = 79, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "PURWAKARTA", Kode = 80, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "SUBANG", Kode = 81, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "SUKABUMI", Kode = 82, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "SUMEDANG", Kode = 83, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "TASIKMALAYA", Kode = 84, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "BANJAR", Kode = 85, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "CIMAHI", Kode = 86, KodeProvinsi = 3 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idJabar, CityName = "DEPOK", Kode = 87, KodeProvinsi = 3 }
            //);

            //Guid idSumut = Guid.NewGuid();
            //modelBuilder.Entity<Province>().HasData(
            //    new Province { ID = idSumut, Provinsi = "SUMATERA UTARA", Kode = 4 }
            //);

            //modelBuilder.Entity<City>().HasData(
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "MEDAN", Kode = 5, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "ASAHAN", Kode = 458, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "BATU BARA", Kode = 459, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "DAIRI", Kode = 460, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "DELI SERDANG", Kode = 461, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "DELI SERDANG", Kode = 462, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "KARO", Kode = 463, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "LABUHANBATU", Kode = 464, KodeProvinsi = 4 },
            //    new City { ID = Guid.NewGuid(), ProvinceID = idSumut, CityName = "LABUHANBATU SELATAN", Kode = 465, KodeProvinsi = 4 }
            //);




           
           

            

            
        }


    }
}
