using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fituska.DAL.Migrations
{
    public partial class AddedModeratorApprovalForCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ModeratorApproved",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModeratorApproved",
                table: "Courses");
        }
    }
}
