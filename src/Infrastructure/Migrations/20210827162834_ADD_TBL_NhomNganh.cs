using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ADD_TBL_NhomNganh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileStorages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OriginFullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginFileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageFullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StorageFileExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDisable = table.Column<bool>(type: "bit", nullable: false),
                    UploadAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStorages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock_NhomNganh",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock_NhomNganh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock_NhomNganhPhu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhomNganhId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_Stock_NhomNganhPhu_NhomNganhId",
                table: "Stock_NhomNganhPhu",
                column: "NhomNganhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileStorages");

            migrationBuilder.DropTable(
                name: "Stock_NhomNganhPhu");

            migrationBuilder.DropTable(
                name: "Stock_NhomNganh");
        }
    }
}
