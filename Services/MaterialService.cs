using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly ApplicationDbContext _context;

        public MaterialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Material>> GetAll() => await _context.Materiales.ToListAsync();
        public async Task<Material> GetById(int id) => await _context.Materiales.FindAsync(id);
        public async Task<Material> Create(Material material)
        {
            _context.Materiales.Add(material);
            await _context.SaveChangesAsync();
            return material;
        }
        public async Task<Material> Update(int id, Material material)
        {
            var existing = await _context.Materiales.FindAsync(id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(material);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> Delete(int id)
        {
            var material = await _context.Materiales.FindAsync(id);
            if (material == null) return false;
            _context.Materiales.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<MaterialCostoDto>> ObtenerMaterialesConCostosAsync(int presupuestoId)
        {
            var materiales = await (from mp in _context.MaterialPresupuesto
                                    join mr in _context.MaterialRealPresupuesto
                                        on mp.Id equals mr.MaterialPresupuestoId into joined
                                    from mr in joined.DefaultIfEmpty()
                                    where mp.PresupuestoId == presupuestoId
                                    select new MaterialCostoDto
                                    {
                                        MaterialPresupuestoId = mp.Id,
                                        Nombre = mp.Material.Nombre,
                                        //Cantidad = mp.Cantidad,
                                        //CostoUnitarioPresupuestado = mp.CostoUnitario,
                                        //CostoRealUnitario = mr != null ? mr.CostoRealUnitario : null
                                    }).ToListAsync();

            return materiales;
        }
        public async Task<ResponseDto<object>> RegistrarCostosRealesAsync(int presupuestoId, List<CostoRealMaterialDto> costosReales)
        {
            var response = new ResponseDto<object>();

            try
            {
                if (presupuestoId <= 0 || costosReales == null || !costosReales.Any())
                {
                    response.Ok = false;
                    response.Mensaje = "Datos inválidos para registrar los costos reales.";
                    return response;
                }

                int insertados = 0;
                int actualizados = 0;

                foreach (var item in costosReales)
                {
                    var costoExistente = await _context.MaterialRealPresupuesto
                        .FirstOrDefaultAsync(c =>
                            c.PresupuestoId == presupuestoId &&
                            c.MaterialPresupuestoId == item.MaterialPresupuestoId
                        );

                    if (costoExistente != null)
                    {
                        costoExistente.CostoReal = item.CostoReal;
                        costoExistente.FechaRegistro = DateTime.UtcNow;
                        _context.MaterialRealPresupuesto.Update(costoExistente);
                        actualizados++;
                    }
                    else
                    {
                        var nuevo = new MaterialRealPresupuesto
                        {
                            Id = Guid.NewGuid(),
                            PresupuestoId = presupuestoId,
                            MaterialPresupuestoId = item.MaterialPresupuestoId,
                            CostoReal = item.CostoReal,
                            FechaRegistro = DateTime.UtcNow
                        };
                        await _context.MaterialRealPresupuesto.AddAsync(nuevo);
                        insertados++;
                    }
                }

                await _context.SaveChangesAsync();

                response.Ok = true;
                response.Mensaje = "Costos reales de materiales registrados correctamente.";
                response.Contenido = new
                {
                    PresupuestoId = presupuestoId,
                    Insertados = insertados,
                    Actualizados = actualizados,
                    TotalProcesados = insertados + actualizados
                };
            }
            catch (Exception ex)
            {
                response.Ok = false;
                response.Mensaje = $"Error al registrar los costos reales: {ex.Message}";
            }

            return response;
        }


    }
}