using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fituska.DAL.Migrations
{
    public partial class PhotoBytesToUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Questions_QuestionId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Votes",
                newName: "AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_QuestionId",
                table: "Votes",
                newName: "IX_Votes_AnswerId");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Answers_AnswerId",
                table: "Votes",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Answers_AnswerId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AnswerId",
                table: "Votes",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_AnswerId",
                table: "Votes",
                newName: "IX_Votes_QuestionId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "AspNetUsers",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Questions_QuestionId",
                table: "Votes",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
