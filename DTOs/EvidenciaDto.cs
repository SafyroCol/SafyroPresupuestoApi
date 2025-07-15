namespace SafyroPresupuestos.DTOs
{
    public class EvidenciaDto
    {
        public Guid Id { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaSubida { get; set; }
        public string UsuarioId { get; set; }
    }

}
