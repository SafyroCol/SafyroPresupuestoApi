namespace SafyroPresupuestos.Entities
{
    public class ManoObra
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoHora { get; set; }
        public int HorasTrabajadas { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
		public int TipoManoObraId { get; set; }
		public TipoManoObra TipoManoObra { get; set; }

	}
}