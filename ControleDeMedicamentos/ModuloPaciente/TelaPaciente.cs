using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.ModuloPaciente;

public class TelaPaciente : TelaBase<Paciente>, ITelaCrud
{
    public IRepositorioPaciente repositorioPaciente;

    public TelaPaciente(IRepositorioPaciente repositorioPaciente) : base("Paciente",repositorioPaciente)
    {
        this.repositorioPaciente = repositorioPaciente;
    }

    public override Paciente ObterDados()
    {
        Console.Write("Digite o nome do Paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do Paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o cartão do SUS do Paciente: ");
        string cartao = Console.ReadLine() ?? string.Empty;
        Paciente paciente = new Paciente(nome, telefone, cartao);
        return paciente;
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine("Visualizando Pacientes...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -6} | {1, -12} | {2, -15} | {3, -30}",
            "Id", "Nome", "Telefone", "Cartão SUS"
        );

        List<Paciente> registros = repositorioPaciente.SelecionarTodos();

        foreach (var p in registros)
        {

            Console.WriteLine(
                "{0, -6} | {1, -12} | {2, -15} | {3, -30}",
                p.Id, p.Nome, p.Telefone, p.CartaoSUS
            );
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }
}