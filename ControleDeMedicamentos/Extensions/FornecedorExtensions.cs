using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFornecedor;


namespace ControleDeMedicamentos.Extensions
{
    public static class FornecedorExtensions 
    {
        public static Fornecedor ParaEntidade(this FormularioFornecedorViewModel formularioVM)
        {
            return new Fornecedor(formularioVM.Nome, formularioVM.CNPJ, formularioVM.Telefone);
        }

        public static DetalhesFornecedorViewModel ParaDetalhesVM(this Fornecedor fornecedor)
        {
            return new DetalhesFornecedorViewModel(
                fornecedor.Id,
                fornecedor.Nome,
                fornecedor.CNPJ,
                fornecedor.Telefone
            );
        }
    }
}
