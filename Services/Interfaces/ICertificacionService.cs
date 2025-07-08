using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface ICertificacionService
    {
        Task<List<CertificacionDto>> ObtenerPorProyectoAsync(Guid proyectoId);
    }

}
