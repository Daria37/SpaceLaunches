using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpaceLaunch.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Launches_rocket_RocketID",
                table: "Launches");

            migrationBuilder.DropForeignKey(
                name: "FK_rocket_Agency_AgencyID",
                table: "rocket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rocket",
                table: "rocket");

            migrationBuilder.DropColumn(
                name: "Config",
                table: "rocket");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "rocket");

            migrationBuilder.RenameTable(
                name: "rocket",
                newName: "Rocket");

            migrationBuilder.RenameIndex(
                name: "IX_rocket_AgencyID",
                table: "Rocket",
                newName: "IX_Rocket_AgencyID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rocket",
                table: "Rocket",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Launches_Rocket_RocketID",
                table: "Launches",
                column: "RocketID",
                principalTable: "Rocket",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rocket_Agency_AgencyID",
                table: "Rocket",
                column: "AgencyID",
                principalTable: "Agency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Launches_Rocket_RocketID",
                table: "Launches");

            migrationBuilder.DropForeignKey(
                name: "FK_Rocket_Agency_AgencyID",
                table: "Rocket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rocket",
                table: "Rocket");

            migrationBuilder.RenameTable(
                name: "Rocket",
                newName: "rocket");

            migrationBuilder.RenameIndex(
                name: "IX_Rocket_AgencyID",
                table: "rocket",
                newName: "IX_rocket_AgencyID");

            migrationBuilder.AddColumn<string>(
                name: "Config",
                table: "rocket",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "rocket",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rocket",
                table: "rocket",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Launches_rocket_RocketID",
                table: "Launches",
                column: "RocketID",
                principalTable: "rocket",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_rocket_Agency_AgencyID",
                table: "rocket",
                column: "AgencyID",
                principalTable: "Agency",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
