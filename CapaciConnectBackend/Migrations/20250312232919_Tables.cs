using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapaciConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Multimedia",
                columns: table => new
                {
                    Id_multimedia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Media_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Media_type = table.Column<int>(type: "int", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multimedia", x => x.Id_multimedia);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id_rol);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopTypes",
                columns: table => new
                {
                    Id_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopTypes", x => x.Id_type);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Last_names = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Profile_img = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_rol_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id_user);
                    table.ForeignKey(
                        name: "FK_Users_Rols_Id_rol_id",
                        column: x => x.Id_rol_id,
                        principalTable: "Rols",
                        principalColumn: "Id_rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id_log = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id_log);
                    table.ForeignKey(
                        name: "FK_Logs_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id_session = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id_session);
                    table.ForeignKey(
                        name: "FK_Sessions_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    Id_workshop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_user_id = table.Column<int>(type: "int", nullable: false),
                    Id_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id_workshop);
                    table.ForeignKey(
                        name: "FK_Workshops_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workshops_WorkshopTypes_Id_type_id",
                        column: x => x.Id_type_id,
                        principalTable: "WorkshopTypes",
                        principalColumn: "Id_type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id_calendar = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_end = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_workshop_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id_calendar);
                    table.ForeignKey(
                        name: "FK_Calendars_Workshops_Id_workshop_id",
                        column: x => x.Id_workshop_id,
                        principalTable: "Workshops",
                        principalColumn: "Id_workshop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id_comment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_user_id = table.Column<int>(type: "int", nullable: false),
                    Id_workshop_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id_comment);
                    table.ForeignKey(
                        name: "FK_Comments_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Workshops_Id_workshop_id",
                        column: x => x.Id_workshop_id,
                        principalTable: "Workshops",
                        principalColumn: "Id_workshop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Progressions",
                columns: table => new
                {
                    Id_progression = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Progression_status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_user_id = table.Column<int>(type: "int", nullable: false),
                    Id_workshop_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressions", x => x.Id_progression);
                    table.ForeignKey(
                        name: "FK_Progressions_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progressions_Workshops_Id_workshop_id",
                        column: x => x.Id_workshop_id,
                        principalTable: "Workshops",
                        principalColumn: "Id_workshop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id_Report = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tittle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_user_id = table.Column<int>(type: "int", nullable: false),
                    Id_workshop_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id_Report);
                    table.ForeignKey(
                        name: "FK_Reports_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Workshops_Id_workshop_id",
                        column: x => x.Id_workshop_id,
                        principalTable: "Workshops",
                        principalColumn: "Id_workshop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id_subscription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_user_id = table.Column<int>(type: "int", nullable: false),
                    Id_workshop_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id_subscription);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_Id_user_id",
                        column: x => x.Id_user_id,
                        principalTable: "Users",
                        principalColumn: "Id_user",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Workshops_Id_workshop_id",
                        column: x => x.Id_workshop_id,
                        principalTable: "Workshops",
                        principalColumn: "Id_workshop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopMultimedia",
                columns: table => new
                {
                    Id_workshop_id = table.Column<int>(type: "int", nullable: false),
                    Id_multimedia_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopMultimedia", x => new { x.Id_workshop_id, x.Id_multimedia_id });
                    table.ForeignKey(
                        name: "FK_WorkshopMultimedia_Multimedia_Id_multimedia_id",
                        column: x => x.Id_multimedia_id,
                        principalTable: "Multimedia",
                        principalColumn: "Id_multimedia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopMultimedia_Workshops_Id_workshop_id",
                        column: x => x.Id_workshop_id,
                        principalTable: "Workshops",
                        principalColumn: "Id_workshop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_Id_workshop_id",
                table: "Calendars",
                column: "Id_workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Id_user_id",
                table: "Comments",
                column: "Id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Id_workshop_id",
                table: "Comments",
                column: "Id_workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_Id_user_id",
                table: "Logs",
                column: "Id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Progressions_Id_user_id",
                table: "Progressions",
                column: "Id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Progressions_Id_workshop_id",
                table: "Progressions",
                column: "Id_workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Id_user_id",
                table: "Reports",
                column: "Id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Id_workshop_id",
                table: "Reports",
                column: "Id_workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Id_user_id",
                table: "Sessions",
                column: "Id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Id_user_id",
                table: "Subscriptions",
                column: "Id_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_Id_workshop_id",
                table: "Subscriptions",
                column: "Id_workshop_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Id_rol_id",
                table: "Users",
                column: "Id_rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopMultimedia_Id_multimedia_id",
                table: "WorkshopMultimedia",
                column: "Id_multimedia_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_Id_type_id",
                table: "Workshops",
                column: "Id_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_Id_user_id",
                table: "Workshops",
                column: "Id_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Progressions");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "WorkshopMultimedia");

            migrationBuilder.DropTable(
                name: "Multimedia");

            migrationBuilder.DropTable(
                name: "Workshops");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkshopTypes");

            migrationBuilder.DropTable(
                name: "Rols");
        }
    }
}
