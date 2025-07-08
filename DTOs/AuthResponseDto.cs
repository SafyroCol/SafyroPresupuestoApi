namespace SafyroPresupuestos.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public Guid EmpresaId { get; set; }
        public string NombreUsuario { get; set; }
        public string Rol { get; set; }
    }
}
