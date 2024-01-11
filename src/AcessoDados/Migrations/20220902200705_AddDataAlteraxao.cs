using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcessoDados.Migrations
{
    public partial class AddDataAlteraxao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                table: "Producto",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                table: "Funcoes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                table: "Funcionario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                table: "Cliente",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataUltimaAlteracao",
                table: "Categoria",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                table: "Producto");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                table: "Funcoes");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                table: "Funcionario");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "DataUltimaAlteracao",
                table: "Categoria");
        }
    }
}
