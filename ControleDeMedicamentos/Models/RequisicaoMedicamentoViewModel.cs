using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;

namespace ControleDeMedicamentos.Models
{
    public class CadastrarRequisicaoEntradaViewModel
    {
        public int MedicamentoId { get; set; }
        public int FuncionarioId { get; set; }
        public int QuantidadeRequisitada { get; set; }
        public List<SelecionarFuncionarioViewModel> FuncionariosDisponiveis { get; set; }

        public CadastrarRequisicaoEntradaViewModel() { }

        public CadastrarRequisicaoEntradaViewModel(int medicamentoId, List<Funcionario> funcionarios)
        {
            MedicamentoId = medicamentoId;
            FuncionariosDisponiveis = new List<SelecionarFuncionarioViewModel>();

            foreach (var f in funcionarios)
            {
                var selecionarVM = new SelecionarFuncionarioViewModel(f.Id, f.Nome);

                FuncionariosDisponiveis.Add(selecionarVM);
            }
        }
    }

    public class SelecionarFuncionarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public SelecionarFuncionarioViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class CadastrarRequisicaoSaidaViewModel
    {
        public int MedicamentoId { get; set; }
        public int PacienteId { get; set; }
        public int QuantidadeRequisitada { get; set; }
        public List<SelecionarPacienteViewModel> PacientesDisponiveis { get; set; }

        public CadastrarRequisicaoSaidaViewModel() { }

        public CadastrarRequisicaoSaidaViewModel(int medicamentoId, List<Paciente> pacientes)
        {
            MedicamentoId = medicamentoId;
            PacientesDisponiveis = new List<SelecionarPacienteViewModel>();

            foreach (var p in pacientes)
            {
                var selecionarVM = new SelecionarPacienteViewModel(p.Id, p.Nome);

                PacientesDisponiveis.Add(selecionarVM);
            }
        }
    }

    public class SelecionarPacienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public SelecionarPacienteViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}

public class SelecionarPrescricaoViewModel
{
    public Guid Id { get; set; }
    public string NomePaciente { get; set; }
    public DateTime DataEmissao { get; set; }
    public List<SelecionarMedicamentoPrescritoViewModel> MedicamentoPrescritos { get; set; }

    public SelecionarPrescricaoViewModel() { }

    public SelecionarPrescricaoViewModel(
        Guid id,
        string nomePaciente,
        DateTime dataEmissao,
        List<MedicamentoPrescrito> medicamentoPrescritos
    ) : this()
    {
        Id = id;
        NomePaciente = nomePaciente;
        DataEmissao = dataEmissao;

        MedicamentoPrescritos = new List<SelecionarMedicamentoPrescritoViewModel>();

        foreach (var m in medicamentoPrescritos)
        {
            var selecionarVM = new SelecionarMedicamentoPrescritoViewModel(m.Medicamento.Nome);

            MedicamentoPrescritos.Add(selecionarVM);
        }
    }

    public override string ToString()
    {
        var nomesMedicamentos = string.Join(", ", MedicamentoPrescritos);

        return string.Join(" - ", $"Emissão: {DataEmissao.ToShortDateString()}", $"[{nomesMedicamentos}]");
    }
}

public class SelecionarMedicamentoPrescritoViewModel
{
    public string Nome { get; set; }

    public SelecionarMedicamentoPrescritoViewModel(string nome)
    {
        Nome = nome;
    }

    public override string ToString()
    {
        return Nome;
    }
}
