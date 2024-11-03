﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProyectoVentas.Context;

#nullable disable

namespace ProyectoVentas.Migrations
{
    [DbContext(typeof(LocalVentasDatabaseContext))]
    partial class LocalVentasDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProyectoVentas.Models.Prenda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("precio")
                        .HasColumnType("float");

                    b.Property<int>("publico")
                        .HasColumnType("int");

                    b.Property<int>("stock")
                        .HasColumnType("int");

                    b.Property<int>("talle")
                        .HasColumnType("int");

                    b.Property<int>("tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Prendas");
                });

            modelBuilder.Entity("ProyectoVentas.Models.Reposicion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaReposicion")
                        .HasColumnType("datetime2");

                    b.Property<int>("PrendaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrendaId");

                    b.ToTable("Reposiciones");
                });

            modelBuilder.Entity("ProyectoVentas.Models.Reposicion", b =>
                {
                    b.HasOne("ProyectoVentas.Models.Prenda", "Prenda")
                        .WithMany()
                        .HasForeignKey("PrendaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prenda");
                });
#pragma warning restore 612, 618
        }
    }
}
