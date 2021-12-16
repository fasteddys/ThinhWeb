using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ADD_TBL_NhomNganh2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlChiTiet",
                table: "Stock_NhomNganhPhu",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlChiTiet",
                table: "Stock_NhomNganh",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlChiTiet",
                table: "Stock_NhomNganhPhu");

            migrationBuilder.DropColumn(
                name: "UrlChiTiet",
                table: "Stock_NhomNganh");
        }
    }
}
