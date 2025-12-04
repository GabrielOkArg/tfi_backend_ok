using Microsoft.EntityFrameworkCore;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Infrastructure.Data
{
    public class GestionReclamosDbContext : DbContext
    {
        public GestionReclamosDbContext(DbContextOptions<GestionReclamosDbContext> options)
           : base(options)
        {
        }


        public DbSet<User> Usuarios { get; set; }
        public DbSet<Reclamo> Reclamos { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<ReclamoImagen> ReclamoImagenes { get; set; }
        public DbSet<ReclamoPresupuesto> ReclamoPresupuesto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reclamo>()
              .HasQueryFilter(r => !r.isDelete);
            // Relación Usuario - Reclamo
            modelBuilder.Entity<Reclamo>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Area>()
       .HasOne(a => a.ParentArea)
       .WithMany(a => a.SubAreas)
       .HasForeignKey(a => a.ParentAreaId)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Area>()
                .HasQueryFilter(a => !a.isDelete);

            modelBuilder.Entity<ReclamoImagen>()
                    .HasOne(i => i.Reclamo)
                    .WithMany(r => r.Imagenes)
                    .HasForeignKey(i => i.ReclamoId);
            modelBuilder.Entity<ReclamoPresupuesto>()
                                .HasOne(rp => rp.Reclamo)
                .WithOne(r => r.Presupuesto)
    .HasForeignKey<ReclamoPresupuesto>(rp => rp.ReclamoId);

        }

    }
}
