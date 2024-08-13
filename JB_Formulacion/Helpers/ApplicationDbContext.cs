using Microsoft.EntityFrameworkCore;
using RecepciónPesosJamesBrown.Models.DAO;
using System.Configuration;
using System.Reflection;
using Microsoft.Data.SqlClient;

namespace RecepciónPesosJamesBrown.Helpers
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["PrecitrolConnection"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<TransferenciaDAO> Transferencias { get; set; }
        public DbSet<LineaDAO> Lineas { get; set; }
        public DbSet<LoteDAO> Lotes { get; set; }
    }
}
