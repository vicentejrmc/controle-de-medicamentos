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
    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Paciente novoRegistro = ObterDados();

        foreach (var item in repositorioPaciente.SelecionarTodos())
        {
            if(item.CartaoSUS == novoRegistro.CartaoSUS)
            {
                Notificador.ExibirMensagem("o Cartão do SUS digitado já existe, tente novamente", ConsoleColor.Red);
                CadastrarRegistro();
            }
        }
        if (novoRegistro == null) return;

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        repositorioPaciente.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }
    public override Paciente ObterDados()
    {
        Console.Write("Digite o nome do Paciente: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone do Paciente: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o cartão do SUS do Paciente: ");
        string cartao = Console.ReadLine() ?? string.Empty;

        List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();
        foreach (var p in pacientes)
        {
            if (p.CartaoSUS == cartao)
            {
                Notificador.ExibirMensagem("Erro! Cartão SUS já cadastrado.", ConsoleColor.Red);
                return null!;
            }
        }

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