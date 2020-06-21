using Microsoft.EntityFrameworkCore.Migrations;

namespace Billboard360.DataAccess.Migrations
{
    public partial class change_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Traffic",
                table: "TitikLokasi",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<bool>(
                name: "IsKelipatan",
                table: "TitikLokasi",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "TitikLokasi",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Traffic",
                table: "TitikLokasi",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsKelipatan",
                table: "TitikLokasi",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "TitikLokasi",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
