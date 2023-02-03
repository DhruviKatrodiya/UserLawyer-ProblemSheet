using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProjectWebAPI.Migrations
{
    public partial class initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lawyers_LawyerId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "LawyerId",
                table: "Questions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lawyers_LawyerId",
                table: "Questions",
                column: "LawyerId",
                principalTable: "Lawyers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Lawyers_LawyerId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "LawyerId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Lawyers_LawyerId",
                table: "Questions",
                column: "LawyerId",
                principalTable: "Lawyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
