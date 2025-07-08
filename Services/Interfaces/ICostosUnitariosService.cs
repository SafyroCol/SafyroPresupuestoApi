using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface ICostosUnitariosService
    {
        Task<List<CostosUnitariosDto>> ObtenerTodosAsync(Guid empresaId);
    }

}
