using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtworkSharingPlatform.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackageInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Credit = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    RemainingCredit = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Artworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    ReleaseCount = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artworks_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConfigManagers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConfigDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsServicePackageConfig = table.Column<bool>(type: "bit", nullable: false),
                    IsPhysicalImageConfig = table.Column<bool>(type: "bit", nullable: false),
                    MaxReleaseCount = table.Column<int>(type: "int", nullable: false),
                    IsGeneralConfig = table.Column<bool>(type: "bit", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPagingConfig = table.Column<bool>(type: "bit", nullable: false),
                    TotalItemPerPage = table.Column<int>(type: "int", nullable: false),
                    RowSize = table.Column<int>(type: "int", nullable: false),
                    IsAdvertisementConfig = table.Column<bool>(type: "bit", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdministratorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigManagers_Users_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ArtworkImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsThumbnail = table.Column<bool>(type: "bit", nullable: true),
                    ArtworkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtworkImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtworkImages_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ArtworkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ArtworkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Likes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PreOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtworkId = table.Column<int>(type: "int", nullable: false),
                    EstimateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    BuyerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreOrders_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreOrders_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    ArtworkId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Artworks_ArtworkId",
                        column: x => x.ArtworkId,
                        principalTable: "Artworks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConfigManagerPackageInformation",
                columns: table => new
                {
                    ConfigManagersId = table.Column<int>(type: "int", nullable: false),
                    PackageConfigsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigManagerPackageInformation", x => new { x.ConfigManagersId, x.PackageConfigsId });
                    table.ForeignKey(
                        name: "FK_ConfigManagerPackageInformation_ConfigManagers_ConfigManagersId",
                        column: x => x.ConfigManagersId,
                        principalTable: "ConfigManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConfigManagerPackageInformation_PackageInformation_PackageConfigsId",
                        column: x => x.PackageConfigsId,
                        principalTable: "PackageInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackageBilling",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageBilling", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackageBilling_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PackageBilling_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackageBillingPackageInformation",
                columns: table => new
                {
                    PackageBillingsId = table.Column<int>(type: "int", nullable: false),
                    PackageInformationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageBillingPackageInformation", x => new { x.PackageBillingsId, x.PackageInformationId });
                    table.ForeignKey(
                        name: "FK_PackageBillingPackageInformation_PackageBilling_PackageBillingsId",
                        column: x => x.PackageBillingsId,
                        principalTable: "PackageBilling",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageBillingPackageInformation_PackageInformation_PackageInformationId",
                        column: x => x.PackageInformationId,
                        principalTable: "PackageInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtworkImages_ArtworkId",
                table: "ArtworkImages",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Artworks_OwnerId",
                table: "Artworks",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArtworkId",
                table: "Comments",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigManagerPackageInformation_PackageConfigsId",
                table: "ConfigManagerPackageInformation",
                column: "PackageConfigsId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfigManagers_AdministratorId",
                table: "ConfigManagers",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ArtworkId",
                table: "Likes",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageBilling_TransactionId",
                table: "PackageBilling",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageBilling_UserId",
                table: "PackageBilling",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageBillingPackageInformation_PackageInformationId",
                table: "PackageBillingPackageInformation",
                column: "PackageInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_ArtworkId",
                table: "PreOrders",
                column: "ArtworkId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreOrders_BuyerId",
                table: "PreOrders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ArtworkId",
                table: "Ratings",
                column: "ArtworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ManagerId",
                table: "Transactions",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtworkImages");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ConfigManagerPackageInformation");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "PackageBillingPackageInformation");

            migrationBuilder.DropTable(
                name: "PreOrders");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "ConfigManagers");

            migrationBuilder.DropTable(
                name: "PackageBilling");

            migrationBuilder.DropTable(
                name: "PackageInformation");

            migrationBuilder.DropTable(
                name: "Artworks");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
