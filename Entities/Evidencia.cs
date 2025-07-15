namespace SafyroPresupuestos.Entities
{
    public class Evidencia
    {
        public Guid Id { get; set; } = Guid.NewGuid();  // Clave primaria Guid
        public int ProyectoId { get; set; }             // Asociado al proyecto
        public Proyecto Proyecto { get; set; }
        public string FileUrl { get; set; }
        public string FileName { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
        public string UsuarioId { get; set; }
    }


}
