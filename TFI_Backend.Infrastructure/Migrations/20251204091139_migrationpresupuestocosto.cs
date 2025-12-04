using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFI_Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migrationpresupuestocosto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "Reclamos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "ReclamoPresupuesto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ruta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReclamoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReclamoPresupuesto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReclamoPresupuesto_Reclamos_ReclamoId",
                        column: x => x.ReclamoId,
                        principalTable: "Reclamos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReclamoPresupuesto_ReclamoId",
                table: "ReclamoPresupuesto",
                column: "ReclamoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReclamoPresupuesto");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "Reclamos");
        }
    }
}
