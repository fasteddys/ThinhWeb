using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class DEL_TBL_NhomNganhPhu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Stock_NhomNganhPhu_NhomNganhPhuId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "Stock_NhomNganhPhu");

            migrationBuilder.DropIndex(
                name: "IX_Stock_NhomNganhPhuId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "NhomNganhPhuId",
                table: "Stock");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Stock_NhomNganh",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NhomNganhChaId",
                table: "Stock_NhomNganh",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Stock_NhomNganhId",
                table: "Stock_NhomNganh",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenCongTy",
                table: "Stock",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_NhomNganh_Stock_NhomNganhId",
                table: "Stock_NhomNganh",
                column: "Stock_NhomNganhId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_NhomNganh_Stock_NhomNganh_Stock_NhomNganhId",
                table: "Stock_NhomNganh",
                column: "Stock_NhomNganhId",
                principalTable: "Stock_NhomNganh",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_NhomNganh_Stock_NhomNganh_Stock_NhomNganhId",
                table: "Stock_NhomNganh");

            migrationBuilder.DropIndex(
                name: "IX_Stock_NhomNganh_Stock_NhomNganhId",
                table: "Stock_NhomNganh");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Stock_NhomNganh");

            migrationBuilder.DropColumn(
                name: "NhomNganhChaId",
                table: "Stock_NhomNganh");

            migrationBuilder.DropColumn(
                name: "Stock_NhomNganhId",
                table: "Stock_NhomNganh");

            migrationBuilder.DropColumn(
                name: "TenCongTy",
                table: "Stock");

            migrationBuilder.AddColumn<string>(
                name: "NhomNganhPhuId",
                table: "Stock",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stock_NhomNganhPhu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhomNganhId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_NhomNganhPhu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_NhomNganhPhu_Stock_NhomNganh_NhomNganhId",
                        column: x => x.NhomNganhId,
                        principalTable: "Stock_NhomNganh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_NhomNganhPhuId",
                table: "Stock",
                column: "NhomNganhPhuId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_NhomNganhPhu_NhomNganhId",
                table: "Stock_NhomNganhPhu",
                column: "NhomNganhId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Stock_NhomNganhPhu_NhomNganhPhuId",
                table: "Stock",
                column: "NhomNganhPhuId",
                principalTable: "Stock_NhomNganhPhu",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
