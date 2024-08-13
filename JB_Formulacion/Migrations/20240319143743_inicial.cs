using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecepciónPesosJamesBrown.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    NumOrdenFabricacion = table.Column<int>(type: "int", nullable: false),
                    DocNumOF = table.Column<int>(type: "int", nullable: false),
                    CodArticulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripción = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodBodegaDesde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodBodegaHasta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.NumOrdenFabricacion);
                });

            migrationBuilder.CreateTable(
                name: "Lineas",
                columns: table => new
                {
                    NumOrdenFabricacion = table.Column<int>(type: "int", nullable: false),
                    CodArticulo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineas", x => new { x.NumOrdenFabricacion, x.CodArticulo });
                    table.ForeignKey(
                        name: "FK_Lineas_Transferencias_NumOrdenFabricacion",
                        column: x => x.NumOrdenFabricacion,
                        principalTable: "Transferencias",
                        principalColumn: "NumOrdenFabricacion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOrdenFabricacion = table.Column<int>(type: "int", nullable: false),
                    CodArticulo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<decimal>(type: "decimal(18,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Lineas_NumOrdenFabricacion_CodArticulo",
                        columns: x => new { x.NumOrdenFabricacion, x.CodArticulo },
                        principalTable: "Lineas",
                        principalColumns: new[] { "NumOrdenFabricacion", "CodArticulo" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_NumOrdenFabricacion_CodArticulo",
                table: "Lotes",
                columns: new[] { "NumOrdenFabricacion", "CodArticulo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Lineas");

            migrationBuilder.DropTable(
                name: "Transferencias");
        }
    }
}
