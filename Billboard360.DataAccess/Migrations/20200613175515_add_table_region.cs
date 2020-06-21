using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Billboard360.DataAccess.Migrations
{
    public partial class add_table_region : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreateByUserID = table.Column<Guid>(nullable: false),
                    LastUpdateDate = table.Column<DateTime>(nullable: true),
                    LastUpdateByUserID = table.Column<Guid>(nullable: true),
                    DeletedDate = table.Column<DateTime>(nullable: true),
                    DeletedByUserID = table.Column<Guid>(nullable: true),
                    KodeKota = table.Column<string>(nullable: true),
                    KotaOrKab = table.Column<string>(nullable: true),
                    KodeProvinsi = table.Column<string>(nullable: true),
                    Provinsi = table.Column<string>(nullable: true),
                    KodePulau = table.Column<string>(nullable: true),
                    Pulau = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
