using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.ModuloPaciente;

namespace ControleDeMedicamentos.Models
{
    public abstract class FormularioPacienteViewModel // Classe abstrata gerando herança para reduzir a repetição de codigo/Atributos
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CartaoSus { get; set; }
    }

    public class CadastrarPacienteViewModel : FormularioPacienteViewModel
    {
        public CadastrarPacienteViewModel() { }

        public CadastrarPacienteViewModel(string nome, string telefone, string cartaoSus) : this()
        {
            Nome = nome;
            Telefone = telefone;
            CartaoSus = cartaoSus;
        }
    }

    public class EditarPacienteViewModel : FormularioPacienteViewModel
    {
        public int Id { get; set; }

        public EditarPacienteViewModel() { }

        public EditarPacienteViewModel(int id, string nome, string telefone, string cartaoSus) : this()
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CartaoSus = cartaoSus;
        }
    }

    public class ExcluirPacienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set;}

        public ExcluirPacienteViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class VisualizarPacientesViewModel
    {
        public List<DetalhesPacienteViewModel> Registros { get; } = new List<DetalhesPacienteViewModel>();

        public VisualizarPacientesViewModel(List<Paciente> pacientes)
        {
            foreach (Paciente p in pacientes)
            {
                DetalhesPacienteViewModel detalhesVM = p.ParaDetalhesVM(); // Método de extenção 
                Registros.Add(detalhesVM);
            }
        }
    }

    public class DetalhesPacienteViewModel // Mesmos atributos do EditarPacienteViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set;}
        public string Telefone { get; set;}
        public string CartaoSus { get; set;}

        public DetalhesPacienteViewModel(int id, string nome, string telefone, string cartaoSus)
        { 
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CartaoSus = cartaoSus;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nome: {Nome}, Telefone: {Telefone}, CartaoSus: {CartaoSus}";
        }
    }
}
