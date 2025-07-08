using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IReporteGerencialService
    {
        Task<ReporteGerencialDto> ObtenerResumenAsync(Guid proyectoId);
    }

}
