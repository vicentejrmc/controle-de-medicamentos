using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Util;
namespace ControleDeMedicamentos.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    public IRepositorioFornecedor repositorioFornecedor;
    public TelaFornecedor(IRepositorioFornecedor repositorioFornecedor) : base("Fornecedor", repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public override Fornecedor ObterDados()
    {
        Console.Write("Digite o nome do Fornecedor: ");
        string nome = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite o telefone do fornecedor: ");
        string telefone = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite o CNPJ do fornecedor");
        string cnpj = Console.ReadLine()! ?? string.Empty;

        Fornecedor novoFornecedor = new Fornecedor(nome, telefone, cnpj);

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