using Microsoft.EntityFrameworkCore;
using SafyroPresupuestos.Data;
using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using SafyroPresupuestos.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ApplicationDbContext _context;

        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(List<EmpresaDto> Empresas, int Total)> GetAllPagedAsync(int page, int pageSize, string? search)
        {
            var query = _context.Empresas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(e =>
                    e.Nombre.Contains(search) ||
                    e.Dominio.Contains(search)
                );
            }

            var total = await query.CountAsync();

            var empresas = await query
                .OrderBy(e => e.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmpresaDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Dominio = e.Dominio
                })
                .ToListAsync();

            return (empresas, total);
        }

        public async Task<List<EmpresaDto>> GetAllAsync()
        {
            return await _context.Empresas
                .OrderBy(e => e.Nombre)
                .Select(e => new EmpresaDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Dominio = e.Dominio
                }).ToListAsync();
        }

        public async Task<EmpresaDto?> GetByIdAsync(int id)
        {
            var entity = await _context.Empresas.FindAsync(id);
            if (entity == null) return null;

            return new EmpresaDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Dominio = entity.Dominio
            };
        }

        public async Task<EmpresaDto> CreateAsync(EmpresaCreateDto dto)
        {
            var entity = new Empresa
            {
                Nombre = dto.Nombre,
                Dominio = dto.Dominio
            };
            _context.Empresas.Add(entity);
            await _context.SaveChangesAsync();

            // Devuelves el DTO completo, ahora con Id generado por la DB
            return new EmpresaDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Dominio = entity.Dominio
            };
        }

        public async Task<EmpresaDto?> UpdateAsync(int id, EmpresaDto dto)
        {
            var entity = await _context.Empresas.FindAsync(id);
            if (entity == null) return null;

            entity.Nombre = dto.Nombre;
            entity.Dominio = dto.Dominio;

            _context.Empresas.Update(entity);
            await _context.SaveChangesAsync();

            return new EmpresaDto
            {
                Id = entity.Id,
                Nombre = entity.Nombre,
                Dominio = entity.Dominio
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Empresas.FindAsync(id);
            if (entity == null) return false;

            _context.Empresas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
