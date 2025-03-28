using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaciConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class progressionCascade2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Workshops_Id_workshop_id",
                table: "Subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Workshops_Id_workshop_id",
                table: "Subscriptions",
                column: "Id_workshop_id",
                principalTable: "Workshops",
                principalColumn: "Id_workshop",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Workshops_Id_workshop_id",
                table: "Subscriptions");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Workshops_Id_workshop_id",
                table: "Subscriptions",
                column: "Id_workshop_id",
                principalTable: "Workshops",
                principalColumn: "Id_workshop");
        }
    }
}
