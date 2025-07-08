using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IDocumentoService
    {
        Task<bool> SubirDocumentoAsync(DocumentoDto dto);
    }

}
