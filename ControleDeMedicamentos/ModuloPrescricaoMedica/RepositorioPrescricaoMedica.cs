using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class RepositorioPrescricaoMedica : RepositorioBasEmArquivo<PrescricaoMedica>, IRepositorioPrescricaoMedica
{
    public RepositorioPrescricaoMedica(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<PrescricaoMedica> ObterRegistros()
    {
        return contexto.PrescricoesMedicas;
    }
}