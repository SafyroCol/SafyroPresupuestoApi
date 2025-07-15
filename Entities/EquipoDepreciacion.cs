using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.Entities
{
    public class EquipoDepreciacion
    {
        [Key]
        public int Id { get; set; }
        public string NombreEquipo { get; set; }
        public decimal ValorInicial { get; set; }
        public decimal ValorResidual { get; set; }
        public int VidaUtilMeses { get; set; }
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public decimal CostoMensualDepreciacion =>
            (ValorInicial - ValorResidual) / VidaUtilMeses;
    }
}