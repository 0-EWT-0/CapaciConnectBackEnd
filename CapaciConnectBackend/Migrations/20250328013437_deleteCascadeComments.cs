﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaciConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class deleteCascadeComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Workshops_Id_workshop_id",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Workshops_Id_workshop_id",
                table: "Comments",
                column: "Id_workshop_id",
                principalTable: "Workshops",
                principalColumn: "Id_workshop",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Workshops_Id_workshop_id",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Workshops_Id_workshop_id",
                table: "Comments",
                column: "Id_workshop_id",
                principalTable: "Workshops",
                principalColumn: "Id_workshop");
        }
    }
}
