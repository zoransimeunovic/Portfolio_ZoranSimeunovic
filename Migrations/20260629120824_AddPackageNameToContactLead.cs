using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_ZoranSimeunovic.Migrations
{
    /// <inheritdoc />
    public partial class AddPackageNameToContactLead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackageName",
                table: "contact_leads",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageName",
                table: "contact_leads");
        }
    }
}
