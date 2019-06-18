﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Veiculos.Models;

namespace Veiculos.Migrations
{
    [DbContext(typeof(VeiculosContext))]
    [Migration("20190617203206_Veiculos")]
    partial class Veiculos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Veiculos.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnoFabricacao");

                    b.Property<int>("AnoModelo");

                    b.Property<int>("Cor");

                    b.Property<DateTime>("DataLancamento");

                    b.Property<string>("Motor")
                        .IsRequired();

                    b.Property<string>("NomeFabricante")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("NomeVeiculo")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Veiculo");
                });
#pragma warning restore 612, 618
        }
    }
}
