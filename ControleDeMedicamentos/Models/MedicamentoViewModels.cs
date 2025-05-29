using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;

namespace ControleDeMedicamentos.Models;

public abstract class FormularioMedicamentoViewModel
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
    public int FornecedorId { get; set; }
    // pegamos uma unica informação do objeto mais complexo FornecedorId
    // e com essa informação nós conseguimos depois recuperar as informações completas dele
    //para isso além das informações básicas passamos uma lista do objetdo mais complexo que será selecionado
    public List<SelecionarFornecedorViewModel> FornecedoresDisponiveis { get; set; }

    protected FormularioMedicamentoViewModel()
    {
        FornecedoresDisponiveis = new List<SelecionarFornecedorViewModel>();
    }
}

public class SelecionarFornecedorViewModel // atravez dessa classe iremos recuperar os dados do objeto complexo
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarFornecedorViewModel(int id, string nome) 
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarMedicamentoViewModel : FormularioMedicamentoViewModel
{ // Desta forma, toda vez que formos cadastar um Medicamento, precisamos passar uma lista de fornecedor

    public CadastrarMedicamentoViewModel() { }

    public CadastrarMedicamentoViewModel(List<Fornecedor> fornecedores)
    {
        foreach (var fornecedor in fornecedores)
        {
            var selecionarVM = new SelecionarFornecedorViewModel(fornecedor.Id, fornecedor.Nome);

            FornecedoresDisponiveis.Add(selecionarVM);
        }         
    }
}

public class EditarMedicamentoViewModel : FormularioMedicamentoViewModel
{
    public int Id { get; set; }
    public List<Fornecedor> Fornecedor { get; set; }

    public EditarMedicamentoViewModel() { }

    public EditarMedicamentoViewModel(
        int id, 
        string nome, 
        string descricao, 
        int fornecedorId,
        List<Fornecedor> fornecedores, 
        int quantidade)
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        Quantidade = quantidade;
        FornecedorId = fornecedorId;

        foreach (var fornecedor in fornecedores)
        {
            var selecionarVM = new SelecionarFornecedorViewModel(fornecedor.Id, fornecedor.Nome);

            FornecedoresDisponiveis.Add(selecionarVM);
        }
    }
}

public class ExcluirMedicamentoViewModel
{ 
    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirMedicamentoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class VisualizarMedicamentosViewModel
{
    public List<DetalhesMedicamentoViewModel> Registros { get; set; }

    public VisualizarMedicamentosViewModel(List<Medicamento> medicamentos)
    {
        Registros = new List<DetalhesMedicamentoViewModel>();

        foreach (Medicamento m in medicamentos)
        {
            DetalhesMedicamentoViewModel detalhesVM = m.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
   
}

public class DetalhesMedicamentoViewModel
{
    private string nome;

    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string NomeFornecedor { get; set; }
    public int Quantidade { get; set; }
    public bool EmFalta { get; set; }

    public DetalhesMedicamentoViewModel(
        int id, 
        string nome, 
        string descricao, 
        string nomeFornecedor, 
        int quantidade,
        bool emFalta
        )
    {
        Id = id;
        Nome = nome;
        Descricao = descricao;
        NomeFornecedor = nomeFornecedor;
        Quantidade = quantidade;
        EmFalta = emFalta;
    }

    public override string ToString()
    {
        return $"Id: {Id} Nome: {Nome} Descrição: {Descricao} Fornecedor: {NomeFornecedor} Quantidade: {Quantidade}";
    }
}