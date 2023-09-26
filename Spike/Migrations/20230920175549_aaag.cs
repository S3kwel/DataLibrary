using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spike.Migrations
{
    /// <inheritdoc />
    public partial class aaag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorsV1",
                keyColumn: "Id",
                keyValue: new Guid("74ed5347-b141-4199-8d67-cf1d4518663f"));

            migrationBuilder.DeleteData(
                table: "AuthorsV1",
                keyColumn: "Id",
                keyValue: new Guid("91229e2c-080b-4278-9534-fa6e67c292f4"));

            migrationBuilder.DeleteData(
                table: "AuthorsV1",
                keyColumn: "Id",
                keyValue: new Guid("bbd174a6-7b4f-433c-81b8-566cf1c256cc"));

            migrationBuilder.DeleteData(
                table: "AuthorsV1",
                keyColumn: "Id",
                keyValue: new Guid("c4b55537-3a96-4fa4-b4ab-1e9370f43f1d"));

            migrationBuilder.DeleteData(
                table: "AuthorsV1",
                keyColumn: "Id",
                keyValue: new Guid("cb60381c-40f7-4f60-877d-8cec6e0f4f14"));

            migrationBuilder.DeleteData(
                table: "AuthorsV1",
                keyColumn: "Id",
                keyValue: new Guid("e9c65d02-fbd0-4d04-8f80-72434a4d8418"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("003841a5-ab79-4bdf-a542-f6f19ac7f82f"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("029918c4-e431-4e8f-a2f8-1126f6ba7ec9"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("22137203-08ef-471d-8e72-e38a2c83493b"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("46748805-5a5b-4026-a59d-5c9a5fb1f224"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("50d968af-b91d-4755-aaf2-44a0213f1579"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("742ddaae-dc5e-4e17-b417-4c71edcead9f"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("bed71ac2-af31-4924-8705-e09506864f2c"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("fb8ef815-7d12-429c-b722-ecf2c910d794"));

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

            migrationBuilder.AddColumn<string>(
                name: "StringId",
                table: "AuthorsV1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StringId",
                table: "DocumentV1")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DocumentHistoricV1")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropColumn(
                name: "StringId",
                table: "AuthorsV1");

            migrationBuilder.InsertData(
                table: "AuthorsV1",
                columns: new[] { "Id", "FirstName", "IsDeleted", "LastName", "VersionTag" },
                values: new object[,]
                {
                    { new Guid("74ed5347-b141-4199-8d67-cf1d4518663f"), "TEST5", false, "TEST5", new Guid("86ab04ff-417d-4588-9d19-159a8a31b7e2") },
                    { new Guid("91229e2c-080b-4278-9534-fa6e67c292f4"), "TEST", false, "TEST", new Guid("3310af3b-a082-4f43-8b1e-16cf518a7aca") },
                    { new Guid("bbd174a6-7b4f-433c-81b8-566cf1c256cc"), "TEST4", false, "TEST4", new Guid("54a0fcd2-88e8-41a3-81ad-1eab71ce67ae") },
                    { new Guid("c4b55537-3a96-4fa4-b4ab-1e9370f43f1d"), "TEST2", false, "TEST2", new Guid("5a089d88-b3ef-4353-aa3b-b0fb7cbf23a0") },
                    { new Guid("cb60381c-40f7-4f60-877d-8cec6e0f4f14"), "TEST3", false, "TEST3", new Guid("fbaf9e5f-efa2-4ab6-a13c-5dcd07374ea1") },
                    { new Guid("e9c65d02-fbd0-4d04-8f80-72434a4d8418"), "Dustin", false, "Hickman", new Guid("00776a78-d85f-4642-8ac6-ed41c3af0908") }
                });

            migrationBuilder.InsertData(
                table: "DocumentV1",
                columns: new[] { "Id", "Description", "IsDeleted", "Title", "VersionTag" },
                values: new object[,]
                {
                    { new Guid("003841a5-ab79-4bdf-a542-f6f19ac7f82f"), "TEST DESCRIPTION", false, "DOCUMENT 4", new Guid("60c75823-d1b5-4d28-87fc-1cc6d8a6ca6b") },
                    { new Guid("029918c4-e431-4e8f-a2f8-1126f6ba7ec9"), "TEST DESCRIPTION", false, "DOCUMENT 2", new Guid("361eb655-a3c9-4ba0-871c-076d2ab908ed") },
                    { new Guid("22137203-08ef-471d-8e72-e38a2c83493b"), "TEST DESCRIPTION", false, "DOCUMENT 6", new Guid("b9d61f5a-9a26-425d-8f5d-933af23ee89d") },
                    { new Guid("46748805-5a5b-4026-a59d-5c9a5fb1f224"), "TEST DESCRIPTION", false, "DOCUMENT 7", new Guid("3bc0107e-e2ed-4125-84f0-72efb7077de2") },
                    { new Guid("50d968af-b91d-4755-aaf2-44a0213f1579"), "TEST DESCRIPTION", false, "DOCUMENT 5", new Guid("a652dabf-dd83-442a-bcd8-50e8adcda9c9") },
                    { new Guid("742ddaae-dc5e-4e17-b417-4c71edcead9f"), "TEST DESCRIPTION", false, "DOCUMENT 8", new Guid("ab91b520-2db5-425d-be67-34a57fd526ca") },
                    { new Guid("bed71ac2-af31-4924-8705-e09506864f2c"), "TEST DESCRIPTION", false, "DOCUMENT 3", new Guid("c12f14e9-35e7-4ed1-b2ad-efc9f81ca987") },
                    { new Guid("fb8ef815-7d12-429c-b722-ecf2c910d794"), "TEST DESCRIPTION", false, "DOCUMENT 1", new Guid("5d361d70-133f-4ea6-a4a6-38d6c093d6f2") }
                });
        }
    }
}
