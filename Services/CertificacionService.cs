using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Services
{
    public class CertificacionService : ICertificacionService
    {
        public async Task<List<CertificacionDto>> ObtenerPorProyectoAsync(Guid proyectoId)
        {
            try
            {
                return new List<CertificacionDto>();
            }
            catch
            {
                throw new ApplicationException("Error al consultar certificaciones del proyecto.");
            }
        }
    }

}
