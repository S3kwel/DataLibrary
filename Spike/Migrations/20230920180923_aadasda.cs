using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spike.Migrations
{
    /// <inheritdoc />
    public partial class aadasda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StringId",
                table: "DocumentV1")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DocumentHistoricV1")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.InsertData(
                table: "DocumentV1",
                columns: new[] { "Id", "Description", "IsDeleted", "Title", "VersionTag" },
                values: new object[,]
                {
                    { new Guid("05c54966-eae1-4840-b8c5-63b03616e7d7"), "TEST DESCRIPTION", false, "DOCUMENT 1", new Guid("9ee61862-ad85-4f8b-9e45-a2d8caa7b176") },
                    { new Guid("2f239bf9-b8d3-49a4-8eb0-8a973364b080"), "TEST DESCRIPTION", false, "DOCUMENT 4", new Guid("234a0bd3-b8fc-4b14-a343-84ca5ca39c5e") },
                    { new Guid("424d1e00-7538-4075-b240-9f28b482dbde"), "TEST DESCRIPTION", false, "DOCUMENT 5", new Guid("f592402a-ddb4-4d4a-8533-a1b72a22ddbd") },
                    { new Guid("5197aca2-ad81-49a7-b4b6-a93a8695dd63"), "TEST DESCRIPTION", false, "DOCUMENT 3", new Guid("aea74fa2-e073-4ef9-bee1-c9eb9b9bf969") },
                    { new Guid("5e9e5905-aa15-4817-9fe3-e762a6510214"), "TEST DESCRIPTION", false, "DOCUMENT 7", new Guid("c31498b0-191b-46d9-8be4-7eddd07e26ae") },
                    { new Guid("6ce1b3d3-fdea-4f44-83c6-d461d4ff2ea9"), "TEST DESCRIPTION", false, "DOCUMENT 6", new Guid("c8d2d867-eb73-4164-b400-5ebb5105feb7") },
                    { new Guid("a7d9768a-2e65-4273-9d50-d0072efc89ca"), "TEST DESCRIPTION", false, "DOCUMENT 2", new Guid("5e2e6daa-4cb1-4d1d-85ed-32997bd3d378") },
                    { new Guid("f99d5fee-f8a0-4e8f-84b1-c3abd412bf5b"), "TEST DESCRIPTION", false, "DOCUMENT 8", new Guid("06292dcf-6538-4210-9105-ee84d9c7155a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("05c54966-eae1-4840-b8c5-63b03616e7d7"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("2f239bf9-b8d3-49a4-8eb0-8a973364b080"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("424d1e00-7538-4075-b240-9f28b482dbde"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("5197aca2-ad81-49a7-b4b6-a93a8695dd63"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("5e9e5905-aa15-4817-9fe3-e762a6510214"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("6ce1b3d3-fdea-4f44-83c6-d461d4ff2ea9"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("a7d9768a-2e65-4273-9d50-d0072efc89ca"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("f99d5fee-f8a0-4e8f-84b1-c3abd412bf5b"));

            migrationBuilder.AddColumn<string>(
                name: "StringId",
                table: "DocumentV1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DocumentHistoricV1")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}
