using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloRequisicaoSaida;

public class RepositorioRequisicaoSaida : RepositorioBasEmArquivo<RequisicaoSaida>, IRepositorioRequisicaoSaida
{
    public RepositorioRequisicaoSaida(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicaoSaidas;
    }
}