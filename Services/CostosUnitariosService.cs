using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Services
{
    public class CostosUnitariosService : ICostosUnitariosService
    {
        public async Task<List<CostosUnitariosDto>> ObtenerTodosAsync(Guid empresaId)
        {
            try
            {
                // Simulación de acceso a datos
                return new List<CostosUnitariosDto>();
            }
            catch (Exception)
            {
                throw new ApplicationException("No fue posible obtener los costos unitarios. Intente nuevamente.");
            }
        }
    }

}
