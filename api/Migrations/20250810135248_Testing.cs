using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceLaunch.Migrations
{
    /// <inheritdoc />
    public partial class Testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rocket_Agency_AgencyID",
                table: "Rocket");

            migrationBuilder.DropIndex(
                name: "IX_Rocket_AgencyID",
                table: "Rocket");

            migrationBuilder.DropColumn(
                name: "Configuration",
                table: "Rocket");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Configuration",
                table: "Rocket",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rocket_AgencyID",
                table: "Rocket",
                column: "AgencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rocket_Agency_AgencyID",
                table: "Rocket",
                column: "AgencyID",
                principalTable: "Agency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
