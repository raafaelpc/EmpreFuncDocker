using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpresaFuncionario.Domain.Entities
{
    public class Funcionario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
