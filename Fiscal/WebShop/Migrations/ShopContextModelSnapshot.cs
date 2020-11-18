﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.Data;

namespace WebShop.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebShop.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRetail")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "Drink Store Address",
                            Email = "drinkstore@gmail.com",
                            IsRetail = false,
                            Name = "DrinkStore"
                        },
                        new
                        {
                            CustomerId = 2,
                            Address = "Ivan Address",
                            Email = "ivan@gmail.com",
                            IsRetail = true,
                            Name = "Ivan"
                        });
                });

            modelBuilder.Entity("WebShop.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("InvoiceId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            InvoiceId = 1,
                            CustomerId = 1,
                            DateCreated = new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Local)
                        },
                        new
                        {
                            InvoiceId = 2,
                            CustomerId = 2,
                            DateCreated = new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Local)
                        });
                });

            modelBuilder.Entity("WebShop.Models.InvoiceIteam", b =>
                {
                    b.Property<int>("InvoiceIteamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("InvoiceIteamId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoiceIteams");

                    b.HasData(
                        new
                        {
                            InvoiceIteamId = 1,
                            InvoiceId = 1,
                            ProductId = 1,
                            Quantity = 10m
                        },
                        new
                        {
                            InvoiceIteamId = 2,
                            InvoiceId = 1,
                            ProductId = 2,
                            Quantity = 2m
                        },
                        new
                        {
                            InvoiceIteamId = 3,
                            InvoiceId = 2,
                            ProductId = 1,
                            Quantity = 20m
                        },
                        new
                        {
                            InvoiceIteamId = 4,
                            InvoiceId = 2,
                            ProductId = 3,
                            Quantity = 3m
                        });
                });

            modelBuilder.Entity("WebShop.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("ProductTable");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Name = "Beer",
                            Price = 100m
                        },
                        new
                        {
                            ProductId = 2,
                            Name = "Vodka",
                            Price = 500m
                        },
                        new
                        {
                            ProductId = 3,
                            Name = "Tequila",
                            Price = 1200m
                        });
                });

            modelBuilder.Entity("WebShop.Models.Invoice", b =>
                {
                    b.HasOne("WebShop.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebShop.Models.InvoiceIteam", b =>
                {
                    b.HasOne("WebShop.Models.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}