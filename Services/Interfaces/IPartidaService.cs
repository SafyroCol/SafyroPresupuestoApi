using SafyroPresupuestos.Entities;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IPartidaService
    {
        Task<IEnumerable<Partida>> GetAllAsync();
        Task<Partida> GetByIdAsync(Guid id);
        Task<Partida> CreateAsync(Partida entidad);
        Task<Partida> UpdateAsync(Partida entidad);
        Task DeleteAsync(Guid id);
    }
}