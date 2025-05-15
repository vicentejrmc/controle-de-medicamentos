using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.ModuloFuncionario;

namespace ControleDeMedicamentos.Models
{
    public abstract class FormularioFuncionarioViewModel
    {
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
    }

    public class CadastrarFuncionarioViewModel : FormularioFuncionarioViewModel
    {
        public CadastrarFuncionarioViewModel(){ }

        public CadastrarFuncionarioViewModel(string nome, string cpf, string telefone) : this()
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
        }
    }

    public class VisualizarFuncionarioViewModel
    {
        public List<DetalhesFuncionarioViewModel> Registros { get; } = new List<DetalhesFuncionarioViewModel>();

        public VisualizarFuncionarioViewModel(List<Funcionario> funcionario)
        {
            foreach (Funcionario p in funcionario)
            {
                DetalhesFuncionarioViewModel detalhesVM = p.ParaDetalhesVM(); // Método de extenção 
                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesFuncionarioViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }

        public DetalhesFuncionarioViewModel(int id, string? nome, string? cPF, string? telefone)
        {
            Id = id;
            Nome = nome;
            CPF = cPF;
            Telefone = telefone;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome} CPF: {CPF}, Telefone: {Telefone}";
        }
    }
}
