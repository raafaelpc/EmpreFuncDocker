using EmpresaFuncionario.Domain.Entities;
using EmpresaFuncionario.Infrastructure.Interfaces;
using EmpresaFuncionario.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EmpresaFuncionario.Infrastructure.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly AppDbContext _context;

        public FuncionarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Funcionario>> GetFuncionariosAsync()
        {
            try
            {
                return await _context.Funcionarios.Include(f => f.Empresa).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the funcionarios.", ex);
            }
        }

        public async Task<Funcionario> GetFuncionarioByIdAsync(Guid id)
        {
            try
            {
                return await _context.Funcionarios.Include(f => f.Empresa)
                    .FirstOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the funcionario with ID {id}.", ex);
            }
        }

        public async Task AddFuncionarioAsync(Funcionario funcionario)
        {
            try
            {
                await _context.Funcionarios.AddAsync(funcionario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the funcionario.", ex);
            }
        }

        public async Task UpdateFuncionarioAsync(Funcionario funcionario)
        {
            try
            {
                _context.Funcionarios.Update(funcionario);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the funcionario with ID {funcionario.Id}.", ex);
            }
        }

        public async Task DeleteFuncionarioAsync(Guid id)
        {
            try
            {
                var funcionario = await _context.Funcionarios.FindAsync(id);
                if (funcionario != null)
                {
                    _context.Funcionarios.Remove(funcionario);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the funcionario with ID {id}.", ex);
            }
        }
    }
}
