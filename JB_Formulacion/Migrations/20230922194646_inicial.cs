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
                name: "Ordenes",
                columns: table => new
                {
                    IdOf = table.Column<int>(type: "int", nullable: false),
                    NumOrdenFabricacion = table.Column<int>(type: "int", nullable: false),
                    CodArticulo = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodegaDesde = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BodegaHasta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.IdOf);
                });

            migrationBuilder.CreateTable(
                name: "Componentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnidadMedida = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CantidadTotal = table.Column<double>(type: "float", nullable: false),
                    RequiereRepesaje = table.Column<bool>(type: "bit", nullable: false),
                    CantidadPesada = table.Column<double>(type: "float", nullable: false),
                    CodigoArticulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsPesado = table.Column<bool>(type: "bit", nullable: false),
                    OrdenComponentesIdOf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Componentes_Ordenes_OrdenComponentesIdOf",
                        column: x => x.OrdenComponentesIdOf,
                        principalTable: "Ordenes",
                        principalColumn: "IdOf");
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
                    ComponenteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Componentes_ComponenteId",
                        column: x => x.ComponenteId,
                        principalTable: "Componentes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Componentes_OrdenComponentesIdOf",
                table: "Componentes",
                column: "OrdenComponentesIdOf");

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_ComponenteId",
                table: "Lotes",
                column: "ComponenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "Componentes");

            migrationBuilder.DropTable(
                name: "Ordenes");
        }
    }
}
