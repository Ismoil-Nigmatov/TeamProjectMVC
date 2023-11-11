using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeamProjectMVC.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40faf2f2-8beb-4538-91c4-bf6043cce97e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "829b5ff4-a94e-4323-b1cf-39cd4c195b15");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38015abd-8ea1-4814-bd45-02d7c367a1bc", null, "ADMIN", "ADMIN" },
                    { "d3de1f4b-a575-4d05-bb6e-8db1a131473d", null, "USER", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "40faf2f2-8beb-4538-91c4-bf6043cce97e", null, "ADMIN", "ADMIN" },
                    { "829b5ff4-a94e-4323-b1cf-39cd4c195b15", null, "USER", "USER" }
                });
        }
    }
}
