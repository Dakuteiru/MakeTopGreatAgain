using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeTopGreatAgain.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeacherFile",
                table: "Homeworks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentFile",
                table: "HomeworkCompletions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherFile",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "StudentFile",
                table: "HomeworkCompletions");
        }
    }
}
