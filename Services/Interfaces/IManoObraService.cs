using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IManoObraService
    {
        Task<List<ManoObra>> GetAll();
        Task<ManoObra> GetById(int id);
        Task<ManoObra> Create(ManoObra manoObra);
        Task<ManoObra> Update(int id, ManoObra manoObra);
        Task<bool> Delete(int id);
    }
}