using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Services
{
    public class ReporteGerencialService : IReporteGerencialService
    {
        public async Task<ReporteGerencialDto> ObtenerResumenAsync(Guid proyectoId)
        {
            try
            {
                return new ReporteGerencialDto
                {
                    ProyectoId = proyectoId,
                    AvanceFisico = 60,
                    AvanceFinanciero = 55
                };
            }
            catch
            {
                throw new ApplicationException("Ocurrió un error al obtener el resumen gerencial.");
            }
        }
    }

}
