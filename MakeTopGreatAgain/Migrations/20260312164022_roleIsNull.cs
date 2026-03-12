using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MakeTopGreatAgain.Migrations
{
    /// <inheritdoc />
    public partial class roleIsNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_AspNetRoles_RoleId",
                table: "GroupStudents");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "GroupStudents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_AspNetRoles_RoleId",
                table: "GroupStudents",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_AspNetRoles_RoleId",
                table: "GroupStudents");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "GroupStudents",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_AspNetRoles_RoleId",
                table: "GroupStudents",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
