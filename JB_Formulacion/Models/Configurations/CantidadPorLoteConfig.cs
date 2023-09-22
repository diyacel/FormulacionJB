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
    public class CantidadPorLoteConfig : IEntityTypeConfiguration<CantidadPorLote>
    {
        public void Configure(EntityTypeBuilder<CantidadPorLote> builder)
        {
            builder.HasKey(prop => prop.Id);
        }
    }
}
