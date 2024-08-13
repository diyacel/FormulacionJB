using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecepciónPesosJamesBrown.Models.DAO.Configurations
{
    public class LineaDAOConfig : IEntityTypeConfiguration<LineaDAO>
    {
        public void Configure(EntityTypeBuilder<LineaDAO> builder)
        {
            //builder.HasKey(prop => prop.Id);
            builder.HasKey(e => new { e.NumOrdenFabricacion, e.CodArticulo });
            builder.HasOne(l => l.Transferencia)
                .WithMany(t => t.lineas)
                .HasForeignKey(l => l.NumOrdenFabricacion)
                .IsRequired();

            //builder
            //.HasMany(c => c.Lotes)
            //.WithOne(p => p.Linea)
            //.HasForeignKey(p => p.lineaId)
            //.HasConstraintName("lineaId");
        }
    }
}
