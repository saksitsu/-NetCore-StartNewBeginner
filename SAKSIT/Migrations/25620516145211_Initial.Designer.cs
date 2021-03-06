﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SAKSIT.Data;

namespace SAKSIT.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("25620516145211_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SAKSIT.Models.customer", b =>
                {
                    b.Property<string>("customer_id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("address")
                        .HasMaxLength(50);

                    b.Property<string>("firstname")
                        .HasMaxLength(50);

                    b.Property<string>("lastname")
                        .HasMaxLength(50);

                    b.HasKey("customer_id");

                    b.ToTable("customer");
                });

            modelBuilder.Entity("SAKSIT.Models.order_detail", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("amount");

                    b.Property<string>("order_id")
                        .HasMaxLength(50);

                    b.Property<string>("product_id")
                        .HasMaxLength(10);

                    b.Property<decimal>("qty");

                    b.HasKey("id");

                    b.HasIndex("order_id");

                    b.HasIndex("product_id");

                    b.ToTable("order_detail");
                });

            modelBuilder.Entity("SAKSIT.Models.order_list", b =>
                {
                    b.Property<string>("order_id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("customer_id");

                    b.Property<DateTime>("order_date");

                    b.Property<bool>("status");

                    b.HasKey("order_id");

                    b.HasIndex("customer_id");

                    b.ToTable("order_list");
                });

            modelBuilder.Entity("SAKSIT.Models.product", b =>
                {
                    b.Property<string>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10);

                    b.Property<string>("product_name")
                        .HasMaxLength(100);

                    b.HasKey("product_id");

                    b.ToTable("product");
                });

            modelBuilder.Entity("SAKSIT.Models.order_detail", b =>
                {
                    b.HasOne("SAKSIT.Models.order_list", "order_List")
                        .WithMany()
                        .HasForeignKey("order_id");

                    b.HasOne("SAKSIT.Models.product", "product")
                        .WithMany()
                        .HasForeignKey("product_id");
                });

            modelBuilder.Entity("SAKSIT.Models.order_list", b =>
                {
                    b.HasOne("SAKSIT.Models.customer", "customer")
                        .WithMany()
                        .HasForeignKey("customer_id");
                });
#pragma warning restore 612, 618
        }
    }
}
