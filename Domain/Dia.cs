using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Dia
    {
        public long Id { get; set; }
        public string DiaSemana { get; set; }
        public DateTime Data { get; set; }

        public IList<Registro> Registros { get; set; }

        public static void Map(ModelBuilder modelBuilder)
        {
            var map = modelBuilder.Entity<Dia>();
            map.HasKey(x => x.Id);
            map.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
