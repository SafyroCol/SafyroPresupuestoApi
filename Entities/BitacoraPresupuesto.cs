namespace SafyroPresupuestos.Entities
{
    public class BitacoraPresupuesto
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
		public Presupuestos Presupuesto { get; set; }
		public string Accion { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
    }
}
