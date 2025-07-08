namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IExportarService
    {
        Task<byte[]> ExportarResumenFinanciero(Guid proyectoId);
    }

}
