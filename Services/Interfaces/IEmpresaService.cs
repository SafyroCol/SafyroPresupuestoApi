using SafyroPresupuestos.DTOs;
using SafyroPresupuestos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SafyroPresupuestos.Services.Interfaces
{
    public interface IEmpresaService
    {
        Task<(List<EmpresaDto> Empresas, int Total)> GetAllPagedAsync(int page, int pageSize, string? search);
        Task<List<EmpresaDto>> GetAllAsync();
        Task<EmpresaDto?> GetByIdAsync(int id); // <- nullable
        Task<EmpresaDto> CreateAsync(EmpresaCreateDto dto);
        Task<EmpresaDto?> UpdateAsync(int id, EmpresaDto dto); // <- nullable
        Task<bool> DeleteAsync(int id);
    }
}
