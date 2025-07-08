using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{

    [Table("EquipoRealPresupuesto")]
    public class EquipoRealPresupuesto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int PresupuestoId { get; set; }

        [Required]
        public Guid EquipoPresupuestoId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostoReal { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relaciones de navegación (opcional)
        [ForeignKey(nameof(PresupuestoId))]
        public virtual Presupuestos Presupuesto { get; set; }

        [ForeignKey(nameof(EquipoPresupuestoId))]
        public virtual EquipoPresupuesto EquipoPresupuesto { get; set; }
    }


}
