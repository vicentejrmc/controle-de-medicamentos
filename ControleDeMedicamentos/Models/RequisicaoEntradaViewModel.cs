using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloPaciente;

namespace ControleDeMedicamentos.Models
{
    public class CadastrarRequisicaoEntradaViewModel
    {
        public Guid MedicamentoId { get; set; }
        public Guid FuncionarioId { get; set; }
        public int QuantidadeRequisitada { get; set; }
        public List<SelecionarFuncionarioViewModel> FuncionariosDisponiveis { get; set; }

        public CadastrarRequisicaoEntradaViewModel() { }

        public CadastrarRequisicaoEntradaViewModel(Guid medicamentoId, List<Funcionario> funcionarios)
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
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public SelecionarFuncionarioViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class CadastrarRequisicaoSaidaViewModel
    {
        public Guid MedicamentoId { get; set; }
        public Guid PacienteId { get; set; }
        public int QuantidadeRequisitada { get; set; }
        public List<SelecionarPacienteViewModel> PacientesDisponiveis { get; set; }

        public CadastrarRequisicaoSaidaViewModel() { }

        public CadastrarRequisicaoSaidaViewModel(Guid medicamentoId, List<Paciente> pacientes)
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
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public SelecionarPacienteViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
