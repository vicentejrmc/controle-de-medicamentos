using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.Util;
namespace ControleDeMedicamentos.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    public IRepositorioFornecedor repositorioFornecedor;
    public TelaFornecedor(IRepositorioFornecedor repositorioFornecedor) : base("Fornecedor", repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();
        Fornecedor novoRegistro = ObterDados();

        if (novoRegistro == null) CadastrarRegistro();

        foreach (var item in repositorioFornecedor.SelecionarTodos())
        {
            if (item.CNPJ == novoRegistro.CNPJ)
            {
                Notificador.ExibirMensagem("o CNPJ digitado já existe, tente novamente", ConsoleColor.Red);
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

        repositorioFornecedor.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }
    public override Fornecedor ObterDados()
    {
        Console.Write("Digite o nome do Fornecedor: ");
        string nome = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite o telefone do fornecedor: ");
        string telefone = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite o CNPJ do fornecedor: ");
        string cnpj = Console.ReadLine()! ?? string.Empty;

        List<Fornecedor> Fornecedores = repositorioFornecedor.SelecionarTodos();
        foreach(var f in Fornecedores)
        {
            if (f.CNPJ == cnpj)
            {
                Notificador.ExibirMensagem("Erro! CNPJ já cadastrado.", ConsoleColor.Red);
                return null!;
            }
        }

        Fornecedor novoFornecedor = new Fornecedor(nome, cnpj, telefone);

        return novoFornecedor;
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        Console.WriteLine();

        Console.WriteLine("Visualizando Fornecedores...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -30} | {2, -20} | {3, -20}",
            "Id", "Nome", "Telefone", "CNPJ"
        );

        List<Fornecedor> registros = repositorioFornecedor.SelecionarTodos();
        foreach (var e in registros)
        {
            Console.WriteLine(
                "{0, -10} | {1, -30} | {2, -20} | {3, -20}",
                e.Id, e.Nome, e.Telefone, e.CNPJ
            );
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }
}