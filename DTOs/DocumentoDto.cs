using System.ComponentModel.DataAnnotations;

namespace SafyroPresupuestos.DTOs
{
    public class DocumentoDto
    {
        [Required(ErrorMessage = "El nombre del documento es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe adjuntar el archivo del documento.")]
        public IFormFile Archivo { get; set; }
    }

}
