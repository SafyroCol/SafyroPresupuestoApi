using SafyroPresupuestos.Data;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SafyroPresupuestos.DTOs;

namespace SafyroPresupuestos.Services
{
    public class PresupuestoService : IPresupuestoService
    {
        private readonly ApplicationDbContext _context;

        public PresupuestoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Presupuestos>> GetAll() => await _context.Presupuestos.ToListAsync();
        public async Task<Presupuestos> GetById(int id) => await _context.Presupuestos.FindAsync(id);
        public async Task<Presupuestos> Create(Presupuestos presupuesto)
        {
            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;
        }
        public async Task<Presupuestos> Update(int id, Presupuestos presupuesto)
        {
            var existing = await _context.Presupuestos.FindAsync(id);
            if (existing == null) return null;
            _context.Entry(existing).CurrentValues.SetValues(presupuesto);
            await _context.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> Delete(int id)
        {
            var presupuesto = await _context.Presupuestos.FindAsync(id);
            if (presupuesto == null) return false;
            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> ObtenerEmpresaPorPresupuesto(int presupuestoId)
        {
            var presupuesto = await _context.Presupuestos
                .FirstOrDefaultAsync(x => x.Id == presupuestoId);


            if (presupuesto == null)
                throw new Exception("Presupuesto no encontrado.");

            var proyecto =  _context.Proyectos
                 .FirstOrDefaultAsync(x => x.Id == presupuesto.ProyectoId);
           
            if (proyecto == null)
                throw new Exception("Proyecto no encontrado.");
            if(proyecto.Result != null)
                return proyecto.Result.EmpresaId;
            return -1;
        }

        public async Task<List<MaterialConCostoDto>> ObtenerMaterialesConCostosAsync(int presupuestoId)
        {
            var materiales = await _context.MaterialPresupuesto
                .Where(m => m.PresupuestoId == presupuestoId)
                .Select(m => new MaterialConCostoDto
                {
                    MaterialPresupuestoId = m.Id,
                    NombreMaterial = m.Material.Nombre,
                    UnidadMedida = m.UnidadDeMedida,
                    Cantidad = m.Cantidad,
                    CostoUnitarioPresupuestado = m.CostoUnitario,
                    CostoTotalPresupuestado = m.CostoUnitario * m.Cantidad
                })
                .ToListAsync();

            var costosReales = await _context.MaterialRealPresupuesto
                .Where(r => r.PresupuestoId == presupuestoId)
                .ToListAsync();

            foreach (var material in materiales)
            {
                var real = costosReales.FirstOrDefault(r => r.MaterialPresupuestoId == material.MaterialPresupuestoId);
                if (real != null)
                {
                    material.CostoReal = real.CostoReal;
                }
            }

            return materiales;
        }

        public async Task<ResponseDto<object>> RegistrarCostosRealesAsync(CostosRealesMaterialDto dto)
        {
            var response = new ResponseDto<object>();

            try
            {
                // Validación inicial
                if (dto.PresupuestoId <= 0 || dto.CostosReales == null || !dto.CostosReales.Any())
                {
                    response.Ok = false;
                    response.Mensaje = "Los datos para registrar costos reales son inválidos.";
                    return response;
                }

                int insertados = 0;
                int actualizados = 0;

                foreach (var item in dto.CostosReales)
                {
                    var existente = await _context.MaterialRealPresupuesto
                        .FirstOrDefaultAsync(x =>
                            x.PresupuestoId == dto.PresupuestoId &&
                            x.MaterialPresupuestoId == item.MaterialPresupuestoId
                        );

                    if (existente != null)
                    {
                        existente.CostoReal = item.CostoReal;
                        existente.FechaRegistro = DateTime.UtcNow;
                        _context.MaterialRealPresupuesto.Update(existente);
                        actualizados++;
                    }
                    else
                    {
                        var nuevo = new MaterialRealPresupuesto
                        {
                            Id = Guid.NewGuid(),
                            PresupuestoId = dto.PresupuestoId,
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
                response.Mensaje = "Costos reales registrados correctamente.";
                response.Contenido = new
                {
                    PresupuestoId = dto.PresupuestoId,
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

        public async Task<PresupuestoDto> ObtenerPorProyectoIdAsync(int proyectoId)
        {
            var presupuesto = await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.ProyectoId == proyectoId);

            if (presupuesto == null)
                return null;

            return new PresupuestoDto
            {
                Id = presupuesto.Id,
                ProyectoId = presupuesto.ProyectoId,
                Nombre = presupuesto.Nombre,
                FechaCreacion = presupuesto.Fecha,
                CostoMateriales = presupuesto.SubtotalMateriales,
                CostoManoObra = presupuesto.SubtotalManoObra,
                CostoDepreciacion = presupuesto.SubtotalEquipos,
                // Incluye más campos si los necesitas
            };
        }


    }
}