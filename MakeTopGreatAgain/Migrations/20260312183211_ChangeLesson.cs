using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeTopGreatAgain.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLesson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons");

            migrationBuilder.AlterColumn<Guid>(
                name: "HomeworkId",
                table: "Lessons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons");

            migrationBuilder.AlterColumn<Guid>(
                name: "HomeworkId",
                table: "Lessons",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Homeworks_HomeworkId",
                table: "Lessons",
                column: "HomeworkId",
                principalTable: "Homeworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
