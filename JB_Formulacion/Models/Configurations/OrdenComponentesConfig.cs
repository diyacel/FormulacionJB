using JB_Formulacion.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjemploEntityForms2
{
    public class OrdenComponentesConfig : IEntityTypeConfiguration<OrdenComponentes>
    {
        public void Configure(EntityTypeBuilder<OrdenComponentes> builder)
        {
            builder.HasKey(prop => prop.IdOf);
            builder.Property(prop=>prop.IdOf).ValueGeneratedNever();
            
        }
    }
}
