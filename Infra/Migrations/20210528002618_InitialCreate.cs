using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<byte[]>(type: "varbinary(16)", nullable: false),
                    CountryName = table.Column<string>(type: "varchar(767)", nullable: false),
                    ActiveCases = table.Column<double>(type: "double", nullable: false),
                    LastUpdate = table.Column<string>(type: "text", nullable: false),
                    NewCases = table.Column<string>(type: "text", nullable: false),
                    NewDeaths = table.Column<string>(type: "text", nullable: false),
                    TotalCases = table.Column<string>(type: "text", nullable: false),
                    TotalDeaths = table.Column<string>(type: "text", nullable: false),
                    TotalRecovered = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CountryName",
                table: "Countries",
                column: "CountryName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
