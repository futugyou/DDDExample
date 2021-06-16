﻿// <auto-generated />
using System;
using Example.Infrastruct.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Example.Infrastruct.Data.Migrations.EventStoreSQL
{
    [DbContext(typeof(EventStoreSQLContext))]
    [Migration("20210507050018_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Example.Domain.Core.Events.StoredEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Data")
                        .HasColumnType("longtext");

                    b.Property<string>("MessageType")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Action");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("CreateDate");

                    b.Property<string>("User")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("StoredEvents");
                });
#pragma warning restore 612, 618
        }
    }
}