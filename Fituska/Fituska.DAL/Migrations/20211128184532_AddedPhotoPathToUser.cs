using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fituska.DAL.Migrations
{
    public partial class AddedPhotoPathToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "AspNetUsers");
        }
    }
}
