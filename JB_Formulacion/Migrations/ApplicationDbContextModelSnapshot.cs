﻿// <auto-generated />
using System;
using JB_Formulacion.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JB_Formulacion.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JB_Formulacion.Models.CantidadPorLote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cantidad")
                        .HasColumnType("float");

                    b.Property<double>("CantidadPesada")
                        .HasColumnType("float");

                    b.Property<double>("CantidadTotal")
                        .HasColumnType("float");

                    b.Property<string>("Lote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MateriaPrimaCodigo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("OrdenFabricacionNumOrdenFabricacion")
                        .HasColumnType("int");

                    b.Property<int?>("TransferenciaDocNumOf")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MateriaPrimaCodigo");

                    b.HasIndex("OrdenFabricacionNumOrdenFabricacion");

                    b.HasIndex("TransferenciaDocNumOf");

                    b.ToTable("Lotes");
                });

            modelBuilder.Entity("JB_Formulacion.Models.MateriaPrima", b =>
                {
                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnidadMedida")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("Materias");
                });

            modelBuilder.Entity("JB_Formulacion.Models.OrdenFabricacion", b =>
                {
                    b.Property<int>("NumOrdenFabricacion")
                        .HasColumnType("int");

                    b.Property<string>("CodigoArticulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NumOrdenFabricacion");

                    b.ToTable("Ordenes");
                });

            modelBuilder.Entity("JB_Formulacion.Models.Transferencia", b =>
                {
                    b.Property<int>("DocNumOf")
                        .HasColumnType("int");

                    b.Property<string>("CodBodegaDesde")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodBodegaHasta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocNumOf");

                    b.ToTable("Transferencias");
                });

            modelBuilder.Entity("JB_Formulacion.Models.CantidadPorLote", b =>
                {
                    b.HasOne("JB_Formulacion.Models.MateriaPrima", "MateriaPrima")
                        .WithMany("CantidadesPorLote")
                        .HasForeignKey("MateriaPrimaCodigo");

                    b.HasOne("JB_Formulacion.Models.OrdenFabricacion", "OrdenFabricacion")
                        .WithMany("CantidadesPorLote")
                        .HasForeignKey("OrdenFabricacionNumOrdenFabricacion");

                    b.HasOne("JB_Formulacion.Models.Transferencia", "Transferencia")
                        .WithMany("CantidadesPorLote")
                        .HasForeignKey("TransferenciaDocNumOf");

                    b.Navigation("MateriaPrima");

                    b.Navigation("OrdenFabricacion");

                    b.Navigation("Transferencia");
                });

            modelBuilder.Entity("JB_Formulacion.Models.MateriaPrima", b =>
                {
                    b.Navigation("CantidadesPorLote");
                });

            modelBuilder.Entity("JB_Formulacion.Models.OrdenFabricacion", b =>
                {
                    b.Navigation("CantidadesPorLote");
                });

            modelBuilder.Entity("JB_Formulacion.Models.Transferencia", b =>
                {
                    b.Navigation("CantidadesPorLote");
                });
#pragma warning restore 612, 618
        }
    }
}
