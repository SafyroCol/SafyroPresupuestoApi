using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
    public class ManoObraPresupuesto
    {
        [Key]
        public Guid Id { get; set; }
        public int PresupuestoId { get; set; }
        public Presupuestos? Presupuesto { get; set; }
        public int ManoObraId { get; set; }
        public ManoObra? ManoObra { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string Rubro { get; set; }
        public string Observaciones { get; set; }
    }
}