using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ResyRoom.Models
{
    public class ResyRoomEntities : DbContext
    {
        public ResyRoomEntities()
            : base("name=ResyRoomEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Comuna> Comunas { get; set; }
        public DbSet<EstadoReserva> EstadosReserva { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Banda> Bandas { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Estudio> Estudios { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Grabacion> Grabacion { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoEquipo> TipoEquipos { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Horario> Horarios { get; set; }
    }
}
