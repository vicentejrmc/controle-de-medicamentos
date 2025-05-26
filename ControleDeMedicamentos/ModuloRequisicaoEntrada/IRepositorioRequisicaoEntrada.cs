using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ModuloRequisicaoEntrada;

public interface IRepositorioRequisicaoEntrada
{
    public void CadastrarRequisicaoEntrada(RequisicaoEntrada requisicao);
}