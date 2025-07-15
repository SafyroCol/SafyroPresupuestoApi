using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class ManoObra
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public decimal CostoHora { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public int TipoManoObraId { get; set; }
        public TipoManoObra TipoManoObra { get; set; }
    }

}