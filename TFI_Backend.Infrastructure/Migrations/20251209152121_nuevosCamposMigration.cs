using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class nuevosCamposMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ComentarioTecnico",
                table: "Reclamos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Reclamos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComentarioTecnico",
                table: "Reclamos");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Reclamos");
        }
    }
}
