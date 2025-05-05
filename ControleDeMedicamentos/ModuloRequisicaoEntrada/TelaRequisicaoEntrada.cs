using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.ModuloRequisicaoEntrada;

public class TelaRequisicaoEntrada : TelaBase<RequisicaoEntrada>, ITelaCrud
{
    IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    IRepositorioFuncionario repositorioFuncionario;
    IRepositorioMedicamento repositorioMedicamento;
    IRepositorioFornecedor repositorioFornecedor;

    public TelaRequisicaoEntrada(IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada, IRepositorioMedicamento repositorioMedicamento, 
        IRepositorioFuncionario repositorioFuncionario, IRepositorioFornecedor repositorioFornecedor) : base("Requisição de Entrada", repositorioRequisicaoEntrada)
    {
        this.repositorioRequisicaoEntrada = repositorioRequisicaoEntrada;
        this.repositorioFuncionario = repositorioFuncionario;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioFornecedor = repositorioFornecedor;
    }

    public void ApresentarMenuRequisicaoEntrada()
    {
        ExibirCabecalho();

        Console.WriteLine("[1] Cadastrar Requisição de Entrada.");
        Console.WriteLine("[2] Visualizar Requisições de Entrada.");
        Console.WriteLine("[S] Voltar");

        Console.Write("\nEscolha uma das Opções: ");
        string opcao = Console.ReadLine() ?? string.Empty;
        if (opcao.Length > 0)
        {
            char operacaoEscolhida = Convert.ToChar(opcao[0]);

            if (operacaoEscolhida == '1')
                CadastrarRegistro();

            if (operacaoEscolhida == '2')
                VisualizarRegistros(false);
        }
        else
        {
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);

            ApresentarMenuRequisicaoEntrada();
        }
    }

    public override void CadastrarRegistro()
    {
        ExibirCabecalho();
        Console.WriteLine("Cadastrando Requisição de Entrada");
        Console.WriteLine("\n--------------------------------------------");

        List<Medicamento> med = repositorioMedicamento.SelecionarTodos();
        if (med.Count == 0)
        {
            Notificador.ExibirMensagem("Erro! Ainda não há Medicamentos Cadastrados.", ConsoleColor.Red);
            return;
        }

        RequisicaoEntrada novaRequisicao = ObterDados();
        if (novaRequisicao == null) return;
        string erros = novaRequisicao.Validar();
       
        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        Medicamento medicamento = novaRequisicao.Medicamento;

        medicamento.AdicionarEstoque(novaRequisicao.Quantidade);

        repositorioRequisicaoEntrada.CadastrarRegistro(novaRequisicao);

        Notificador.ExibirMensagem("Requisição cadastrada com sucesso!", ConsoleColor.Green);
    }

    public override RequisicaoEntrada ObterDados()
    {
        Console.Write("Digite a data da requisição | dd/MM/yyyy |: ");
        string datastring = Console.ReadLine()!;
        DateTime? data = Convertor.ConverterStringParaDate(datastring);
        if (data == null) return null;

        TelaMedicamento telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
        telaMedicamento.VisualizarRegistros(false);

        Console.Write("Selecione o Id do Medicamento: ");
        int idMedicamento = Convertor.ConverterStringParaInt();
        if (idMedicamento == 0) return null;

        Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);
        if (medicamento == null)
        {
            Notificador.ExibirMensagem("Medicamento não encontrado!", ConsoleColor.Red);
            return null!;
        }

        TelaFuncionario telaFuncionario = new TelaFuncionario(repositorioFuncionario);
        telaFuncionario.VisualizarRegistros(false);

        Console.Write("Selecione o Id do Funcionario: ");
        int idFuncionario = Convertor.ConverterStringParaInt();
        if (idFuncionario == 0) return null;

        Funcionario funcionario = repositorioFuncionario.SelecionarRegistroPorId(idFuncionario);
        
        if (funcionario == null)
        {
            Notificador.ExibirMensagem("Funcionario não encontrado!", ConsoleColor.Red);
            return null;
        }

        Console.Write("Digite a quantidade: ");
        int quantidade = Convertor.ConverterStringParaInt();
        if (quantidade == 0) return null;

        return new RequisicaoEntrada(data, medicamento, funcionario, quantidade);
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        ExibirCabecalho();
        Console.WriteLine("Visualizando Requisições de Entrada");
        Console.WriteLine("--------------------------------------------\n");

        List<RequisicaoEntrada> requisicoes = repositorioRequisicaoEntrada.SelecionarTodos();

        Console.WriteLine("{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -10}"
            ,"ID", "Data", "Medicamento", "Funcionario", "Quantidade");

        foreach (RequisicaoEntrada req in requisicoes)
        {
            Console.WriteLine("{0, -10} | {1, -15} | {2, -20} | {3, -20} | {4, -10}",
                req.Id, req.Data.ToString("dd/MM/yyyy"), req.Medicamento.NomeMedicamento, req.Funcionario.Nome, req.Quantidade);
        }

        Notificador.ExibirMensagem("Pressione qualquer tecla para continuar...", ConsoleColor.Yellow);
    }
}