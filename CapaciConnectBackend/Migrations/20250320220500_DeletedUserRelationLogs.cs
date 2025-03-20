using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaciConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class DeletedUserRelationLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_Id_user_id",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_Id_user_id",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Id_user_id",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "UsersId_user",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_UsersId_user",
                table: "Logs",
                column: "UsersId_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_UsersId_user",
                table: "Logs",
                column: "UsersId_user",
                principalTable: "Users",
                principalColumn: "Id_user");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_UsersId_user",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_UsersId_user",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UsersId_user",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "Id_user_id",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Id_user_id",
                table: "Logs",
                column: "Id_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_Id_user_id",
                table: "Logs",
                column: "Id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
