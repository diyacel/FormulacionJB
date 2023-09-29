using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models.Configurations
{
    public class MateriaPrimaConfig : IEntityTypeConfiguration<MateriaPrima>
    {
        public void Configure(EntityTypeBuilder<MateriaPrima> builder)
        {
            builder.HasKey(prop => prop.Codigo);
            builder.Property(prop => prop.Codigo).ValueGeneratedNever();
        }
    }
}
