using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RecepciónPesosJamesBrown.Models.DAO.Configurations
{
    public class LoteDAOConfig : IEntityTypeConfiguration<LoteDAO>
    {
        public void Configure(EntityTypeBuilder<LoteDAO> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Cantidad).HasColumnType("decimal(18,6)");
            builder.HasOne(l => l.Linea)
                .WithMany(l => l.Lotes)
                .HasForeignKey(l => new { l.NumOrdenFabricacion, l.CodArticulo })
                .IsRequired();
        }
    }
}
