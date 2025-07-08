using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class Material
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal CostoUnitario { get; set; }
        public int EmpresaId { get; set; }
		public Empresa Empresa { get; set; }
        public Guid CategoriaMaterialId { get; set; }
		public CategoriaMaterial CategoriaMaterial { get; set; }
    }
}