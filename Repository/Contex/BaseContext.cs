using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository.Contex
{
    public class BaseContext : DbContext
    {
        public static string _connectionString = @"Server=localhost;Port=5432;Database=controle_ponto;User Id=postgres;Password=root;";

        public BaseContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Dia.Map(modelBuilder);
            Registro.Map(modelBuilder);
        }

        public BaseContext(DbContextOptions<BaseContext> option)
            : base(option)
        { }

        public DbSet<Dia> Dias { get; set; }
        public DbSet<Registro> Registros { get; set; }
    }
}
