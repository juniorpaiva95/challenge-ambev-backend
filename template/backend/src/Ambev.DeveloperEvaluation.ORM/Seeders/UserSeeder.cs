using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ambev.DeveloperEvaluation.ORM.Seeders;

public static class UserSeeder
{
    public static void SeedUsers(this ModelBuilder modelBuilder)
    {
        // Senhas: Admin123!, Manager123!, Customer123!
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = new Guid("e2028bc7-d451-421c-894c-95e31c9f4dbb"),
                Username = "Administrador",
                Email = "admin@ambev.com",
                Phone = "(11) 99999-0001",
                Password = "$2a$11$w8QwQn6QwQn6QwQn6QwQnOQwQn6QwQn6QwQn6QwQn6QwQn6QwQn6", // hash fictício
                Role = UserRole.Admin,
                Status = UserStatus.Active,
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = new Guid("0e1819b2-00ac-47e6-bbe3-5dcfbe7ef812"),
                Username = "Gerente",
                Email = "manager@ambev.com",
                Phone = "(11) 99999-0002",
                Password = "$2a$11$w8QwQn6QwQn6QwQn6QwQnOQwQn6QwQn6QwQn6QwQn6QwQn6QwQn6", // hash fictício
                Role = UserRole.Manager,
                Status = UserStatus.Active,
                CreatedAt = new DateTime(2024, 1, 2, 12, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = new Guid("e1fb2153-2a32-437d-82fb-2a440cf34f95"),
                Username = "Cliente",
                Email = "customer@ambev.com",
                Phone = "(11) 99999-0003",
                Password = "$2a$11$w8QwQn6QwQn6QwQn6QwQnOQwQn6QwQn6QwQn6QwQn6QwQn6QwQn6", // hash fictício
                Role = UserRole.Customer,
                Status = UserStatus.Active,
                CreatedAt = new DateTime(2024, 1, 3, 12, 0, 0, DateTimeKind.Utc)
            }
        );
    }
} 