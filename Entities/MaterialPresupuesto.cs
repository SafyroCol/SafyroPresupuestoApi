using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
    public class MaterialPresupuesto
    {
        [Key]
        public Guid Id { get; set; }
		public int PresupuestoId { get; set; }
		public Presupuestos Presupuesto { get; set; }
		public int MaterialId { get; set; }
		public Material Material { get; set; }
        public string UnidadDeMedida { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CostoUnitario { get; set; }


    }
}