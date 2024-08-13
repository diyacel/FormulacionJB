using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecepciónPesosJamesBrown.Models.DAO.Configurations
{
    public class TransferenciaDAOConfig : IEntityTypeConfiguration<TransferenciaDAO>
    {
        public void Configure(EntityTypeBuilder<TransferenciaDAO> builder)
        {
            builder.HasKey(prop => prop.NumOrdenFabricacion);
            builder.Property(prop => prop.NumOrdenFabricacion).ValueGeneratedNever();
            //builder
            //.HasMany(c => c.lineas)
            //.WithOne(p => p.Transferencia)
            //.HasForeignKey(p => p.TransferenciaId)
            //.HasConstraintName("TransferenciaId");
        }
    }
}
