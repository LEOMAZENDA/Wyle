﻿// <auto-generated />
using System;
using AcessoDados.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AcessoDados.Migrations
{
    [DbContext(typeof(SystemAppContext))]
    partial class SystemAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.EStudenteModel", b =>
                {
                    b.Property<int>("IdEstudant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Morada")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefone")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.HasKey("IdEstudant");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Models.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)")
                        .HasDefaultValue("1");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Models.Entidades.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasDefaultValue("Sem E-Mail");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)")
                        .HasDefaultValue("1");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TelefoneNº")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasDefaultValue("Sem Telefone");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Models.Entidades.Funcao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataRegisto")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)")
                        .HasDefaultValue("1");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Funcoes");
                });

            modelBuilder.Entity("Models.Entidades.Funcionario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)")
                        .HasDefaultValue("1");

                    b.Property<int>("IdFuncao")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)")
                        .HasDefaultValue("1234");

                    b.HasKey("Id");

                    b.HasIndex("IdFuncao");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("Models.Entidades.ItemVenda", b =>
                {
                    b.Property<int>("IdVenda")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorUnitario")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdVenda", "IdProducto");

                    b.HasIndex("IdProducto");

                    b.ToTable("ItemVenda");
                });

            modelBuilder.Entity("Models.Entidades.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataRegisto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)")
                        .HasDefaultValue("1");

                    b.Property<int>("Estoque")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Obs")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasDefaultValue("Sem observações");

                    b.Property<decimal>("Preco")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.HasKey("Id");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("Models.Entidades.Venda", b =>
                {
                    b.Property<int>("IdVenda")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataEntrega")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(1)")
                        .HasDefaultValue("1");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<string>("Operador")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("ValorTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdVenda");

                    b.HasIndex("IdCliente");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("Models.Entidades.Cliente", b =>
                {
                    b.OwnsMany("Models.Entidades.Endereco", "Enderecos", b1 =>
                        {
                            b1.Property<int>("Id")
                                .HasColumnType("int");

                            b1.Property<int>("IdEndereco")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("CasaNº")
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Municipio")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Provincia")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Referencia")
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Rua")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<bool>("Selecionado")
                                .HasColumnType("bit");

                            b1.HasKey("Id", "IdEndereco");

                            b1.ToTable("Endereco");

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.Navigation("Enderecos");
                });

            modelBuilder.Entity("Models.Entidades.Funcionario", b =>
                {
                    b.HasOne("Models.Entidades.Funcao", "Funcao")
                        .WithMany("Funcionarios")
                        .HasForeignKey("IdFuncao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcao");
                });

            modelBuilder.Entity("Models.Entidades.ItemVenda", b =>
                {
                    b.HasOne("Models.Entidades.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("IdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entidades.Venda", "Venda")
                        .WithMany("ItensVenda")
                        .HasForeignKey("IdVenda")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venda");
                });

            modelBuilder.Entity("Models.Entidades.Producto", b =>
                {
                    b.HasOne("Models.Entidades.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Models.Entidades.Venda", b =>
                {
                    b.HasOne("Models.Entidades.Cliente", "Cliente")
                        .WithMany("Vendas")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Models.Entidades.Endereco", "EnderecoEntrega", b1 =>
                        {
                            b1.Property<int>("VendaIdVenda")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("CasaNº")
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Municipio")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Provincia")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Referencia")
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.Property<string>("Rua")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("varchar(50)");

                            b1.HasKey("VendaIdVenda");

                            b1.ToTable("Venda");

                            b1.WithOwner()
                                .HasForeignKey("VendaIdVenda");
                        });

                    b.Navigation("Cliente");

                    b.Navigation("EnderecoEntrega");
                });

            modelBuilder.Entity("Models.Entidades.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("Models.Entidades.Cliente", b =>
                {
                    b.Navigation("Vendas");
                });

            modelBuilder.Entity("Models.Entidades.Funcao", b =>
                {
                    b.Navigation("Funcionarios");
                });

            modelBuilder.Entity("Models.Entidades.Venda", b =>
                {
                    b.Navigation("ItensVenda");
                });
#pragma warning restore 612, 618
        }
    }
}
