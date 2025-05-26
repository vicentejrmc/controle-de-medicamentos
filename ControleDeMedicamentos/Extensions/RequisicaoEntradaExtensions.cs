using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.Extensions
{
    public static class RequisicaoEntradaExtensions
    {
        public static RequisicaoEntrada ParaEntidade(
        this CadastrarRequisicaoEntradaViewModel formularioVM,
        List<Funcionario> funcionarios,
        List<Medicamento> medicamentos
    )
        {
            Funcionario funcionarioSelecionado = null;

            foreach (var f in funcionarios)
            {
                if (f.Id == formularioVM.FuncionarioId)
                    funcionarioSelecionado = f;
            }

            Medicamento medicamentoSelecionado = null;

            foreach (var m in medicamentos)
            {
                if (m.Id == formularioVM.MedicamentoId)
                    medicamentoSelecionado = m;
            }

            return new RequisicaoEntrada(funcionarioSelecionado, medicamentoSelecionado, formularioVM.QuantidadeRequisitada);
        }
    }
}
