using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamProjectMVC.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38015abd-8ea1-4814-bd45-02d7c367a1bc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d3de1f4b-a575-4d05-bb6e-8db1a131473d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "087f617a-6d3a-4061-8f9f-cd5cc4763231", null, "USER", "USER" },
                    { "e116c0cd-4005-4598-899e-c005fece7907", null, "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "087f617a-6d3a-4061-8f9f-cd5cc4763231");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e116c0cd-4005-4598-899e-c005fece7907");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38015abd-8ea1-4814-bd45-02d7c367a1bc", null, "ADMIN", "ADMIN" },
                    { "d3de1f4b-a575-4d05-bb6e-8db1a131473d", null, "USER", "USER" }
                });
        }
    }
}
