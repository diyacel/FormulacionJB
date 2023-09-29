using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JB_Formulacion.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    NumOrdenFabricacion = table.Column<int>(type: "int", nullable: false),
                    CodigoArticulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.NumOrdenFabricacion);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    DocNumOf = table.Column<int>(type: "int", nullable: false),
                    CodBodegaDesde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodBodegaHasta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.DocNumOf);
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    CantidadPesada = table.Column<double>(type: "float", nullable: false),
                    CantidadTotal = table.Column<double>(type: "float", nullable: false),
                    OrdenFabricacionNumOrdenFabricacion = table.Column<int>(type: "int", nullable: true),
                    MateriaPrimaCodigo = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TransferenciaDocNumOf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Materias_MateriaPrimaCodigo",
                        column: x => x.MateriaPrimaCodigo,
                        principalTable: "Materias",
                        principalColumn: "Codigo");
                    table.ForeignKey(
                        name: "FK_Lotes_Ordenes_OrdenFabricacionNumOrdenFabricacion",
                        column: x => x.OrdenFabricacionNumOrdenFabricacion,
                        principalTable: "Ordenes",
                        principalColumn: "NumOrdenFabricacion");
                    table.ForeignKey(
                        name: "FK_Lotes_Transferencias_TransferenciaDocNumOf",
                        column: x => x.TransferenciaDocNumOf,
                        principalTable: "Transferencias",
                        principalColumn: "DocNumOf");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_MateriaPrimaCodigo",
                table: "Lotes",
                column: "MateriaPrimaCodigo");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_OrdenFabricacionNumOrdenFabricacion",
                table: "Lotes",
                column: "OrdenFabricacionNumOrdenFabricacion");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_TransferenciaDocNumOf",
                table: "Lotes",
                column: "TransferenciaDocNumOf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Transferencias");
        }
    }
}
