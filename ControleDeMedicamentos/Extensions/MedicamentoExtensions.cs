using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;
using System.Runtime.CompilerServices;


namespace ControleDeMedicamentos.Extensions;

public static class MedicamentoExtensions
{
    public static Medicamento ParaEntidade(this FormularioMedicamentoViewModel formularioVM, List<Fornecedor> fornecedor)
    {
        Fornecedor fornecedorSelecionado = null;

        foreach(var f in fornecedor)
        {
            if(f.Id == formularioVM.FornecedorId)
                fornecedorSelecionado = f;
        }

        return new Medicamento(
            formularioVM.Nome,
            formularioVM.Descricao,
            fornecedorSelecionado,
            formularioVM.Quantidade
        );
    }

    public static DetalhesMedicamentoViewModel ParaDetalhesVM(this Medicamento medicamento)
    {
        return new DetalhesMedicamentoViewModel(
            medicamento.Id,
            medicamento.Nome,
            medicamento.Descricao,
            medicamento.Fornecedor.Nome,
            medicamento.Quantidade,
            medicamento.EmFalta
        );
    }
}
