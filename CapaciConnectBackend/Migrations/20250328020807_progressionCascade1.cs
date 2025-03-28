using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaciConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class progressionCascade1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_Users_Id_user_id",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_Workshops_Id_workshop_id",
                table: "Progressions");

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_Users_Id_user_id",
                table: "Progressions",
                column: "Id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_Workshops_Id_workshop_id",
                table: "Progressions",
                column: "Id_workshop_id",
                principalTable: "Workshops",
                principalColumn: "Id_workshop",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_Users_Id_user_id",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_Workshops_Id_workshop_id",
                table: "Progressions");

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_Users_Id_user_id",
                table: "Progressions",
                column: "Id_user_id",
                principalTable: "Users",
                principalColumn: "Id_user",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_Workshops_Id_workshop_id",
                table: "Progressions",
                column: "Id_workshop_id",
                principalTable: "Workshops",
                principalColumn: "Id_workshop");
        }
    }
}
