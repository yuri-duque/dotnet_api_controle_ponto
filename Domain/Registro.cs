using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Registro
    {
        public long Id { get; set; }
        public DateTime Horario { get; set; }

        [Required]
        public long IdDia { get; set; }
        public Dia Dia { get; set; }

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Registro>();
            map.HasKey(x => x.Id);
            map.Property(x => x.Id).ValueGeneratedOnAdd();

            map.HasOne(x => x.Dia).WithMany(x => x.Registros).HasForeignKey(x => x.IdDia).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
