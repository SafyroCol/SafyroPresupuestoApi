using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Services
{
    public class DocumentoService : IDocumentoService
    {
        public async Task<bool> SubirDocumentoAsync(DocumentoDto dto)
        {
            try
            {
                // Simular lógica
                return true;
            }
            catch
            {
                throw new ApplicationException("No se pudo subir el documento. Verifique el archivo e intente nuevamente.");
            }
        }
    }

}
