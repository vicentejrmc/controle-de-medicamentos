using ControleDeMedicamentos.Compatilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ModuloPaciente
{
    class RepositorioPaciente : RepositorioBasEmArquivo<Paciente>, IRepositorioPaciente
    {

        public RepositorioPaciente(ContextoDados contexto) : base(contexto)
        {

        }
        protected override List<Paciente> ObterRegistros()
        {
            return contexto.Pacientes;
        }
    }
}
