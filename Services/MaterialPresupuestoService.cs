
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services
{
    public class MaterialPresupuestoService : IMaterialPresupuestoService
    {
        private readonly List<MaterialPresupuesto> _items = new();

        public Task<IEnumerable<MaterialPresupuesto>> GetAllAsync() => Task.FromResult<IEnumerable<MaterialPresupuesto>>(_items);

        public Task<MaterialPresupuesto> GetByIdAsync(Guid id) => Task.FromResult(_items.Find(x => x.Id == id));

        public Task CreateAsync(MaterialPresupuesto item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(MaterialPresupuesto item)
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
