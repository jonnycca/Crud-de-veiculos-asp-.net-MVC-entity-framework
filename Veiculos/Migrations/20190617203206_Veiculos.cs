using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Veiculos.Migrations
{
    public partial class Veiculos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeVeiculo = table.Column<string>(maxLength: 30, nullable: false),
                    NomeFabricante = table.Column<string>(maxLength: 20, nullable: false),
                    AnoFabricacao = table.Column<int>(nullable: false),
                    AnoModelo = table.Column<int>(nullable: false),
                    Motor = table.Column<string>(nullable: false),
                    Cor = table.Column<int>(nullable: false),
                    DataLancamento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
