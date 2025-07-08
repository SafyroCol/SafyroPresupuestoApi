
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services
{
    public class EquipoPresupuestoService : IEquipoPresupuestoService
    {
        private readonly List<EquipoPresupuesto> _items = new();

        public Task<IEnumerable<EquipoPresupuesto>> GetAllAsync() => Task.FromResult<IEnumerable<EquipoPresupuesto>>(_items);

        public Task<EquipoPresupuesto> GetByIdAsync(Guid id) => Task.FromResult(_items.Find(x => x.Id == id));

        public Task CreateAsync(EquipoPresupuesto item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(EquipoPresupuesto item)
        {
            var existing = _items.Find(x => x.Id == item.Id);
            if (existing != null)
            {
                _items.Remove(existing);
                _items.Add(item);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var item = _items.Find(x => x.Id == id);
            if (item != null)
                _items.Remove(item);
            return Task.CompletedTask;
        }
    }
}
