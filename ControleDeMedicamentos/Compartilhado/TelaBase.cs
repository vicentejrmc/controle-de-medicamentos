using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>
{
    protected string nomeEntidade;
    char operacaoSelecionada = '0';
    private IRepositorio<T> repositorio;

    protected TelaBase(string nomeEntidade, IRepositorio<T> repositorio)
    {
        this.nomeEntidade = nomeEntidade;
        this.repositorio = repositorio;
    }

    public void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine($"Gestão de {nomeEntidade}");
        Console.WriteLine("--------------------------------------------\n");
    }

    public virtual char ApresentarMenu()
    {
        ExibirCabecalho();

        Console.WriteLine($"[1] Cadastrar {nomeEntidade}");
        Console.WriteLine($"[2] Editar {nomeEntidade}");
        Console.WriteLine($"[3] Excluir {nomeEntidade}");
        Console.WriteLine($"[4] Visualizar {nomeEntidade}s");
        Console.WriteLine("[S] Voltar");

        Console.Write("\nEscolha uma das opções: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper() ?? string.Empty!;
        if (opcaoEscolhida.Length > 0)
            operacaoSelecionada = Convert.ToChar(opcaoEscolhida[0]);
        else
        {
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);
           
            ApresentarMenu();
        }

        return operacaoSelecionada;
    }

    public virtual void CadastrarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"Cadastrando {nomeEntidade}...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        T novoRegistro = ObterDados();

        if (novoRegistro == null) return;

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);

            CadastrarRegistro();

            return;
        }

        repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem("O registro foi concluído com sucesso!", ConsoleColor.Green);
    }

    public virtual void EditarRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Editando {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convertor.ConverterStringParaInt();
        if (idRegistro == 0) return;

        Console.WriteLine();

        T registroEditado = ObterDados();

        bool conseguiuEditar = repositorio.EditarRegistro(idRegistro, registroEditado);

        if (!conseguiuEditar)
        {
            Notificador.ExibirMensagem("Houve um erro durante a edição do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi editado com sucesso!", ConsoleColor.Green);
    }

    public virtual void ExcluirRegistro()
    {
        ExibirCabecalho();

        Console.WriteLine($"Excluindo {nomeEntidade}...");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        VisualizarRegistros(false);

        Console.Write("Digite o ID do registro que deseja selecionar: ");
        int idRegistro = Convertor.ConverterStringParaInt();
        if (idRegistro == 0) return;

        Console.WriteLine();

        bool conseguiuExcluir = repositorio.ExcluirRegistro(idRegistro);

        if (!conseguiuExcluir)
        {
            Notificador.ExibirMensagem("Houve um erro durante a exclusão do registro...", ConsoleColor.Red);

            return;
        }

        Notificador.ExibirMensagem("O registro foi excluído com sucesso!", ConsoleColor.Green);
    }

    public abstract void VisualizarRegistros(bool exibirTitulo);

    public abstract T ObterDados();
}

