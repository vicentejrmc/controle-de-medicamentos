using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloRequisicaoSaida;

public interface IRepositorioRequisicaoSaida
{
    public void CadastrarRequisicaoSaida(RequisicaoSaida requisicao);
}