using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fituska.DAL.Migrations
{
    public partial class DbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSawAnswer_Answers_AnswerId",
                table: "UserSawAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSawAnswer_AspNetUsers_UserId",
                table: "UserSawAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSawQuestion_AspNetUsers_UserId",
                table: "UserSawQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSawQuestion_Question_QuestionId",
                table: "UserSawQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSawQuestion",
                table: "UserSawQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSawAnswer",
                table: "UserSawAnswer");

            migrationBuilder.RenameTable(
                name: "UserSawQuestion",
                newName: "UsersSawQuestions");

            migrationBuilder.RenameTable(
                name: "UserSawAnswer",
                newName: "UsersSawAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_UserSawQuestion_UserId",
                table: "UsersSawQuestions",
                newName: "IX_UsersSawQuestions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSawQuestion_QuestionId",
                table: "UsersSawQuestions",
                newName: "IX_UsersSawQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSawAnswer_UserId",
                table: "UsersSawAnswers",
                newName: "IX_UsersSawAnswers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSawAnswer_AnswerId",
                table: "UsersSawAnswers",
                newName: "IX_UsersSawAnswers_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersSawQuestions",
                table: "UsersSawQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersSawAnswers",
                table: "UsersSawAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSawAnswers_Answers_AnswerId",
                table: "UsersSawAnswers",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSawAnswers_AspNetUsers_UserId",
                table: "UsersSawAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSawQuestions_AspNetUsers_UserId",
                table: "UsersSawQuestions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersSawQuestions_Question_QuestionId",
                table: "UsersSawQuestions",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersSawAnswers_Answers_AnswerId",
                table: "UsersSawAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSawAnswers_AspNetUsers_UserId",
                table: "UsersSawAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSawQuestions_AspNetUsers_UserId",
                table: "UsersSawQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersSawQuestions_Question_QuestionId",
                table: "UsersSawQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersSawQuestions",
                table: "UsersSawQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersSawAnswers",
                table: "UsersSawAnswers");

            migrationBuilder.RenameTable(
                name: "UsersSawQuestions",
                newName: "UserSawQuestion");

            migrationBuilder.RenameTable(
                name: "UsersSawAnswers",
                newName: "UserSawAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_UsersSawQuestions_UserId",
                table: "UserSawQuestion",
                newName: "IX_UserSawQuestion_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersSawQuestions_QuestionId",
                table: "UserSawQuestion",
                newName: "IX_UserSawQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersSawAnswers_UserId",
                table: "UserSawAnswer",
                newName: "IX_UserSawAnswer_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UsersSawAnswers_AnswerId",
                table: "UserSawAnswer",
                newName: "IX_UserSawAnswer_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSawQuestion",
                table: "UserSawQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSawAnswer",
                table: "UserSawAnswer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSawAnswer_Answers_AnswerId",
                table: "UserSawAnswer",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSawAnswer_AspNetUsers_UserId",
                table: "UserSawAnswer",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSawQuestion_AspNetUsers_UserId",
                table: "UserSawQuestion",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSawQuestion_Question_QuestionId",
                table: "UserSawQuestion",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
