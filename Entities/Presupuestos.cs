using System;
using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class Presupuestos
    {
        [Key]
        public int Id { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }

        // Presupuestado
        public decimal SubtotalMateriales { get; set; }
        public decimal SubtotalManoObra { get; set; }
        public decimal SubtotalEquipos { get; set; }
        public decimal SubtotalOtros { get; set; }
        public decimal Indirectos { get; set; }
        public decimal Utilidad { get; set; }
        public decimal Iva { get; set; }
        public decimal CostoTotal { get; set; }

        public int MonedaId { get; set; }
        public Moneda Moneda { get; set; }

        // NUEVO: Costos Reales por rubro
        public decimal? CostoRealMateriales { get; set; }
        public decimal? CostoRealManoObra { get; set; }
        public decimal? CostoRealEquipos { get; set; }
        public decimal? CostoRealServiciosTerceros { get; set; }
        public decimal? CostoRealCostosIndirectos { get; set; }
    }
}
