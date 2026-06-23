using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_ZoranSimeunovic.Migrations
{
    /// <inheritdoc />
    public partial class UseTimestampsForNotificationFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "completion_notification_sent_at",
                table: "questionnaire",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationEmailSentAt",
                table: "contact_leads",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OptOutNotificationSentAt",
                table: "contact_leads",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QuestionnaireEmailSentAt",
                table: "contact_leads",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationNotificationSentAt",
                table: "contact_leads",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "completion_notification_sent_at",
                table: "questionnaire");

            migrationBuilder.DropColumn(
                name: "ConfirmationEmailSentAt",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "OptOutNotificationSentAt",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "QuestionnaireEmailSentAt",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "RegistrationNotificationSentAt",
                table: "contact_leads");

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
    }
}
