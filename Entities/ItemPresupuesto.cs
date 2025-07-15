using System;
using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class ItemPresupuesto
    {
        [Key]
        public Guid Id { get; set; }
        public int PresupuestoId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string Rubro { get; set; } // Capítulo / Rubro
        public string Tamaño { get; set; }
        public string Observaciones { get; set; }
        public decimal? CostoReal { get; set; } // Puedes poner default 0
        public ICollection<EvidenciaItemPresupuesto> EvidenciasItemPresupuesto { get; set; }

    }
}
