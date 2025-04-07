using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KutseApp_Viblyy.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThinkingCountToHoliday2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThinkingCount",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThinkingCount",
                table: "Holidays");
        }
    }
}
