namespace SafyroPresupuestos.Entities
{
    public class ResumenFinanciero
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
		public Presupuestos Presupuesto { get; set; }

		public decimal CostoTotalMateriales { get; set; }
        public decimal CostoTotalManoObra { get; set; }
        public decimal CostoTotalEquipos { get; set; }

        public decimal CostoTotal => CostoTotalMateriales + CostoTotalManoObra + CostoTotalEquipos;

        public decimal IngresosEstimados { get; set; }

        public decimal Utilidad => IngresosEstimados - CostoTotal;

    }
}
