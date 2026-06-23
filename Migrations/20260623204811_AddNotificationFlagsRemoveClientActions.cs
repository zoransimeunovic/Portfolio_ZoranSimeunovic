using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_ZoranSimeunovic.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationFlagsRemoveClientActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_actions");

            migrationBuilder.AddColumn<bool>(
                name: "completion_notification_sent",
                table: "questionnaire",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmationEmailSent",
                table: "contact_leads",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OptOutNotificationSent",
                table: "contact_leads",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "QuestionnaireEmailSent",
                table: "contact_leads",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RegistrationNotificationSent",
                table: "contact_leads",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completion_notification_sent",
                table: "questionnaire");

            migrationBuilder.DropColumn(
                name: "ConfirmationEmailSent",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "OptOutNotificationSent",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "QuestionnaireEmailSent",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "RegistrationNotificationSent",
                table: "contact_leads");

            migrationBuilder.CreateTable(
                name: "client_actions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    contact_id = table.Column<int>(type: "int", nullable: false),
                    action_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    executed_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_actions", x => x.id);
                    table.ForeignKey(
                        name: "FK_client_actions_contact_leads_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contact_leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_client_actions_contact_id",
                table: "client_actions",
                column: "contact_id");
        }
    }
}
