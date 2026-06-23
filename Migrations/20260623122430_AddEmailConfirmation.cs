using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_ZoranSimeunovic.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "contact_leads",
                type: "varchar(64)",
                maxLength: 64,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmationTokenExpiresAt",
                table: "contact_leads",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailConfirmedAt",
                table: "contact_leads",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_contact_leads_ConfirmationToken",
                table: "contact_leads",
                column: "ConfirmationToken",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_contact_leads_ConfirmationToken",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "ConfirmationTokenExpiresAt",
                table: "contact_leads");

            migrationBuilder.DropColumn(
                name: "EmailConfirmedAt",
                table: "contact_leads");
        }
    }
}
