
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services
{
    public class ManoObraPresupuestoService : IManoObraPresupuestoService
    {
        private readonly List<ManoObraPresupuesto> _items = new();

        public Task<IEnumerable<ManoObraPresupuesto>> GetAllAsync() => Task.FromResult<IEnumerable<ManoObraPresupuesto>>(_items);

        public Task<ManoObraPresupuesto> GetByIdAsync(int id) => Task.FromResult(_items.Find(x => x.Id == id));

        public Task CreateAsync(ManoObraPresupuesto item)
        {
            _items.Add(item);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ManoObraPresupuesto item)
        {
            var existing = _items.Find(x => x.Id == item.Id);
            if (existing != null)
            {
                _items.Remove(existing);
                _items.Add(item);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var item = _items.Find(x => x.Id == id);
            if (item != null)
                _items.Remove(item);
            return Task.CompletedTask;
        }
    }
}
