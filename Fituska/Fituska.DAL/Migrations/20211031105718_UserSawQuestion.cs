using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fituska.DAL.Migrations
{
    public partial class UserSawQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Answers_AnswerEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoID",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AnswerEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AnswerEntityId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "AspNetUsers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserSawAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnswerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSawAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSawAnswer_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSawAnswer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSawQuestion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    QuestionId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSawQuestion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSawQuestion_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSawQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSawAnswer_AnswerId",
                table: "UserSawAnswer",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSawAnswer_UserId",
                table: "UserSawAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSawQuestion_QuestionId",
                table: "UserSawQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSawQuestion_UserId",
                table: "UserSawQuestion",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSawAnswer");

            migrationBuilder.DropTable(
                name: "UserSawQuestion");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "AnswerEntityId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoID",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AnswerEntityId",
                table: "AspNetUsers",
                column: "AnswerEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoID",
                table: "AspNetUsers",
                column: "PhotoID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Answers_AnswerEntityId",
                table: "AspNetUsers",
                column: "AnswerEntityId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoID",
                table: "AspNetUsers",
                column: "PhotoID",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
