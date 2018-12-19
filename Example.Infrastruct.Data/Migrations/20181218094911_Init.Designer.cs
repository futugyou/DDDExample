﻿// <auto-generated />
using System;
using Example.Infrastruct.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Example.Infrastruct.Data.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20181218094911_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview.18572.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Example.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(11);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Example.Domain.Models.Customer", b =>
                {
                    b.OwnsOne("Example.Domain.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("City");

                            b1.Property<string>("County");

                            b1.Property<string>("Province");

                            b1.Property<string>("Street");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.HasOne("Example.Domain.Models.Customer")
                                .WithOne("Address")
                                .HasForeignKey("Example.Domain.Models.Address", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
