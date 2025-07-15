using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
    public class MaterialPresupuesto
    {
        [Key]
        public Guid Id { get; set; }
		public int PresupuestoId { get; set; } // CORRECTO
		public Presupuestos? Presupuesto { get; set; }
		public int MaterialId { get; set; } 
		public Material? Material { get; set; }
        public string Codigo { get; set; } // C�digo del �tem
        public string Descripcion { get; set; } // Descripci�n del �tem
        public string Unidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string Rubro { get; set; } // Cap�tulo / Rubro
        public string Tama�o { get; set; }
        public string Observaciones { get; set; }

    }
}