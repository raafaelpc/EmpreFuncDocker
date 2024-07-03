using EmpresaFuncionario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaFuncionario.Infrastructure.Interfaces
{
    public interface IEmpresaService
    {
        Task<IEnumerable<Empresa>> GetEmpresasAsync();
        Task<Empresa> GetEmpresaByIdAsync(Guid id);
        Task AddEmpresaAsync(Empresa empresa);
        Task UpdateEmpresaAsync(Empresa empresa);
        Task DeleteEmpresaAsync(Guid id);
    }
}
