using ControleDeMedicamentos.Compartilhado;

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
