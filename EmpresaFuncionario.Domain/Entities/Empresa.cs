using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaFuncionario.Domain.Entities
{
    public class Empresa
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
