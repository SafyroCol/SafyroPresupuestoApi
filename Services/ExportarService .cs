using SafyroPresupuestos.Services.Interfaces;
using System.Text;

namespace SafyroPresupuestos.Services
{
    public class ExportarService : IExportarService
    {
        public async Task<byte[]> ExportarResumenFinanciero(Guid proyectoId)
        {
            try
            {
                // Simulación de archivo PDF
                return Encoding.UTF8.GetBytes("Reporte exportado");
            }
            catch
            {
                throw new ApplicationException("No se pudo exportar el reporte. Intente nuevamente.");
            }
        }
    }

}
