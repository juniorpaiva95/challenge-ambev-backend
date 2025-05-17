using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Password", "Phone", "Role", "Status", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { new Guid("0e1819b2-00ac-47e6-bbe3-5dcfbe7ef812"), new DateTime(2024, 1, 2, 12, 0, 0, 0, DateTimeKind.Utc), "manager@ambev.com", "$2a$11$w8QwQn6QwQn6QwQn6QwQnOQwQn6QwQn6QwQn6QwQn6QwQn6QwQn6", "(11) 99999-0002", "Manager", "Active", null, "Gerente" },
                    { new Guid("e1fb2153-2a32-437d-82fb-2a440cf34f95"), new DateTime(2024, 1, 3, 12, 0, 0, 0, DateTimeKind.Utc), "customer@ambev.com", "$2a$11$w8QwQn6QwQn6QwQn6QwQnOQwQn6QwQn6QwQn6QwQn6QwQn6QwQn6", "(11) 99999-0003", "Customer", "Active", null, "Cliente" },
                    { new Guid("e2028bc7-d451-421c-894c-95e31c9f4dbb"), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Utc), "admin@ambev.com", "$2a$11$w8QwQn6QwQn6QwQn6QwQnOQwQn6QwQn6QwQn6QwQn6QwQn6QwQn6", "(11) 99999-0001", "Admin", "Active", null, "Administrador" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_CustomerId",
                table: "Sales");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0e1819b2-00ac-47e6-bbe3-5dcfbe7ef812"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e1fb2153-2a32-437d-82fb-2a440cf34f95"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e2028bc7-d451-421c-894c-95e31c9f4dbb"));
        }
    }
}
