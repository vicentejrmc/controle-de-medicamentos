using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;

namespace ControleDeMedicamentos.Models;

public abstract class FormularioMedicamentoViewModel
{
    public string NomeMedicamento { get; set; }
    public string Descricao { get; set; }
    public Fornecedor Fornecedor { get; set; }
    public int Quantidade { get; set; }
}


public class CadastrarMedicamentoViewModel : FormularioMedicamentoViewModel
{

    public CadastrarMedicamentoViewModel() { }

    public CadastrarMedicamentoViewModel(
        string nomeMedicamento, string descricao, 
        Fornecedor fornecedor, int quantidade) : this()
    {
        NomeMedicamento = nomeMedicamento;
        Descricao = descricao;
        Fornecedor = fornecedor;
        Quantidade = quantidade;
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
    public string NomeMedicamento { get; set; }
    public string Descricao { get; set; }
    public string NomeFornecedor { get; set; }
    public int Quantidade { get; set; }

    public DetalhesMedicamentoViewModel(
        int id, 
        string nomeMedicamento, 
        string descricao, 
        string nomeFornecedor, 
        int quantidade)
    {
        Id = id;
        NomeMedicamento = nomeMedicamento;
        Descricao = descricao;
        NomeFornecedor = nomeFornecedor;
        Quantidade = quantidade;
    }

    public override string ToString()
    {
        return $"Id: {Id} " +
            $"- NomeMedicamento: {NomeMedicamento} " +
            $"- Descrição: {Descricao} " +
            $"- Fornecedor: {NomeFornecedor}" +
            $" - Quantidade: {Quantidade}";
    }
}