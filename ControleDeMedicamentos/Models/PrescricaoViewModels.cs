using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;


namespace ControleDeMedicamentos.ConsoleApp.Model;

public class CadastrarPrescricaoViewModel
{
    public int PacienteId { get; set; }
    public List<SelecionarPacienteViewModel> PacientesDisponiveis { get; set; }

    public string CrmMedico { get; set; }

    public List<SelecionarMedicamentoViewModel> MedicamentosDisponiveis { get; set; }
    public List<DetalhesMedicamentoPrescritoViewModel> MedicamentosPrescritos { get; set; }

    public int MedicamentoId { get; set; }
    public string DosagemMedicamento { get; set; }
    public string PeriodoMedicamento { get; set; }
    public int QuantidadeMedicamento { get; set; }

    public CadastrarPrescricaoViewModel()
    {
        PacientesDisponiveis = new List<SelecionarPacienteViewModel>();
        MedicamentosDisponiveis = new List<SelecionarMedicamentoViewModel>();
        MedicamentosPrescritos = new List<DetalhesMedicamentoPrescritoViewModel>();
    }

    public CadastrarPrescricaoViewModel(List<Paciente> pacientes, List<Medicamento> medicamentos) : this()
    {
        AdicionarPacientes(pacientes);
        AdicionarMedicamentos(medicamentos);
    }

    public void AdicionarPacientes(List<Paciente> pacientes)
    {
        foreach (var p in pacientes)
        {
            var selecionarPacienteVM = new SelecionarPacienteViewModel(p.Id, p.Nome);

            PacientesDisponiveis.Add(selecionarPacienteVM);
        }
    }

    public void AdicionarMedicamentos(List<Medicamento> medicamentos)
    {
        foreach (var m in medicamentos)
        {
            var selecionarMedicamentoVM = new SelecionarMedicamentoViewModel(m.Id, m.NomeMedicamento);

            MedicamentosDisponiveis.Add(selecionarMedicamentoVM);
        }
    }
}

public class SelecionarMedicamentoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public SelecionarMedicamentoViewModel() { }

    public SelecionarMedicamentoViewModel(int id, string nome) : this()
    {
        Id = id;
        Nome = nome;
    }
}

public class DetalhesMedicamentoPrescritoViewModel
{
    public int MedicamentoId { get; set; }
    public string Medicamento { get; set; }
    public string Dosagem { get; set; }
    public string Periodo { get; set; }
    public int Quantidade { get; set; }

    public DetalhesMedicamentoPrescritoViewModel() { }

    public DetalhesMedicamentoPrescritoViewModel(
        int medicamentoId,
        string nomeMedicamento,
        string dosagem,
        string periodo,
        int quantidade
    ) : this()
    {
        MedicamentoId = medicamentoId;
        Medicamento = nomeMedicamento;
        Dosagem = dosagem;
        Periodo = periodo;
        Quantidade = quantidade;
    }
}


public class VisualizarPrescricoesViewModel
{
    public List<DetalhesPrescricaoViewModel> Registros { get; }

    public VisualizarPrescricoesViewModel(List<PrescricaoMedica> prescricoes)
    {
        Registros = [];

        foreach (var p in prescricoes)
        {
            var detalhesVM = p.ParaDetalhesVM();

            Registros.Add(detalhesVM);
        }
    }
}

public class DetalhesPrescricaoViewModel
{
    public int Id { get; set; }
    public string CrmMedico { get; set; }
    public string Paciente { get; set; }
    public DateTime DataEmissao { get; set; }

    public DetalhesPrescricaoViewModel(int id, string crmMedico, string paciente, DateTime dataEmissao)
    {
        Id = id;
        CrmMedico = crmMedico;
        Paciente = paciente;
        DataEmissao = dataEmissao;
    }
}