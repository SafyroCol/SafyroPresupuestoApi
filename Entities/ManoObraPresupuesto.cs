using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
    public class ManoObraPresupuesto
    {
		public int Id { get; set; }
		public int PresupuestoId { get; set; }
		public Presupuestos Presupuesto { get; set; }
		public int ManoObraId { get; set; }
		public ManoObra ManoObra { get; set; }
    }
}