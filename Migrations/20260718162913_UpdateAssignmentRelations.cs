using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assignments",
                newName: "CreatedByUserId");

            migrationBuilder.AddColumn<int>(
                name: "AssigneUserId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_AssigneUserId",
                table: "Assignments",
                column: "AssigneUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_CreatedByUserId",
                table: "Assignments",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_AssigneUserId",
                table: "Assignments",
                column: "AssigneUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Users_CreatedByUserId",
                table: "Assignments",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_AssigneUserId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Users_CreatedByUserId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_AssigneUserId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_CreatedByUserId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "AssigneUserId",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserId",
                table: "Assignments",
                newName: "UserId");
        }
    }
}
