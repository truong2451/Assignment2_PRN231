using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrinhHuuTruong.eBookStore.Repositories.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorOther",
                table: "BookAuthors",
                newName: "AuthorOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorOrder",
                table: "BookAuthors",
                newName: "AuthorOther");
        }
    }
}
