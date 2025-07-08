using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;

namespace SafyroPresupuestos.Services
{
    public class PartidaService : IPartidaService
    {
        public Task<IEnumerable<Partida>> GetAllAsync() => throw new NotImplementedException();
        public Task<Partida> GetByIdAsync(Guid id) => throw new NotImplementedException();
        public Task<Partida> CreateAsync(Partida entidad) => throw new NotImplementedException();
        public Task<Partida> UpdateAsync(Partida entidad) => throw new NotImplementedException();
        public Task DeleteAsync(Guid id) => throw new NotImplementedException();
    }
}