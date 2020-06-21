using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billboard360.DataAccess.Migrations
{
    public partial class add_companyCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyCategory",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    KodeCategori = table.Column<int>(nullable: false),
                    Nama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCategory", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyCategory");
        }
    }
}
