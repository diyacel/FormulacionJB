using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB_Formulacion.Models.Configurations
{
    public class OrdenFabricacionConfig : IEntityTypeConfiguration<OrdenFabricacion>
    {
        public void Configure(EntityTypeBuilder<OrdenFabricacion> builder)
        {
            builder.HasKey(prop => prop.NumOrdenFabricacion);
            builder.Property(prop => prop.NumOrdenFabricacion).ValueGeneratedNever();
        }
    }
}
