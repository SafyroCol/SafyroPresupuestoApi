using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class CertificacionDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Debe indicar la fecha de la certificación.")]
        public DateTime Fecha { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100.")]
        public decimal PorcentajeAvance { get; set; }
    }

}
