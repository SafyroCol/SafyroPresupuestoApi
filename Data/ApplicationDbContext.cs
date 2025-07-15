using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Models;

namespace SafyroPresupuestos.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Presupuestos> Presupuestos { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<ManoObra> ManosObra { get; set; }
        public DbSet<EquipoDepreciacion> EquiposDepreciacion { get; set; }
        public DbSet<CategoriaMaterial> CategoriasMaterial { get; set; }
        public DbSet<TipoManoObra> TiposManoObra { get; set; }
        public DbSet<TipoEquipo> TiposEquipo { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<ResumenFinanciero> ResumenesFinancieros { get; set; }
        public DbSet<BitacoraPresupuesto> BitacorasPresupuesto { get; set; }
        public DbSet<PermisoUsuario> PermisosUsuario { get; set; }
		public DbSet<CapituloPresupuesto> CapituloPresupuesto { get; set; }
        public DbSet<EquipoPresupuesto> EquipoPresupuesto { get; set; }
        public DbSet<ManoObraPresupuesto> ManoObraPresupuesto { get; set; }
		public DbSet<Partida> Partida { get; set; }
		public DbSet<PartidaPresupuesto> PartidaPresupuesto { get; set; }
		public DbSet<MaterialPresupuesto> MaterialPresupuesto { get; set; }
        public DbSet<MaterialRealPresupuesto> MaterialRealPresupuesto { get; set; }
        public DbSet<EquipoRealPresupuesto> EquipoRealPresupuesto { get; set; }
        public DbSet<ItemPresupuesto> ItemPresupuesto { get; set; }
        public DbSet<CostoIndirecto> CostoIndirecto { get; set; }
        public DbSet<CostoIndirectoPresupuesto> CostoIndirectoPresupuesto { get; set; }
        public DbSet<ServicioTercero> ServicioTercero { get; set; }
        public DbSet<ServicioTerceroPresupuesto> ServicioTerceroPresupuesto { get; set; }
        public DbSet<Evidencia> Evidencias { get; set; }

        public DbSet<EvidenciaItemPresupuesto> EvidenciasItemPresupuesto { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<EquipoPresupuesto>()
				.HasOne(ep => ep.Presupuesto)
				.WithMany()
				.HasForeignKey(ep => ep.PresupuestoId)
				.OnDelete(DeleteBehavior.Restrict); // ← evita cascada

			modelBuilder.Entity<EquipoPresupuesto>()
				.HasOne(ep => ep.EquipoDepreciacion)
				.WithMany()
				.HasForeignKey(ep => ep.EquipoDepreciacionId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ManoObraPresupuesto>()
				.HasOne(ep => ep.Presupuesto)
				.WithMany()
				.HasForeignKey(ep => ep.PresupuestoId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ManoObraPresupuesto>()
				.HasOne(ep => ep.ManoObra)
				.WithMany()
				.HasForeignKey(ep => ep.ManoObraId)
				.OnDelete(DeleteBehavior.Restrict); /// ← también evita cascada

			modelBuilder.Entity<MaterialPresupuesto>()
				.HasOne(ep => ep.Presupuesto)
				.WithMany()
				.HasForeignKey(ep => ep.PresupuestoId)
				.OnDelete(DeleteBehavior.Restrict); /// ← también evita cascada
			modelBuilder.Entity<MaterialPresupuesto>()
				.HasOne(ep => ep.Material)
				.WithMany()
				.HasForeignKey(ep => ep.MaterialId)
				.OnDelete(DeleteBehavior.Restrict); /// ← también evita cascada

			modelBuilder.Entity<PartidaPresupuesto>()
				.HasOne(ep => ep.Presupuesto)
				.WithMany()
				.HasForeignKey(ep => ep.PresupuestoId)
				.OnDelete(DeleteBehavior.Restrict); /// ← también evita cascada
			modelBuilder.Entity<PartidaPresupuesto>()
				.HasOne(ep => ep.Partida)
				.WithMany()
				.HasForeignKey(ep => ep.PartidaId)
				.OnDelete(DeleteBehavior.Restrict); /// ← también evita cascada
            // En tu DbContext, dentro de OnModelCreating
            modelBuilder.Entity<CostoIndirectoPresupuesto>()
                .HasOne(c => c.Presupuesto)
                .WithMany() // o .WithMany(p => p.CostosIndirectosPresupuesto) si tienes la colección
                .HasForeignKey(c => c.PresupuestoId)
                .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.NoAction

            modelBuilder.Entity<CostoIndirectoPresupuesto>()
                .HasOne(c => c.CostoIndirecto)
                .WithMany()
                .HasForeignKey(c => c.CostoIndirectoId)
                .OnDelete(DeleteBehavior.Cascade); // Solo una puede ser Cascade
                                                   // En tu DbContext, dentro de OnModelCreating
            modelBuilder.Entity<ServicioTerceroPresupuesto>()
                .HasOne(c => c.Presupuesto)
                .WithMany() // o .WithMany(p => p.ServicioTerceroPresupuesto) si tienes la colección
                .HasForeignKey(c => c.PresupuestoId)
                .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.NoAction

            modelBuilder.Entity<ServicioTerceroPresupuesto>()
                .HasOne(c => c.ServicioTercero)
                .WithMany()
                .HasForeignKey(c => c.ServicioTerceroId)
                .OnDelete(DeleteBehavior.Cascade); // Solo una puede ser Cascade
        }
	}
}
