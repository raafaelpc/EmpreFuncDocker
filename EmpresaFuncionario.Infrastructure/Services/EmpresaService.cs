using EmpresaFuncionario.Domain.Entities;
using EmpresaFuncionario.Persistence;
using Microsoft.EntityFrameworkCore;
using EmpresaFuncionario.Infrastructure.Interfaces;

namespace EmpresaFuncionario.Infrastructure.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly AppDbContext _context;

        public EmpresaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empresa>> GetEmpresasAsync()
        {
            try
            {
                return await _context.Empresas.Include(e => e.Funcionarios).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log error and rethrow or handle accordingly
                throw new Exception("An error occurred while fetching the empresas.", ex);
            }
        }

        public async Task<Empresa> GetEmpresaByIdAsync(Guid id)
        {
            try
            {
                return await _context.Empresas.Include(e => e.Funcionarios)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the empresa with ID {id}.", ex);
            }
        }

        public async Task AddEmpresaAsync(Empresa empresa)
        {
            try
            {
                await _context.Empresas.AddAsync(empresa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the empresa.", ex);
            }
        }

        public async Task UpdateEmpresaAsync(Empresa empresa)
        {
            try
            {
                _context.Empresas.Update(empresa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the empresa with ID {empresa.Id}.", ex);
            }
        }

        public async Task DeleteEmpresaAsync(Guid id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa != null)
                {
                    _context.Empresas.Remove(empresa);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the empresa with ID {id}.", ex);
            }
        }
    }
}