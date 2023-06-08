﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SullivanBurger.Data;

#nullable disable

namespace SullivanBurger.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230608183347_Cambios")]
    partial class Cambios
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SullivanBurger.Models.Distribuidor", b =>
                {
                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Nombre");

                    b.ToTable("Distribuidores");
                });

            modelBuilder.Entity("SullivanBurger.Models.Extra", b =>
                {
                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nombre");

                    b.ToTable("Extras");
                });

            modelBuilder.Entity("SullivanBurger.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("SullivanBurger.Models.Producto", b =>
                {
                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("DistribuidorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Imagen")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Nombre");

                    b.HasIndex("DistribuidorId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("SullivanBurger.Models.ProductoPedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdPedido")
                        .HasColumnType("int");

                    b.Property<string>("NombreProducto")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("Id", "IdPedido", "NombreProducto");

                    b.HasIndex("IdPedido");

                    b.HasIndex("NombreProducto");

                    b.ToTable("ProductosPedidos");
                });

            modelBuilder.Entity("SullivanBurger.Models.Usuario", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Puntos")
                        .HasColumnType("int");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("Email");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("SullivanBurger.Models.Pedido", b =>
                {
                    b.HasOne("SullivanBurger.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("Email");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("SullivanBurger.Models.Producto", b =>
                {
                    b.HasOne("SullivanBurger.Models.Distribuidor", "Distribuidor")
                        .WithMany("Productos")
                        .HasForeignKey("DistribuidorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Distribuidor");
                });

            modelBuilder.Entity("SullivanBurger.Models.ProductoPedido", b =>
                {
                    b.HasOne("SullivanBurger.Models.Pedido", "Pedido")
                        .WithMany("Productos")
                        .HasForeignKey("IdPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SullivanBurger.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("NombreProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("SullivanBurger.Models.Distribuidor", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("SullivanBurger.Models.Pedido", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
