namespace SafyroPresupuestos.DTOs
{
    public class CargarEvidenciaDto
    {
        public Guid ItemId { get; set; }
        public decimal Costo { get; set; }
        public required IFormFile Archivo { get; set; }
    }

}
