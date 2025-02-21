using Microsoft.EntityFrameworkCore;
using UnphuVacantesAPI.Models;

namespace UnphuVacantesAPI.Data
{
    public class UnphuVacantesDbContext : DbContext
    {
        public UnphuVacantesDbContext(DbContextOptions<UnphuVacantesDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Vacante> Vacantes { get; set; }
        public DbSet<Postulante> Postulantes { get; set; }
        public DbSet<Postulacion> Postulaciones { get; set; }
        public DbSet<Favorito> Favoritos { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
    }
}
