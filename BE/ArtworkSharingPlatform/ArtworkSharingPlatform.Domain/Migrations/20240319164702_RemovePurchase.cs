using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtworkSharingPlatform.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemovePurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    ArtworkId = table.Column<int>(type: "int", nullable: false),
                    SellUserId = table.Column<int>(type: "int", nullable: false),
                    BuyUserId = table.Column<int>(type: "int", nullable: false),
                    BuyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => new { x.ArtworkId, x.SellUserId, x.BuyUserId });
                    table.ForeignKey(
                        name: "FK_Purchases_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_AspNetUsers_BuyUserId",
                        column: x => x.BuyUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Purchases_AspNetUsers_SellUserId",
                        column: x => x.SellUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BuyUserId",
                table: "Purchases",
                column: "BuyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SellUserId",
                table: "Purchases",
                column: "SellUserId");
        }
    }
}
