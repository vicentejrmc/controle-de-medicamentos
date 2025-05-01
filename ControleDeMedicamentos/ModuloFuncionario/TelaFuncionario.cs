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

    protected void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Controle de Funcionarios");
        Console.WriteLine("\n--------------------------------------------");
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine("Cadastrando Funcionarios");
        Console.WriteLine("\n--------------------------------------------");

        Funcionario novoFuncionario = ObterDados();

        string erros = novoFuncionario.Validar();
        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

        Notificador.ExibirMensagem("Funcionario cadastrado com sucesso!", ConsoleColor.Green);
    }

    public override void EditarRegistro()
    {
        ExibirCabecalho();
        Console.WriteLine("Editando Funcionarios");
        Console.WriteLine("\n--------------------------------------------");

        VisualizarRegistros(false);

        Console.Write("Digite o id do funcionario que deseja editar: ");
        int idFuncionario = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

        Funcionario funcionario = repositorioFuncionario.SelecionarRegistroPorId(idFuncionario);

        funcionario = ObterDados();

        string erros = funcionario.Validar();
        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        repositorioFuncionario.EditarRegistro(idFuncionario, funcionario);

        Notificador.ExibirMensagem("Funcionario editado com sucesso!", ConsoleColor.Green);
    }

    public override Funcionario ObterDados()
    {
        Console.Write("Digite o nome do funcionario: ");
        string nome = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite o CPF do funcionario: ");
        string cpf = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite o telefone do funcionario: ");
        string telefone = Console.ReadLine()! ?? string.Empty;

        Funcionario funcionario = new Funcionario(nome, cpf, telefone);


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

    public override void ExcluirRegistro()
    {
        ExibirCabecalho();
        Console.WriteLine("Excluindo Funcionarios");
        Console.WriteLine("\n--------------------------------------------");

        VisualizarRegistros(false);

        Console.Write("Digite o id do funcionario que deseja excluir: ");
        int idFuncionario = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

        Funcionario funcionario = repositorioFuncionario.SelecionarRegistroPorId(idFuncionario);
        
        if (funcionario == null)
        {
            Notificador.ExibirMensagem("Funcionario não encontrado!", ConsoleColor.Red);
            return;
        }

        repositorioFuncionario.ExcluirRegistro(idFuncionario);

        Notificador.ExibirMensagem("Funcionario excluido com sucesso!", ConsoleColor.Green);
    }

}