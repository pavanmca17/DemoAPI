﻿// <auto-generated />
using EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DemoAPI.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20190505081026_AddColumnCost-Product")]
    partial class AddColumnCostProduct
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DemoAPI.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cost");

                    b.Property<string>("Desc")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("products");
                });

            modelBuilder.Entity("DemoAPI.Models.TestEmployee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("TestEmployee");
                });
#pragma warning restore 612, 618
        }
    }
}
