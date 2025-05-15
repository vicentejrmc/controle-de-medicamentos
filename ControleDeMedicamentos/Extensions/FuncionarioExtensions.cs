using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFuncionario;


namespace ControleDeMedicamentos.Extensions
{
    public static class FuncionarioExtensions 
    {
        public static Funcionario ParaEntidade(this FormularioFuncionarioViewModel formularioVM)
        {
            return new Funcionario(formularioVM.Nome, formularioVM.CPF, formularioVM.Telefone);
        }

        public static DetalhesFuncionarioViewModel ParaDetalhesVM(this Funcionario funcionario)
        {
            return new DetalhesFuncionarioViewModel(
                funcionario.Id,
                funcionario.Nome,
                funcionario.CPF,
                funcionario.Telefone
            );
        }
    }
}
