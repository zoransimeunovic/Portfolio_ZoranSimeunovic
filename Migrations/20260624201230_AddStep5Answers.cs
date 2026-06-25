using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio_ZoranSimeunovic.Migrations
{
    /// <inheritdoc />
    public partial class AddStep5Answers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Step5Answers",
                table: "questionnaire",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Step5Answers",
                table: "questionnaire");
        }
    }
}
