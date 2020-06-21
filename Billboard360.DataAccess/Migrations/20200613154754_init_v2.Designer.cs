﻿// <auto-generated />
using System;
using Billboard360.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Billboard360.DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200613154754_init_v2")]
    partial class init_v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Bank", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankName");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Kode");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("LogoBank");

                    b.HasKey("ID");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.BillboardType", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Kode");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.ToTable("BillboardType");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Book", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BookNo");

                    b.Property<Guid>("CompanyID");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<Guid?>("PaymentID");

                    b.Property<Guid?>("SiteItemID");

                    b.Property<int>("StatusApproval");

                    b.Property<Guid>("UserID");

                    b.Property<bool>("isNotified");

                    b.HasKey("ID");

                    b.HasIndex("SiteItemID");

                    b.HasIndex("UserID");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.BookDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookID");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<double>("FinalPrice");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Note");

                    b.Property<double>("Price");

                    b.Property<Guid>("PriceID");

                    b.Property<int>("Qty");

                    b.Property<Guid>("SiteID");

                    b.Property<Guid>("SiteItemID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("StatusApprovalPerBillboard");

                    b.Property<double>("TotalPerItem");

                    b.HasKey("ID");

                    b.HasIndex("BookID");

                    b.ToTable("BookDetail");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Cart", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BookID");

                    b.Property<Guid>("CompanyID");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<double>("Price");

                    b.Property<Guid>("PriceID");

                    b.Property<int>("Qty");

                    b.Property<Guid>("SiteID");

                    b.Property<Guid>("SiteItemID");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("StatusApproval");

                    b.Property<double>("TotalPerItem");

                    b.Property<Guid>("UserID");

                    b.Property<bool>("isNotified");

                    b.HasKey("ID");

                    b.HasIndex("SiteItemID");

                    b.HasIndex("UserID");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.City", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int>("Kode");

                    b.Property<int>("KodeProvinsi");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<Guid>("ProvinceID");

                    b.HasKey("ID");

                    b.HasIndex("ProvinceID");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Company", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alamat");

                    b.Property<string>("Alias");

                    b.Property<string>("CompanyName");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Email");

                    b.Property<string>("Kategory");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("NPWP");

                    b.Property<string>("Note");

                    b.Property<Guid>("UserID");

                    b.Property<string>("Website");

                    b.Property<bool>("isMain");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Compare", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<Guid>("SiteID");

                    b.Property<Guid>("SiteItemID");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("SiteID");

                    b.ToTable("Compare");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.ContactPerson", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CompanyID");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Email");

                    b.Property<string>("Jabatan");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Name");

                    b.Property<string>("Note");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("CompanyID");

                    b.ToTable("ContactPerson");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.LogActivity", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("KodeLogger");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Message");

                    b.HasKey("ID");

                    b.ToTable("LogActivity");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.LogForgotPassword", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<DateTime>("DateExpired");

                    b.Property<DateTime>("DateRequest");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<bool>("IsUsed");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("LogForgotPassword");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.LogRateSite", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<double>("RateScore");

                    b.Property<Guid>("SiteID");

                    b.Property<Guid>("SiteItemID");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.ToTable("LogRateSite");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.MidTransLog", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Approval_Code");

                    b.Property<string>("BankName");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Currency");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Gross_Amount");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Merchant_ID");

                    b.Property<int>("MidTransStatus");

                    b.Property<string>("MidTransTransactionType");

                    b.Property<int>("ModeTransaction");

                    b.Property<string>("Order_ID");

                    b.Property<Guid>("PaymentID");

                    b.Property<string>("Payment_Type");

                    b.Property<string>("Signature_Key");

                    b.Property<string>("Status_Code");

                    b.Property<string>("Status_Message");

                    b.Property<string>("Transaction_ID");

                    b.Property<string>("Transaction_Status");

                    b.Property<DateTime>("Transaction_Time");

                    b.Property<string>("VANumber");

                    b.HasKey("ID");

                    b.ToTable("MidTransLog");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Payment", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookID");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<double>("Diskon");

                    b.Property<string>("InvoiceNo");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<int>("PPNProsen");

                    b.Property<int>("PaymentType");

                    b.Property<double>("SubTotalPrice");

                    b.Property<double>("TotalPaid");

                    b.Property<double>("TotalPrice");

                    b.Property<Guid>("UserID");

                    b.Property<bool>("isLunas");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Province", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<int>("Kode");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Provinsi");

                    b.HasKey("ID");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Report", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Desc");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("ReportNo");

                    b.Property<Guid>("SiteID");

                    b.Property<Guid>("SiteItemID");

                    b.Property<Guid>("UserID");

                    b.Property<bool>("isToSPV");

                    b.HasKey("ID");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.ReportDocument", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("PathFile");

                    b.Property<Guid>("ReportID");

                    b.HasKey("ID");

                    b.ToTable("ReportImage");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Role", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.SP_GetTitikLokasiWithDistance", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdaKontruksi");

                    b.Property<string>("Address");

                    b.Property<string>("AddressReal");

                    b.Property<Guid>("AdvTypeID");

                    b.Property<string>("Cabang");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<double>("Distance");

                    b.Property<string>("HorV");

                    b.Property<string>("KelasJalan");

                    b.Property<string>("KodeFile");

                    b.Property<string>("Kota");

                    b.Property<string>("Lampu");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("NoBillboard");

                    b.Property<Guid>("OwnerByUserID");

                    b.Property<string>("PIC");

                    b.Property<string>("Provinsi");

                    b.Property<string>("Pulau");

                    b.Property<double>("RateScoreAverage");

                    b.Property<double>("RateScoreTotal");

                    b.Property<int>("Status");

                    b.Property<double>("Tinggi");

                    b.Property<int>("TotalView");

                    b.Property<int>("TransaksiCount");

                    b.Property<string>("Type");

                    b.HasKey("ID");

                    b.ToTable("SP_GetTitikLokasiWithDistance");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasi", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdaKontruksi");

                    b.Property<string>("Address");

                    b.Property<string>("AddressReal");

                    b.Property<Guid>("AdvTypeID");

                    b.Property<string>("Cabang");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<double>("Grade");

                    b.Property<string>("HorV");

                    b.Property<bool>("IsKelipatan");

                    b.Property<string>("KelasJalan");

                    b.Property<string>("KodeFile");

                    b.Property<string>("Kota");

                    b.Property<string>("Lampu");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("NoBillboard");

                    b.Property<Guid>("OwnerByUserID");

                    b.Property<string>("PIC");

                    b.Property<string>("Provinsi");

                    b.Property<string>("Pulau");

                    b.Property<double>("RateScoreAverage");

                    b.Property<double>("RateScoreTotal");

                    b.Property<int>("Status");

                    b.Property<double>("Tinggi");

                    b.Property<int>("TotalView");

                    b.Property<double>("Traffic");

                    b.Property<int>("TransaksiCount");

                    b.Property<string>("Type");

                    b.Property<string>("URLStreetView");

                    b.HasKey("ID");

                    b.ToTable("TitikLokasi");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasiDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArahLokasi");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("Keterangan");

                    b.Property<string>("KodeArahLokasi");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<double>("Lebar");

                    b.Property<double>("Panjang");

                    b.Property<Guid>("TitikLokasiID");

                    b.HasKey("ID");

                    b.HasIndex("TitikLokasiID");

                    b.ToTable("TitikLokasiDetail");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasiImage", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<string>("IsThumbnail");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("PathImage");

                    b.Property<Guid>("TitikLokasiDetailID");

                    b.Property<Guid>("TitikLokasiID");

                    b.HasKey("ID");

                    b.HasIndex("TitikLokasiDetailID");

                    b.ToTable("TitikLokasiImage");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasiPrice", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Dasar");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<double>("HargaAkhir");

                    b.Property<double>("HargaAwal");

                    b.Property<double>("Kelipatan");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("MinimDasar");

                    b.Property<int>("MinimOrder");

                    b.Property<Guid>("TitikLokasiDetailID");

                    b.Property<Guid>("TitikLokasiID");

                    b.HasKey("ID");

                    b.HasIndex("TitikLokasiDetailID");

                    b.ToTable("TitikLokasiPrice");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<bool>("LockOutEnabled");

                    b.Property<DateTime>("LockOutEnd");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PhoneNumberConfirmed");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("SignInToMobile");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.UserBank", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<string>("AccountNumber");

                    b.Property<Guid>("BankID");

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<Guid>("UserID");

                    b.Property<bool>("isSelected");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.HasIndex("UserID");

                    b.ToTable("UserBank");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.UserRole", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<Guid>("RoleID");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("RoleID");

                    b.HasIndex("UserID");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.WishList", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CreateByUserID");

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("DeletedByUserID");

                    b.Property<DateTime?>("DeletedDate");

                    b.Property<Guid?>("LastUpdateByUserID");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<Guid>("SiteID");

                    b.Property<Guid>("SiteItemID");

                    b.Property<Guid>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("SiteItemID");

                    b.HasIndex("UserID");

                    b.ToTable("WishList");
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Book", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasiDetail", "SiteItem")
                        .WithMany()
                        .HasForeignKey("SiteItemID");

                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.BookDetail", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Cart", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasiDetail", "SiteItem")
                        .WithMany()
                        .HasForeignKey("SiteItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.City", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Company", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Compare", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasi", "Site")
                        .WithMany()
                        .HasForeignKey("SiteID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.ContactPerson", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.Payment", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasiDetail", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasi", "TitikLokasi")
                        .WithMany()
                        .HasForeignKey("TitikLokasiID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasiImage", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasiDetail", "TitikLokasiDetail")
                        .WithMany()
                        .HasForeignKey("TitikLokasiDetailID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.TitikLokasiPrice", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasiDetail", "TitikLokasiDetail")
                        .WithMany()
                        .HasForeignKey("TitikLokasiDetailID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.UserBank", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.Bank", "Bank")
                        .WithMany()
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.UserRole", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Billboard360.DataAccess.Entities.WishList", b =>
                {
                    b.HasOne("Billboard360.DataAccess.Entities.TitikLokasiDetail", "SiteItem")
                        .WithMany()
                        .HasForeignKey("SiteItemID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Billboard360.DataAccess.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
