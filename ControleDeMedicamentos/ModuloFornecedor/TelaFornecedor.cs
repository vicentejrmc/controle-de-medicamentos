using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.Util;
namespace ControleDeMedicamentos.ModuloFornecedor;

public class TelaFornecedor : TelaBase<Fornecedor>, ITelaCrud
{
    public IRepositorioFornecedor repositorioFornecedor;
    public IRepositorioMedicamento repositorioMedicamento;
    public TelaFornecedor(IRepositorioFornecedor repositorioFornecedor, IRepositorioMedicamento repositorioMedicamento) : base("Fornecedor", repositorioFornecedor)
    {
        this.repositorioFornecedor = repositorioFornecedor;
        this.repositorioMedicamento = repositorioMedicamento;
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
    public override void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convertor.ConverterStringParaInt();
        if (idRegistro == 0) return;

        foreach (var item in repositorioMedicamento.SelecionarTodos())
        {
            if(item.Fornecedor == repositorioFornecedor.SelecionarRegistroPorId(idRegistro))
            {
                Notificador.ExibirMensagem("não é possivel excluir um fornecedor com medicamento(s) cadastrado(s)", ConsoleColor.Red);
                return;
            }
        }

        Console.WriteLine();

        Console.WriteLine("Você tem certeza que deseja excluir o Registro? (S/N)");
        string resposta = Console.ReadLine()! ?? string.Empty;

        if (resposta.ToUpper() != "S")
        {
            Notificador.ExibirMensagem("Exclusão cancelada!", ConsoleColor.Yellow);
            return;
        }

        bool conseguiuExcluir = repositorioFornecedor.ExcluirRegistro(idRegistro);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
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