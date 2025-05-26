using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.ModuloRequisicaoSaida;

public class RepositorioRequisicaoSaida : IRepositorioRequisicaoSaida
{
    private readonly ContextoDados contexto;
    private List<RequisicaoSaida> requisicoesSaida;

    public RepositorioRequisicaoSaida(ContextoDados contexto)
    {
        this.contexto = contexto;
        this.requisicoesSaida = contexto.RequisicaoSaidas;
    }

    public void CadastrarRequisicaoSaida(RequisicaoSaida requisicao)
    {
        requisicoesSaida.Add(requisicao);
        contexto.SalvarJson();
    }

    protected List<RequisicaoSaida> ObterRegistros()
    {
        return contexto.RequisicaoSaidas;
    }
}
