using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
    public class EvidenciaItemPresupuesto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ItemPresupuestoId { get; set; }
        [ForeignKey("ItemPresupuestoId")]
        public ItemPresupuesto ItemPresupuesto { get; set; }

        [Required]
        public string NombreArchivo { get; set; }

        [Required]
        public string UrlArchivo { get; set; }

        public string TipoArchivo { get; set; }

        public decimal ValorSoportado { get; set; } // Monto de la factura/soporte si aplica

        public DateTime FechaCarga { get; set; } = DateTime.UtcNow;
    }
}
