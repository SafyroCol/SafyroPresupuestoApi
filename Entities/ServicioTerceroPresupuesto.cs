using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class ServicioTerceroPresupuesto
    {
        [Key]
        public Guid Id { get; set; }
        public int PresupuestoId { get; set; }
        public Presupuestos? Presupuesto { get; set; }
        public int ServicioTerceroId { get; set; }
        public ServicioTercero? ServicioTercero { get; set; }
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
