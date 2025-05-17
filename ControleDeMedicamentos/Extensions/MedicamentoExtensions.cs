using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloMedicamento;
using System.Runtime.CompilerServices;


namespace ControleDeMedicamentos.Extensions;

public static class MedicamentoExtensions
{
    public static Medicamento ParaEntidade(this FormularioMedicamentoViewModel formularioVM)
    {
        return new Medicamento(
            formularioVM.NomeMedicamento,
            formularioVM.Descricao,
            formularioVM.Fornecedor,
            formularioVM.Quantidade
        );
    }

    public static DetalhesMedicamentoViewModel ParaDetalhesVM(this Medicamento medicamento)
    {
        return new DetalhesMedicamentoViewModel(
            medicamento.Id,
            medicamento.NomeMedicamento,
            medicamento.Descricao,
            medicamento.Fornecedor.Nome,
            medicamento.Quantidade
        );
    }
}
