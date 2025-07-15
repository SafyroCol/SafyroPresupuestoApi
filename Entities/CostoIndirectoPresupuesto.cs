using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class CostoIndirectoPresupuesto
    {
        public Guid Id { get; set; } // Corresponde a [Id] uniqueidentifier

        public int PresupuestoId { get; set; } // Corresponde a [PresupuestoId] int
        public int CostoIndirectoId { get; set; } // Corresponde a [CostoIndirectoId] int

        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? ValorTotal { get; set; }
        public string? Rubro { get; set; }
        public string? Observaciones { get; set; }

        // Relaciones de navegación (opcional, si usas EF Core)
        public Presupuestos? Presupuesto { get; set; }
        public CostoIndirecto? CostoIndirecto { get; set; }
    }

}
