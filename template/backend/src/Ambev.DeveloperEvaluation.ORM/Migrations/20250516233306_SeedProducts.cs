using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "IsActive", "Name", "Price", "SKU" },
                values: new object[,]
                {
                    { new Guid("00ae5627-b295-4cd6-89aa-8f5d058301bb"), "Água mineral de produção própria da Ambev - 500ml", true, "AMA Natural", 2.99m, "AMA-NAT-500" },
                    { new Guid("10539897-0d63-4426-be14-fef7ebec6f54"), "Bebida isotônica de produção própria da Ambev - 500ml", true, "Gatorade Limão", 5.99m, "GAT-LIM-500" },
                    { new Guid("1294650b-1575-4295-9377-66f14d43c1b3"), "Refrigerante de produção própria da Ambev - 350ml", true, "Guaraná Antarctica", 3.99m, "GUA-ANT-350" },
                    { new Guid("2f36cd3a-0b4c-496a-92a6-41391307be47"), "Cerveja de produção própria da Ambev - 350ml", true, "Skol Beats", 6.99m, "SKL-BTS-350" },
                    { new Guid("313dad6b-2857-4e05-a767-610ed9b5c14f"), "Refrigerante de produção própria da Ambev - 350ml", true, "Pepsi Zero", 4.29m, "PEP-ZERO-350" },
                    { new Guid("90397c53-458c-4ded-8c35-067ee36e0c12"), "Cerveja de produção própria da Ambev - 350ml", true, "Brahma Chopp", 4.99m, "BRH-CHOPP-350" },
                    { new Guid("ba6dec6a-30eb-4987-a72f-e9ad47f360d6"), "Refrigerante de produção própria da Ambev - 350ml", true, "Guaraná Antarctica Zero", 4.29m, "GUA-ANT-ZERO-350" },
                    { new Guid("be57af16-8590-4e83-b2b2-8111268511c2"), "Bebida isotônica de produção própria da Ambev - 500ml", true, "Gatorade Laranja", 5.99m, "GAT-LAR-500" },
                    { new Guid("cf084da8-3cbb-40ae-8146-28e305c3c278"), "Refrigerante de produção própria da Ambev - 350ml", true, "Pepsi", 3.99m, "PEP-350" },
                    { new Guid("d993bbbf-de1c-4e61-89c4-8a032647e16e"), "Cerveja de produção própria da Ambev - 350ml", true, "Brahma Duplo Malte", 5.99m, "BRH-DM-350" },
                    { new Guid("e13e685d-82fd-4e55-b34b-3d40ee68becb"), "Cerveja de produção própria da Ambev - 350ml", true, "Skol Pilsen", 4.50m, "SKL-PIL-350" },
                    { new Guid("ecc512f8-589d-468a-9ef4-adf5798b4a6a"), "Água mineral de produção própria da Ambev - 500ml", true, "AMA Com Gás", 3.29m, "AMA-GAS-500" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("00ae5627-b295-4cd6-89aa-8f5d058301bb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("10539897-0d63-4426-be14-fef7ebec6f54"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1294650b-1575-4295-9377-66f14d43c1b3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2f36cd3a-0b4c-496a-92a6-41391307be47"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("313dad6b-2857-4e05-a767-610ed9b5c14f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("90397c53-458c-4ded-8c35-067ee36e0c12"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ba6dec6a-30eb-4987-a72f-e9ad47f360d6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("be57af16-8590-4e83-b2b2-8111268511c2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cf084da8-3cbb-40ae-8146-28e305c3c278"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d993bbbf-de1c-4e61-89c4-8a032647e16e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e13e685d-82fd-4e55-b34b-3d40ee68becb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ecc512f8-589d-468a-9ef4-adf5798b4a6a"));
        }
    }
}
