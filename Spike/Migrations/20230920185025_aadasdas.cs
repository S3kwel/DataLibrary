using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Spike.Migrations
{
    /// <inheritdoc />
    public partial class aadasdas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "StringId",
                table: "AuthorV1")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AuthorHistoricV1")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.InsertData(
                table: "AuthorV1",
                columns: new[] { "Id", "FirstName", "IsDeleted", "LastName", "VersionTag" },
                values: new object[,]
                {
                    { new Guid("146eed62-79d3-43e7-8272-abb24be979bb"), "TEST", false, "TEST", new Guid("d44ed1da-d35f-44b9-94e5-e8628ee5bcd2") },
                    { new Guid("4d549e66-92c1-470d-8e01-7e0fea724a99"), "TEST3", false, "TEST3", new Guid("47b10859-c32e-4337-a1b7-bad81c4cd4ba") },
                    { new Guid("68db927a-4199-473c-8f11-1a4aadfcae25"), "TEST4", false, "TEST4", new Guid("710312c8-0453-4f44-beff-28bc8b53bebc") },
                    { new Guid("7e0a3c74-888a-4f33-8eaf-3e3aad9ee6eb"), "Dustin", false, "Hickman", new Guid("110b982e-b412-4959-adb7-a52da3a1d21d") },
                    { new Guid("9aae96e0-71d6-4b83-a28e-9bb0175a3221"), "TEST5", false, "TEST5", new Guid("1a08a3df-e8c9-40e9-80f7-da84f3dfa2f3") },
                    { new Guid("c2020461-f96d-4b3d-a951-9133be91c529"), "TEST2", false, "TEST2", new Guid("58039359-fe12-4c57-8327-0a6cad5c2b4c") }
                });

            migrationBuilder.InsertData(
                table: "DocumentV1",
                columns: new[] { "Id", "Description", "IsDeleted", "Title", "VersionTag" },
                values: new object[,]
                {
                    { new Guid("30cb5bbd-5db9-4a74-b722-7079bce0b028"), "TEST DESCRIPTION", false, "DOCUMENT 6", new Guid("8ab6f9e8-7fbe-4b68-8566-d3fd563d62ea") },
                    { new Guid("3f0b60f3-f97e-4e7d-9d4f-12fd2f2ee01e"), "TEST DESCRIPTION", false, "DOCUMENT 1", new Guid("cb17117e-fa5b-4955-90e3-ef8722a9490f") },
                    { new Guid("60016ac8-7304-40a5-9e3f-b45b4e504fbe"), "TEST DESCRIPTION", false, "DOCUMENT 8", new Guid("a94511bd-cbc6-4cba-ad13-60f8562f21a6") },
                    { new Guid("75cfc5a0-ccfc-4ebf-bd19-533ab61baba6"), "TEST DESCRIPTION", false, "DOCUMENT 2", new Guid("00f8f26c-608d-49b0-97c1-56eff9c921e9") },
                    { new Guid("80c1e35d-94f1-4ace-b3e4-44c261ebecba"), "TEST DESCRIPTION", false, "DOCUMENT 7", new Guid("9d21f275-e3a3-479a-83a9-f4a9240ebc3d") },
                    { new Guid("99f0ad36-a94a-4876-955b-a18f7d812ea2"), "TEST DESCRIPTION", false, "DOCUMENT 5", new Guid("825715ff-ec25-4498-8fcf-4c02abcaa32c") },
                    { new Guid("baaa9b61-0b55-4461-ad93-99bf2fa76235"), "TEST DESCRIPTION", false, "DOCUMENT 4", new Guid("c86ea14e-7b04-4f9e-8dac-f891fc5f2178") },
                    { new Guid("d778974e-32f7-4d64-b0cd-c04b1564f517"), "TEST DESCRIPTION", false, "DOCUMENT 3", new Guid("9e371d22-09f1-4670-bb71-855e651b19d7") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AuthorV1",
                keyColumn: "Id",
                keyValue: new Guid("146eed62-79d3-43e7-8272-abb24be979bb"));

            migrationBuilder.DeleteData(
                table: "AuthorV1",
                keyColumn: "Id",
                keyValue: new Guid("4d549e66-92c1-470d-8e01-7e0fea724a99"));

            migrationBuilder.DeleteData(
                table: "AuthorV1",
                keyColumn: "Id",
                keyValue: new Guid("68db927a-4199-473c-8f11-1a4aadfcae25"));

            migrationBuilder.DeleteData(
                table: "AuthorV1",
                keyColumn: "Id",
                keyValue: new Guid("7e0a3c74-888a-4f33-8eaf-3e3aad9ee6eb"));

            migrationBuilder.DeleteData(
                table: "AuthorV1",
                keyColumn: "Id",
                keyValue: new Guid("9aae96e0-71d6-4b83-a28e-9bb0175a3221"));

            migrationBuilder.DeleteData(
                table: "AuthorV1",
                keyColumn: "Id",
                keyValue: new Guid("c2020461-f96d-4b3d-a951-9133be91c529"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("30cb5bbd-5db9-4a74-b722-7079bce0b028"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("3f0b60f3-f97e-4e7d-9d4f-12fd2f2ee01e"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("60016ac8-7304-40a5-9e3f-b45b4e504fbe"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("75cfc5a0-ccfc-4ebf-bd19-533ab61baba6"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("80c1e35d-94f1-4ace-b3e4-44c261ebecba"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("99f0ad36-a94a-4876-955b-a18f7d812ea2"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("baaa9b61-0b55-4461-ad93-99bf2fa76235"));

            migrationBuilder.DeleteData(
                table: "DocumentV1",
                keyColumn: "Id",
                keyValue: new Guid("d778974e-32f7-4d64-b0cd-c04b1564f517"));

            migrationBuilder.AddColumn<string>(
                name: "StringId",
                table: "AuthorV1",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "AuthorHistoricV1")
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
    }
}
