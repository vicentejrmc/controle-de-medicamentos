using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.Util;
using System.Xml;

namespace ControleDeMedicamentos.ModuloMedicamento;

public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
{
    public IRepositorioMedicamento repositorioMedicamento;
    public IRepositorioFornecedor repositorioFornecedor;

    public TelaMedicamento(IRepositorioMedicamento repositorioMedicamento, IRepositorioFornecedor repositorioFornecedor) : base("Medicamento", repositorioMedicamento)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public void ApresentarMenuMedicamentos()
    {
        Console.Clear();
        ExibirCabecalho();
        Console.WriteLine("Selecione uma opção:");
        Console.WriteLine("[1] Cadastrar Medicamento");
        Console.WriteLine("[2] Editar Medicamento");
        Console.WriteLine("[3] Excluir Medicamento");
        Console.WriteLine("[4] Visualizar Medicamentos");
        Console.WriteLine("[5] Import/Export Dados");
        Console.WriteLine("[S] Sair");

        Console.Write("\nEscolha uma das opções: ");
        string opcao = Console.ReadLine()!.ToUpper() ?? string.Empty;
        
        char operacaoEscolhida = Convert.ToChar(opcao[0]);

        EscolherOpcao(operacaoEscolhida);
  
    }

    public void EscolherOpcao(char operacao)
    {
        if (operacao == '1')
            CadastrarRegistro();
        else if (operacao == '2')
            EditarRegistro();
        else if (operacao == '3')
            ExcluirRegistro();
        else if (operacao == '4')
            VisualizarRegistros(true);
        else if (operacao == '5')
            MenuExportarDados();
        else if (operacao == 'S')
            return;
        else
        {
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);
            ApresentarMenuMedicamentos();
        }
    }

    private void MenuExportarDados()
    {
        Console.Clear();
        ExibirCabecalho();
        Console.WriteLine("Selecione uma opção:");
        Console.WriteLine("[1] Exportar PDF");
        Console.WriteLine("[2] Exportar CSV");

        Console.WriteLine("[S] Sair");

        Console.Write("\nEscolha uma das opções: ");
        string opcao = Console.ReadLine() ?? string.Empty;
        if (opcao.Length > 0)
        {
            char operacaoEscolhida = Convert.ToChar(opcao[0]);
            ContextoDados contexto = new ContextoDados(true);

            if (operacaoEscolhida == '1')
            {
                contexto.ExportarParaPDF();
            }
            else if (operacaoEscolhida == '2')
            {
                contexto.ExportarParaCsv(repositorioMedicamento);
            }
            else if (operacaoEscolhida == 'S')
                return;
            else
            {
                Notificador.ExibirMensagem("Entrada Inválida! verifique a opção digitada e tente novamente.", ConsoleColor.Red);
                MenuExportarDados();
            }
        }
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();

        if (repositorioFornecedor.SelecionarTodos().Count == 0)
        {
            Notificador.ExibirMensagem("Erro! Você precisa cadastrar um 'Fornecedor' antes de cadastrar um 'Medicamento'.", ConsoleColor.Red); return;
        }

        Medicamento novoMedicamento = ObterDados();
        if (novoMedicamento == null)
        {
            CadastrarRegistro();
            return;
        }

        string ehValido = novoMedicamento.Validar();

        if (ehValido.Length > 0)
        {
            Notificador.ExibirMensagem(ehValido, ConsoleColor.Red); return;
        }

        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();
        foreach (Medicamento med in medicamentos)
        {
            if (med.NomeMedicamento == novoMedicamento.NomeMedicamento)
            {
                Notificador.ExibirMensagem("Medicamento já cadastrado!", ConsoleColor.Black);

                Console.Write($"Deseja adicionar {novoMedicamento.Quantidade} ao estoque? S/N: ");
                string resposta = Console.ReadLine()!.ToUpper() ?? string.Empty;

                if (resposta == "S")
                {
                   //med.AdicionarAoEstoque(novoMedicamento.Quantidade);
                    repositorioMedicamento.EditarRegistro(med.Id, med);
                    Notificador.ExibirMensagem("Medicamento atualizado com sucesso!", ConsoleColor.Green);
                }
                else
                    Notificador.ExibirMensagem("Cadastro cancelado!", ConsoleColor.Yellow); return;
            }
        }

        repositorioMedicamento.CadastrarRegistro(novoMedicamento);

        Notificador.ExibirMensagem("Medicamento cadastrado com sucesso!", ConsoleColor.Green);
    }

    public override Medicamento ObterDados()
    {
        Console.Write("Digite o Nome do Medicamento: ");
        string nome = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite a Descrição Medicamento: ");
        string descricao = Console.ReadLine()! ?? string.Empty;

        TelaFornecedor telaFornecedor = new TelaFornecedor(repositorioFornecedor, repositorioMedicamento);
        telaFornecedor.VisualizarRegistros(false);

        Console.Write("Selecione o Id do Fornecedor: ");
        int idFornecedor = Convertor.ConverterStringParaInt();
        if (idFornecedor == 0) return null;

        repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

        Fornecedor fornecedor = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

        Console.Write("Digite a Quantidade de Medicamento: ");
        int quantidade = Convert.ToInt32(Console.ReadLine()!);

        Medicamento novoMedicamento = new Medicamento(nome, descricao, fornecedor, quantidade);

        return novoMedicamento;

    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        if (exibirTitulo)
            ExibirCabecalho();

        Console.WriteLine("Visualizando Medicamentos");
        Console.WriteLine("\n--------------------------------------------");

        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();
        if (medicamentos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum Medicamento cadastrado!", ConsoleColor.Red);
            return;
        }

        Console.Write("Medicamentos com menos de 20 unidades: ");
        Notificador.ExibirCorDeFonte("EM FALTA", ConsoleColor.DarkYellow);
        Console.Write("Medicamentos com 0 unidades: ");
        Notificador.ExibirCorDeFonte("SEM ESTOQUE", ConsoleColor.Red);
        Console.WriteLine();

        Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}", 
            "ID", "Nome", "Quantidade", "Fornecedor", "Descrição");

        foreach (Medicamento med in medicamentos)
        {
            Console.ResetColor();

             if (med.Quantidade == 0)
                Console.ForegroundColor = ConsoleColor.Red;

            else if (med.Quantidade < 20)
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            
            Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}",
               med.Id, med.NomeMedicamento, med.Quantidade, med.Fornecedor.Nome, med.Descricao);
        }

        Notificador.ExibirMensagem("Pressione qualquer tecla para continuar...", ConsoleColor.Yellow);
    }
}