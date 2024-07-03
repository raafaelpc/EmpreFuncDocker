using EmpresaFuncionario.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaFuncionario.Infrastructure.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> GetFuncionariosAsync();
        Task<Funcionario> GetFuncionarioByIdAsync(Guid id);
        Task AddFuncionarioAsync(Funcionario funcionario);
        Task UpdateFuncionarioAsync(Funcionario funcionario);
        Task DeleteFuncionarioAsync(Guid id);
    }
}
