using EmpresaFuncionario.Application.DTOs;
using EmpresaFuncionario.Domain.Entities;
using EmpresaFuncionario.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaFuncionario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmpresas()
        {
            var empresas = await _empresaService.GetEmpresasAsync();
            var empresasDTO = empresas.Select(e => new EmpresaDTO { Id = e.Id, Nome = e.Nome });
            return Ok(empresasDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmpresaById(Guid id)
        {
            var empresa = await _empresaService.GetEmpresaByIdAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            var empresaDTO = new EmpresaDTO { Id = empresa.Id, Nome = empresa.Nome };
            return Ok(empresaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpresa(EmpresaDTO empresaDTO)
        {
            var empresa = new Empresa { Id = empresaDTO.Id, Nome = empresaDTO.Nome };
            await _empresaService.AddEmpresaAsync(empresa);
            return CreatedAtAction(nameof(GetEmpresaById), new { id = empresa.Id }, empresaDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmpresa(Guid id, EmpresaDTO empresaDTO)
        {
            if (id != empresaDTO.Id)
            {
                return BadRequest();
            }
            var empresa = new Empresa { Id = empresaDTO.Id, Nome = empresaDTO.Nome };
            await _empresaService.UpdateEmpresaAsync(empresa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpresa(Guid id)
        {
            await _empresaService.DeleteEmpresaAsync(id);
            return NoContent();
        }
    }
}
