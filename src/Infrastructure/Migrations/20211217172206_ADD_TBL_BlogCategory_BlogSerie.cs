using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ADD_TBL_BlogCategory_BlogSerie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SerieId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogSerie",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSerie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blog_BlogCategory",
                columns: table => new
                {
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog_BlogCategory", x => new { x.PostId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_Blog_BlogCategory_BlogCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Blog_BlogCategory_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SerieId",
                table: "Posts",
                column: "SerieId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategory_CategoryId",
                table: "Blog_BlogCategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_BlogSerie_SerieId",
                table: "Posts",
                column: "SerieId",
                principalTable: "BlogSerie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_BlogSerie_SerieId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Blog_BlogCategory");

            migrationBuilder.DropTable(
                name: "BlogSerie");

            migrationBuilder.DropTable(
                name: "BlogCategory");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SerieId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SerieId",
                table: "Posts");
        }
    }
}
