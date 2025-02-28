using Microsoft.EntityFrameworkCore;

namespace P01_2023EQ651_2022CI650.Models
{
    public class ParqueoDBContext : DbContext
    {
        public ParqueoDBContext(DbContextOptions<ParqueoDBContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<EspacioParqueo> EspaciosParqueo { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
