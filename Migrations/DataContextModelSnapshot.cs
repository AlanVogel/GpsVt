﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpeedSight.Data;

#nullable disable

namespace SpeedSight.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SpeedSight.Models.GpsData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Link")
                        .HasColumnType("int");

                    b.Property<int>("Match_Distance")
                        .HasColumnType("int");

                    b.Property<int>("Match_H")
                        .HasColumnType("int");

                    b.Property<int>("Match_Speed")
                        .HasColumnType("int");

                    b.Property<double>("Match_X")
                        .HasColumnType("float");

                    b.Property<double>("Match_Y")
                        .HasColumnType("float");

                    b.Property<int>("Org_H")
                        .HasColumnType("int");

                    b.Property<int>("Org_Speed")
                        .HasColumnType("int");

                    b.Property<double>("Org_X")
                        .HasColumnType("float");

                    b.Property<double>("Org_Y")
                        .HasColumnType("float");

                    b.Property<int>("Utc")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GpsDatas");
                });
#pragma warning restore 612, 618
        }
    }
}
