using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloRequisicaoEntrada;

public class RepositorioRequisicaoEntrada : IRepositorioRequisicaoEntrada
{
    private readonly ContextoDados contexto;
    private List<RequisicaoEntrada> requisicoesEntrada;

    public RepositorioRequisicaoEntrada(ContextoDados contexto)
    {
        this.contexto = contexto;
        this.requisicoesEntrada = contexto.RequisicaoEntradas;
    }

    public void CadastrarRequisicaoEntrada(RequisicaoEntrada requisicao)
    {
        requisicoesEntrada.Add(requisicao);
        contexto.SalvarJson();
    }

    protected List<RequisicaoEntrada> ObterRegistros()
    {
        return contexto.RequisicaoEntradas;
    }
}