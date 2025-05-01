using ControleDeMedicamentos.Compatilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ModuloFuncionario
{
    public class RepositorioFuncionario : RepositorioBasEmArquivo<Funcionario>, IRepositorioFuncionario
    {
        public RepositorioFuncionario(ContextoDados contexto) : base(contexto)
        {
        }

        protected override List<Funcionario> ObterRegistros()
        {
            return contexto.Funcionario;
        }
    }
}
