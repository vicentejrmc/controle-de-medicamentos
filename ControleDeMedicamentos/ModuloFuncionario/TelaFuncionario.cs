using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.ModuloFuncionario;

public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
{
    public IRepositorioFuncionario repositorioFuncionario;

    public TelaFuncionario(IRepositorioFuncionario repositorioFuncionario) : base("Funcionario", repositorioFuncionario)
    {
        this.repositorioFuncionario = repositorioFuncionario;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Funcionarios");
        Console.WriteLine("\n--------------------------------------------");

        Funcionario novoFuncionario = ObterDados();
        if (novoFuncionario == null) CadastrarRegistro();
        string erros = novoFuncionario.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        Notificador.ExibirMensagem("Funcionario cadastrado com sucesso!", ConsoleColor.Green);
    }

    public override Funcionario ObterDados()
    {
        Console.Write("Digite o nome do funcionario: ");
        string nome = Console.ReadLine()! ?? string.Empty;

        Console.WriteLine("Digite o CPF. XXX.XXX.XXX-XX |");
        Console.Write("CPF: ");
        string cpf = Console.ReadLine()! ?? string.Empty;

        Console.WriteLine("Digite o telefone. XX XXXX-XXXX ou XX XXXXX-XXXX |");
        Console.Write("Telefone: ");
        string telefone = Console.ReadLine()! ?? string.Empty;

        Funcionario funcionario = new Funcionario(nome, cpf, telefone);
        foreach (var item in repositorioFuncionario.SelecionarTodos()) // Validação de CPF
        {
            if (item.CPF == funcionario.CPF)
            {
                Notificador.ExibirMensagem("Erro! CPF já cadastrado.", ConsoleColor.Red);
                return null;
            }
        }
        return funcionario;
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
        ExibirCabecalho();

        Console.WriteLine("Visualizando Funcionarios...");
        Console.WriteLine("----------------------------------------");

        List<Funcionario> funcionarios = repositorioFuncionario.SelecionarTodos();

        Console.WriteLine(
       "{0, -6} | {1, -20} | {2, -25} | {3, -20}", "Id", "Nome", "CPF", "Telefone");

        foreach (Funcionario funcionario in funcionarios)
        {
            Console.WriteLine(
                "{0, -6} | {1, -20} | {2, -25} | {3, -20}",
                funcionario.Id,
                funcionario.Nome,
                funcionario.CPF,
                funcionario.Telefone);
        }

        Notificador.ExibirMensagem("Pressione qualquer tecla para continuar...", ConsoleColor.Yellow);
    }

}