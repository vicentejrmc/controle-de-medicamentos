using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.ModuloFornecedor;


namespace ControleDeMedicamentos.Models
{
    public abstract class FormularioFornecedorViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
    }

    public class CadastrarFornecedorViewModel : FormularioFornecedorViewModel
    {
        public CadastrarFornecedorViewModel(){ }

        public CadastrarFornecedorViewModel(string nome, string cnpj, string telefone) : this()
        {
            Nome = nome;
            CNPJ = cnpj;
            Telefone = telefone;
        }
    }

    public class EditarFornecedorViewModel : FormularioFornecedorViewModel
    {
        public EditarFornecedorViewModel() { }

        public EditarFornecedorViewModel(int id, string nome, string cnpj, string telefone) : this()
        {
            Id = id;
            Nome = nome;
            CNPJ= cnpj;
            Telefone = telefone;
        }

    }

    public class ExcluirFornecedorViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }

        public ExcluirFornecedorViewModel() { }

        public ExcluirFornecedorViewModel(int id, string? nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class VisualizarFornecedorViewModel
    {
        public List<DetalhesFornecedorViewModel> Registros { get; } = new List<DetalhesFornecedorViewModel>();

        public VisualizarFornecedorViewModel(List<Fornecedor> fornecedor)
        {
            foreach (Fornecedor p in fornecedor)
            {
                DetalhesFornecedorViewModel detalhesVM = p.ParaDetalhesVM(); // Método de extenção 
                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesFornecedorViewModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? CNPJ { get; set; }
        public string? Telefone { get; set; }

        public DetalhesFornecedorViewModel(int id, string? nome, string? cnpj, string? telefone)
        {
            Id = id;
            Nome = nome;
            CNPJ = cnpj;
            Telefone = telefone;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome} CNPJ: {CNPJ}, Telefone: {Telefone}";
        }
    }
}
