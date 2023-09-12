using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spike.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorHistoricV1DocumentHistoricV1",
                columns: table => new
                {
                    AuthorsHistoricV1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentsHistoricV1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorHistoricV1DocumentHistoricV1", x => new { x.AuthorsHistoricV1Id, x.DocumentsHistoricV1Id });
                });

            migrationBuilder.CreateTable(
                name: "AuthorsV1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VersionTag = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsV1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsV1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VersionTag = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsV1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorV1DocumentV1",
                columns: table => new
                {
                    AuthorsV1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentsV1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorV1DocumentV1", x => new { x.AuthorsV1Id, x.DocumentsV1Id });
                    table.ForeignKey(
                        name: "FK_AuthorV1DocumentV1_AuthorsV1_AuthorsV1Id",
                        column: x => x.AuthorsV1Id,
                        principalTable: "AuthorsV1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorV1DocumentV1_DocumentsV1_DocumentsV1Id",
                        column: x => x.DocumentsV1Id,
                        principalTable: "DocumentsV1",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorHistoricV1DocumentHistoricV1_DocumentsHistoricV1Id",
                table: "AuthorHistoricV1DocumentHistoricV1",
                column: "DocumentsHistoricV1Id");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorV1DocumentV1_DocumentsV1Id",
                table: "AuthorV1DocumentV1",
                column: "DocumentsV1Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorHistoricV1DocumentHistoricV1");

            migrationBuilder.DropTable(
                name: "AuthorV1DocumentV1");

            migrationBuilder.DropTable(
                name: "AuthorsV1");

            migrationBuilder.DropTable(
                name: "DocumentsV1");
        }
    }
}
