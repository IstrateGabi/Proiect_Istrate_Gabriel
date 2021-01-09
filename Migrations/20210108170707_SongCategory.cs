using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.Migrations
{
    public partial class SongCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublisherID",
                table: "Song",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SongCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongCategory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SongCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongCategory_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_PublisherID",
                table: "Song",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_SongCategory_CategoryID",
                table: "SongCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_SongCategory_SongID",
                table: "SongCategory",
                column: "SongID");

            migrationBuilder.AddForeignKey(
                name: "FK_Song_Publisher_PublisherID",
                table: "Song",
                column: "PublisherID",
                principalTable: "Publisher",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Song_Publisher_PublisherID",
                table: "Song");

            migrationBuilder.DropTable(
                name: "Publisher");

            migrationBuilder.DropTable(
                name: "SongCategory");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Song_PublisherID",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "PublisherID",
                table: "Song");
        }
    }
}
