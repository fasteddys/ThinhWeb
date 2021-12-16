using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ADD_TBL_Stock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaCP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlChiTiet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SanNiemYet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuongLuuHanh = table.Column<long>(type: "bigint", nullable: false),
                    NhomNganhPhuId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NhomNganhId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Stock_NhomNganh_NhomNganhId",
                        column: x => x.NhomNganhId,
                        principalTable: "Stock_NhomNganh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Stock_Stock_NhomNganhPhu_NhomNganhPhuId",
                        column: x => x.NhomNganhPhuId,
                        principalTable: "Stock_NhomNganhPhu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_NhomNganhId",
                table: "Stock",
                column: "NhomNganhId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_NhomNganhPhuId",
                table: "Stock",
                column: "NhomNganhPhuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
