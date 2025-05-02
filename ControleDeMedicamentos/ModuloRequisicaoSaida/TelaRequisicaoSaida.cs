using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloRequisicaoSaida;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
{
    public TelaRequisicaoSaida(string nomeEntidade, IRepositorio<RequisicaoSaida> repositorio) : base("Requisição de Saida", repositorio)
    {
    }

    public override RequisicaoSaida ObterDados()
    {
        throw new NotImplementedException();
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        throw new NotImplementedException();
    }
}