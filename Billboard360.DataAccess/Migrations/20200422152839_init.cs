using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billboard360.DataAccess.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    Kode = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true),
                    LogoBank = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BillboardType",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    Kode = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillboardType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LogActivity",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    KodeLogger = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogActivity", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LogForgotPassword",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    DateRequest = table.Column<DateTime>(nullable: false),
                    DateExpired = table.Column<DateTime>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogForgotPassword", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LogRateSite",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    SiteID = table.Column<Guid>(nullable: false),
                    SiteItemID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    RateScore = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogRateSite", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MidTransLog",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    PaymentID = table.Column<Guid>(nullable: false),
                    MidTransStatus = table.Column<int>(nullable: false),
                    ModeTransaction = table.Column<int>(nullable: false),
                    Transaction_Time = table.Column<DateTime>(nullable: false),
                    Transaction_Status = table.Column<string>(nullable: true),
                    Status_Message = table.Column<string>(nullable: true),
                    Status_Code = table.Column<string>(nullable: true),
                    Signature_Key = table.Column<string>(nullable: true),
                    Payment_Type = table.Column<string>(nullable: true),
                    Order_ID = table.Column<string>(nullable: true),
                    Merchant_ID = table.Column<string>(nullable: true),
                    Gross_Amount = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Approval_Code = table.Column<string>(nullable: true),
                    Transaction_ID = table.Column<string>(nullable: true),
                    VANumber = table.Column<string>(nullable: true),
                    MidTransTransactionType = table.Column<string>(nullable: true),
                    BankName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidTransLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    Kode = table.Column<int>(nullable: false),
                    Provinsi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    SiteID = table.Column<Guid>(nullable: false),
                    SiteItemID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    ReportNo = table.Column<string>(nullable: true),
                    isToSPV = table.Column<bool>(nullable: false),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReportImage",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    ReportID = table.Column<Guid>(nullable: false),
                    PathFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImage", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SP_GetTitikLokasiWithDistance",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    NoBillboard = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AddressReal = table.Column<string>(nullable: true),
                    KelasJalan = table.Column<string>(nullable: true),
                    KodeFile = table.Column<string>(nullable: true),
                    PIC = table.Column<string>(nullable: true),
                    Kota = table.Column<string>(nullable: true),
                    Provinsi = table.Column<string>(nullable: true),
                    Pulau = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Cabang = table.Column<string>(nullable: true),
                    HorV = table.Column<string>(nullable: true),
                    Lampu = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AdvTypeID = table.Column<Guid>(nullable: false),
                    AdaKontruksi = table.Column<bool>(nullable: false),
                    Tinggi = table.Column<double>(nullable: false),
                    TotalView = table.Column<int>(nullable: false),
                    RateScoreTotal = table.Column<double>(nullable: false),
                    TransaksiCount = table.Column<int>(nullable: false),
                    RateScoreAverage = table.Column<double>(nullable: false),
                    OwnerByUserID = table.Column<Guid>(nullable: false),
                    Distance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SP_GetTitikLokasiWithDistance", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TitikLokasi",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    NoBillboard = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    AddressReal = table.Column<string>(nullable: true),
                    KelasJalan = table.Column<string>(nullable: true),
                    KodeFile = table.Column<string>(nullable: true),
                    PIC = table.Column<string>(nullable: true),
                    Kota = table.Column<string>(nullable: true),
                    Provinsi = table.Column<string>(nullable: true),
                    Pulau = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Cabang = table.Column<string>(nullable: true),
                    HorV = table.Column<string>(nullable: true),
                    Lampu = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AdvTypeID = table.Column<Guid>(nullable: false),
                    AdaKontruksi = table.Column<bool>(nullable: false),
                    Tinggi = table.Column<double>(nullable: false),
                    TotalView = table.Column<int>(nullable: false),
                    RateScoreTotal = table.Column<double>(nullable: false),
                    TransaksiCount = table.Column<int>(nullable: false),
                    RateScoreAverage = table.Column<double>(nullable: false),
                    OwnerByUserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitikLokasi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockOutEnd = table.Column<DateTime>(nullable: false),
                    LockOutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    SignInToMobile = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    Kode = table.Column<int>(nullable: false),
                    ProvinceID = table.Column<Guid>(nullable: false),
                    KodeProvinsi = table.Column<int>(nullable: false),
                    CityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compare",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    SiteID = table.Column<Guid>(nullable: false),
                    SiteItemID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Compare_TitikLokasi_SiteID",
                        column: x => x.SiteID,
                        principalTable: "TitikLokasi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitikLokasiDetail",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    TitikLokasiID = table.Column<Guid>(nullable: false),
                    KodeArahLokasi = table.Column<string>(nullable: true),
                    ArahLokasi = table.Column<string>(nullable: true),
                    Panjang = table.Column<double>(nullable: false),
                    Lebar = table.Column<double>(nullable: false),
                    Keterangan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitikLokasiDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitikLokasiDetail_TitikLokasi_TitikLokasiID",
                        column: x => x.TitikLokasiID,
                        principalTable: "TitikLokasi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false),
                    Alamat = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Kategory = table.Column<string>(nullable: true),
                    isMain = table.Column<bool>(nullable: false),
                    NPWP = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Company_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    SubTotalPrice = table.Column<double>(nullable: false),
                    Diskon = table.Column<double>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    TotalPaid = table.Column<double>(nullable: false),
                    isLunas = table.Column<bool>(nullable: false),
                    BookID = table.Column<Guid>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    PPNProsen = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payment_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBank",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false),
                    BankID = table.Column<Guid>(nullable: false),
                    isSelected = table.Column<bool>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBank", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBank_Bank_BankID",
                        column: x => x.BankID,
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBank_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    PaymentID = table.Column<Guid>(nullable: true),
                    CompanyID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    isNotified = table.Column<bool>(nullable: false),
                    StatusApproval = table.Column<int>(nullable: false),
                    BookNo = table.Column<string>(nullable: true),
                    SiteItemID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Book_TitikLokasiDetail_SiteItemID",
                        column: x => x.SiteItemID,
                        principalTable: "TitikLokasiDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    PriceID = table.Column<Guid>(nullable: false),
                    SiteID = table.Column<Guid>(nullable: false),
                    SiteItemID = table.Column<Guid>(nullable: false),
                    CompanyID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    BookID = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    TotalPerItem = table.Column<double>(nullable: false),
                    isNotified = table.Column<bool>(nullable: false),
                    StatusApproval = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cart_TitikLokasiDetail_SiteItemID",
                        column: x => x.SiteItemID,
                        principalTable: "TitikLokasiDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitikLokasiImage",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    TitikLokasiID = table.Column<Guid>(nullable: false),
                    TitikLokasiDetailID = table.Column<Guid>(nullable: false),
                    PathImage = table.Column<string>(nullable: true),
                    IsThumbnail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitikLokasiImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitikLokasiImage_TitikLokasiDetail_TitikLokasiDetailID",
                        column: x => x.TitikLokasiDetailID,
                        principalTable: "TitikLokasiDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitikLokasiPrice",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    TitikLokasiID = table.Column<Guid>(nullable: false),
                    TitikLokasiDetailID = table.Column<Guid>(nullable: false),
                    Dasar = table.Column<string>(nullable: true),
                    Kelipatan = table.Column<double>(nullable: false),
                    HargaAwal = table.Column<double>(nullable: false),
                    HargaAkhir = table.Column<double>(nullable: false),
                    MinimDasar = table.Column<string>(nullable: true),
                    MinimOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitikLokasiPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitikLokasiPrice_TitikLokasiDetail_TitikLokasiDetailID",
                        column: x => x.TitikLokasiDetailID,
                        principalTable: "TitikLokasiDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    SiteID = table.Column<Guid>(nullable: false),
                    SiteItemID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WishList_TitikLokasiDetail_SiteItemID",
                        column: x => x.SiteItemID,
                        principalTable: "TitikLokasiDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishList_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPerson",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    CompanyID = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Jabatan = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPerson", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactPerson_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookDetail",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    BookID = table.Column<Guid>(nullable: false),
                    PriceID = table.Column<Guid>(nullable: false),
                    SiteID = table.Column<Guid>(nullable: false),
                    SiteItemID = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    FinalPrice = table.Column<double>(nullable: false),
                    TotalPerItem = table.Column<double>(nullable: false),
                    StatusApprovalPerBillboard = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookDetail_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bank",
                columns: new[] { "ID", "BankName", "CreateByUserID", "CreateDate", "DeletedByUserID", "DeletedDate", "IsActive", "Kode", "LastUpdateByUserID", "LastUpdateDate", "LogoBank" },
                values: new object[,]
                {
                    { new Guid("8a831fae-00e9-46aa-8bc5-b749b49ee60d"), "BCA", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "BCA", null, null, "" },
                    { new Guid("0a30f0ef-27aa-476a-8207-b9fe4cb5e8f0"), "MANDIRI", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "MNDR", null, null, "" },
                    { new Guid("f8678e27-ea39-4fe4-bd64-a01f1adcdd1c"), "CIMB NIAGA", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "CMB", null, null, "" },
                    { new Guid("d79111e2-edf9-4606-90b0-6e44ca690472"), "BRI", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "BRI", null, null, "" },
                    { new Guid("eb0ac480-e1ba-4468-8ba1-c7e9f59083ee"), "PANIN", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "PNN", null, null, "" },
                    { new Guid("986ab193-fa3d-407f-9ec1-e12b3c185fcd"), "BNI", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "BNI", null, null, "" },
                    { new Guid("94f483d4-8836-48ef-b24e-29e96aa9c3ae"), "JENIUS", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "JNS", null, null, "" }
                });

            migrationBuilder.InsertData(
                table: "BillboardType",
                columns: new[] { "ID", "CreateByUserID", "CreateDate", "DeletedByUserID", "DeletedDate", "Kode", "LastUpdateByUserID", "LastUpdateDate", "Type" },
                values: new object[,]
                {
                    { new Guid("4e9e8b5d-f24d-43f1-a65f-fa309bdc57a8"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "ALM", null, null, "ALTERNATIVE MEDIA" },
                    { new Guid("9ddc454e-8191-4c7a-ad2e-8ce0a48a5c20"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "MCD", null, null, "MERCHANDISE" },
                    { new Guid("32c09574-1216-44d1-9ed0-4ca651183b7d"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "NSG", null, null, "NEON SIGN, LETTER & KIOS DISPLAY" },
                    { new Guid("70c8454a-fcce-45a7-a93a-381a2af6b06b"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "MGT", null, null, "MEGATRON" },
                    { new Guid("cc0277f1-f543-4c1b-a0a2-766a03916faa"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "BBD", null, null, "Billboard" },
                    { new Guid("31f5da4e-c744-4fb9-9430-05add9aadbeb"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "NBX", null, null, "NEON BOX" }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "ConcurrencyStamp", "CreateByUserID", "CreateDate", "DeletedByUserID", "DeletedDate", "LastUpdateByUserID", "LastUpdateDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("86d43cc3-f6e0-4312-86c6-b6879b9eae87"), "", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "MDO", "Media Owner" },
                    { new Guid("b9c78ba1-f5e7-48cf-ae17-d0c859365905"), "", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "ADM", "Admin" },
                    { new Guid("a7e4d5e0-e709-4612-89df-88fd2146cd75"), "", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "SPV", "Supervisor" },
                    { new Guid("3d526bf5-93bb-4c16-b2b9-39e1be9e5207"), "", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "MDB", "Media Buyer" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "AccessFailedCount", "ConcurrencyStamp", "CreateByUserID", "CreateDate", "DeletedByUserID", "DeletedDate", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LastUpdateByUserID", "LastUpdateDate", "LockOutEnabled", "LockOutEnd", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PhotoUrl", "SecurityStamp", "SignInToMobile", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("3e6b87ef-6bc6-4a96-a105-7a2fec6ec3a1"), 0, "", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, true, "Admin", true, "Admin", null, null, true, new DateTime(2020, 4, 22, 22, 28, 38, 330, DateTimeKind.Local).AddTicks(7365), null, "81DC9BDB52D04DC20036DBD8313ED055", "081123456789", "081123456789", "", "", true, true, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "ID", "CreateByUserID", "CreateDate", "DeletedByUserID", "DeletedDate", "LastUpdateByUserID", "LastUpdateDate", "RoleID", "UserID" },
                values: new object[] { new Guid("48e897ea-c58e-42d7-9af6-ad699988a65f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, new Guid("b9c78ba1-f5e7-48cf-ae17-d0c859365905"), new Guid("3e6b87ef-6bc6-4a96-a105-7a2fec6ec3a1") });

            migrationBuilder.CreateIndex(
                name: "IX_Book_SiteItemID",
                table: "Book",
                column: "SiteItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_UserID",
                table: "Book",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookDetail_BookID",
                table: "BookDetail",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_SiteItemID",
                table: "Cart",
                column: "SiteItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceID",
                table: "City",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserID",
                table: "Company",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Compare_SiteID",
                table: "Compare",
                column: "SiteID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPerson_CompanyID",
                table: "ContactPerson",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserID",
                table: "Payment",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TitikLokasiDetail_TitikLokasiID",
                table: "TitikLokasiDetail",
                column: "TitikLokasiID");

            migrationBuilder.CreateIndex(
                name: "IX_TitikLokasiImage_TitikLokasiDetailID",
                table: "TitikLokasiImage",
                column: "TitikLokasiDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TitikLokasiPrice_TitikLokasiDetailID",
                table: "TitikLokasiPrice",
                column: "TitikLokasiDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBank_BankID",
                table: "UserBank",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_UserBank_UserID",
                table: "UserBank",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_SiteItemID",
                table: "WishList",
                column: "SiteItemID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_UserID",
                table: "WishList",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillboardType");

            migrationBuilder.DropTable(
                name: "BookDetail");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Compare");

            migrationBuilder.DropTable(
                name: "ContactPerson");

            migrationBuilder.DropTable(
                name: "LogActivity");

            migrationBuilder.DropTable(
                name: "LogForgotPassword");

            migrationBuilder.DropTable(
                name: "LogRateSite");

            migrationBuilder.DropTable(
                name: "MidTransLog");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "ReportImage");

            migrationBuilder.DropTable(
                name: "SP_GetTitikLokasiWithDistance");

            migrationBuilder.DropTable(
                name: "TitikLokasiImage");

            migrationBuilder.DropTable(
                name: "TitikLokasiPrice");

            migrationBuilder.DropTable(
                name: "UserBank");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "TitikLokasiDetail");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "TitikLokasi");
        }
    }
}
