using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtworkSharingPlatform.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministratorIdForConfigManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigManagers_AspNetUsers_AdministratorId",
                table: "ConfigManagers");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "ConfigManagers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigManagers_AspNetUsers_AdministratorId",
                table: "ConfigManagers",
                column: "AdministratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigManagers_AspNetUsers_AdministratorId",
                table: "ConfigManagers");

            migrationBuilder.AlterColumn<int>(
                name: "AdministratorId",
                table: "ConfigManagers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigManagers_AspNetUsers_AdministratorId",
                table: "ConfigManagers",
                column: "AdministratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
