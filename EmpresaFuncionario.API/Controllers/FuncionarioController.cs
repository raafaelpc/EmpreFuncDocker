using EmpresaFuncionario.Application.DTOs;
using EmpresaFuncionario.Domain.Entities;
using EmpresaFuncionario.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaFuncionario.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFuncionarios()
        {
            var funcionarios = await _funcionarioService.GetFuncionariosAsync();
            var funcionariosDTO = funcionarios.Select(f => new FuncionarioDTO { Id = f.Id, Nome = f.Nome, EmpresaId = f.EmpresaId });
            return Ok(funcionariosDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFuncionarioById(Guid id)
        {
            var funcionario = await _funcionarioService.GetFuncionarioByIdAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            var funcionarioDTO = new FuncionarioDTO { Id = funcionario.Id, Nome = funcionario.Nome, EmpresaId = funcionario.EmpresaId };
            return Ok(funcionarioDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddFuncionario(FuncionarioDTO funcionarioDTO)
        {
            var funcionario = new Funcionario { Id = funcionarioDTO.Id, Nome = funcionarioDTO.Nome, EmpresaId = funcionarioDTO.EmpresaId };
            await _funcionarioService.AddFuncionarioAsync(funcionario);
            return CreatedAtAction(nameof(GetFuncionarioById), new { id = funcionario.Id }, funcionarioDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFuncionario(Guid id, FuncionarioDTO funcionarioDTO)
        {
            if (id != funcionarioDTO.Id)
            {
                return BadRequest();
            }
            var funcionario = new Funcionario { Id = funcionarioDTO.Id, Nome = funcionarioDTO.Nome, EmpresaId = funcionarioDTO.EmpresaId };
            await _funcionarioService.UpdateFuncionarioAsync(funcionario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuncionario(Guid id)
        {
            await _funcionarioService.DeleteFuncionarioAsync(id);
            return NoContent();
        }
    }
}
