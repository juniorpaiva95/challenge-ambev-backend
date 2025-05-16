using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Seeders;

public static class ProductSeeder
{
    public static void SeedProducts(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            // Cervejas
            new Product
            {
                Id = new Guid("90397c53-458c-4ded-8c35-067ee36e0c12"),
                Name = "Brahma Chopp",
                Description = "Cerveja de produção própria da Ambev - 350ml",
                Price = 4.99m,
                SKU = "BRH-CHOPP-350",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("d993bbbf-de1c-4e61-89c4-8a032647e16e"),
                Name = "Brahma Duplo Malte",
                Description = "Cerveja de produção própria da Ambev - 350ml",
                Price = 5.99m,
                SKU = "BRH-DM-350",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("e13e685d-82fd-4e55-b34b-3d40ee68becb"),
                Name = "Skol Pilsen",
                Description = "Cerveja de produção própria da Ambev - 350ml",
                Price = 4.50m,
                SKU = "SKL-PIL-350",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("2f36cd3a-0b4c-496a-92a6-41391307be47"),
                Name = "Skol Beats",
                Description = "Cerveja de produção própria da Ambev - 350ml",
                Price = 6.99m,
                SKU = "SKL-BTS-350",
                IsActive = true
            },
            // Refrigerantes
            new Product
            {
                Id = new Guid("1294650b-1575-4295-9377-66f14d43c1b3"),
                Name = "Guaraná Antarctica",
                Description = "Refrigerante de produção própria da Ambev - 350ml",
                Price = 3.99m,
                SKU = "GUA-ANT-350",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("ba6dec6a-30eb-4987-a72f-e9ad47f360d6"),
                Name = "Guaraná Antarctica Zero",
                Description = "Refrigerante de produção própria da Ambev - 350ml",
                Price = 4.29m,
                SKU = "GUA-ANT-ZERO-350",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("cf084da8-3cbb-40ae-8146-28e305c3c278"),
                Name = "Pepsi",
                Description = "Refrigerante de produção própria da Ambev - 350ml",
                Price = 3.99m,
                SKU = "PEP-350",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("313dad6b-2857-4e05-a767-610ed9b5c14f"),
                Name = "Pepsi Zero",
                Description = "Refrigerante de produção própria da Ambev - 350ml",
                Price = 4.29m,
                SKU = "PEP-ZERO-350",
                IsActive = true
            },
            // Outros
            new Product
            {
                Id = new Guid("10539897-0d63-4426-be14-fef7ebec6f54"),
                Name = "Gatorade Limão",
                Description = "Bebida isotônica de produção própria da Ambev - 500ml",
                Price = 5.99m,
                SKU = "GAT-LIM-500",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("be57af16-8590-4e83-b2b2-8111268511c2"),
                Name = "Gatorade Laranja",
                Description = "Bebida isotônica de produção própria da Ambev - 500ml",
                Price = 5.99m,
                SKU = "GAT-LAR-500",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("00ae5627-b295-4cd6-89aa-8f5d058301bb"),
                Name = "AMA Natural",
                Description = "Água mineral de produção própria da Ambev - 500ml",
                Price = 2.99m,
                SKU = "AMA-NAT-500",
                IsActive = true
            },
            new Product
            {
                Id = new Guid("ecc512f8-589d-468a-9ef4-adf5798b4a6a"),
                Name = "AMA Com Gás",
                Description = "Água mineral de produção própria da Ambev - 500ml",
                Price = 3.29m,
                SKU = "AMA-GAS-500",
                IsActive = true
            }
        );
    }
}