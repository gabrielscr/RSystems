﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RSystem.Infrastructure;

namespace RSystem.Migrations.Admin
{
    [DbContext(typeof(AdminContext))]
    partial class AdminContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RSystem.Domain.Cidade", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("EstadoId");

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Cidade");
                });

            modelBuilder.Entity("RSystem.Domain.Estado", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Nome");

                    b.Property<int>("PaisId");

                    b.Property<string>("Uf");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Estado");
                });

            modelBuilder.Entity("RSystem.Domain.Estoque", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("Estoque");
                });

            modelBuilder.Entity("RSystem.Domain.EstoqueCidade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CidadeId");

                    b.Property<int>("EstoqueId");

                    b.HasKey("Id");

                    b.HasIndex("CidadeId");

                    b.HasIndex("EstoqueId");

                    b.ToTable("EstoqueCidade");
                });

            modelBuilder.Entity("RSystem.Domain.Marca", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Descricao");

                    b.Property<int>("EstoqueId");

                    b.HasKey("Id");

                    b.HasIndex("EstoqueId");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("RSystem.Domain.Pais", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Nome");

                    b.Property<string>("Sigla");

                    b.HasKey("Id");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("RSystem.Domain.Produto", b =>
                {
                    b.Property<int>("Id");

                    b.Property<bool>("Ativo");

                    b.Property<string>("Descricao");

                    b.Property<string>("DescricaoDetalhada");

                    b.Property<byte[]>("Imagem");

                    b.Property<int>("MarcaId");

                    b.Property<int>("Quantidade");

                    b.Property<int>("UnidadeMedida");

                    b.Property<double>("Valor");

                    b.Property<double>("Volume");

                    b.HasKey("Id");

                    b.HasIndex("MarcaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("RSystem.Domain.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Nome");

                    b.Property<string>("Senha");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("RSystem.Domain.Cidade", b =>
                {
                    b.HasOne("RSystem.Domain.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RSystem.Domain.Estado", b =>
                {
                    b.HasOne("RSystem.Domain.Pais", "Pais")
                        .WithMany()
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RSystem.Domain.EstoqueCidade", b =>
                {
                    b.HasOne("RSystem.Domain.Cidade", "Cidade")
                        .WithMany()
                        .HasForeignKey("CidadeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RSystem.Domain.Estoque", "Estoque")
                        .WithMany("EstoqueCidades")
                        .HasForeignKey("EstoqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RSystem.Domain.Marca", b =>
                {
                    b.HasOne("RSystem.Domain.Estoque", "Estoque")
                        .WithMany()
                        .HasForeignKey("EstoqueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RSystem.Domain.Produto", b =>
                {
                    b.HasOne("RSystem.Domain.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
