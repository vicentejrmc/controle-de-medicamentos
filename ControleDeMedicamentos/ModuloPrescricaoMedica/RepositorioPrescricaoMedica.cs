using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloPaciente;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class RepositorioPrescricaoMedica : RepositorioBasEmArquivo<PrescricaoMedica>, IRepositorioPrescricaoMedica
{
    public RepositorioPrescricaoMedica(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<PrescricaoMedica> ObterRegistros()
    {
        throw new NotImplementedException();
    }
}