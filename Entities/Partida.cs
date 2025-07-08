using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SafyroPresupuestos.Entities
{
	public class Partida
	{
		public Guid Id { get; set; }
		public string Codigo { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public string Unidad { get; set; } = string.Empty;
		public decimal Cantidad { get; set; }
		public decimal PrecioUnitario { get; set; }
		public decimal PrecioTotal => Cantidad * PrecioUnitario;
		public Guid? PartidaPadreId { get; set; }
		public Partida? PartidaPadre { get; set; }
		public int Orden { get; set; }
	}
}
