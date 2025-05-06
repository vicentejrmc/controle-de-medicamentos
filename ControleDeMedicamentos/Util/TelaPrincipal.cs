using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.Util;

public class TelaPrincipal
{
    public char opcaoPrincipal;
    private ContextoDados contexto;
    private IRepositorioFuncionario repositorioFuncionario;
    private IRepositorioPaciente repositorioPaciente;
    private IRepositorioFornecedor repositorioFornecedor;
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    private IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    private IRepositorioRequisicaoSaida repositorioRequisicaoSaida;

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);
        repositorioFuncionario = new RepositorioFuncionario(contexto);
        repositorioPaciente = new RepositorioPaciente(contexto);
        repositorioFornecedor = new RepositorioFornecedor(contexto);
        repositorioMedicamento = new RepositorioMedicamento(contexto);
        repositorioRequisicaoEntrada = new RepositorioRequisicaoEntrada(contexto);
        repositorioPrescricaoMedica = new RepositorioPrescricaoMedica(contexto);
        repositorioRequisicaoSaida = new RepositorioRequisicaoSaida(contexto);
    }
    
    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|      Controle de Medicamentos        |");
        Console.WriteLine("----------------------------------------\n");

        Console.WriteLine("[1] Gestão de Fornecedores ");
        Console.WriteLine("[2] Gestão de Pacientes ");
        Console.WriteLine("[3] Gestao de Medicamentos");
        Console.WriteLine("[4] Gestão de Funcionarios");
        Console.WriteLine("[5] Requisição de Entrada");
        Console.WriteLine("[6] Prescrições Médicas");
        Console.WriteLine("[7] Requisições de Saída");
        Console.WriteLine("[S] Sair...");

        Console.Write("\nEscolha uma das opções: ");
        string opcaoEscolhida = Console.ReadLine()!.ToUpper() ?? string.Empty;
        if (opcaoEscolhida.Length > 0)
            opcaoPrincipal = Convert.ToChar(opcaoEscolhida[0]);
        else
        {
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);
            
            ApresentarMenuPrincipal();
        }

    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == 'S')
        {
            Console.WriteLine("\n---------------------");
            Console.WriteLine("Saindo do Sistema....");
            Console.WriteLine("-----------------------");
            Thread.Sleep(2000); ;
            Environment.Exit(0);
        }

        if (opcaoPrincipal == '1')
            return new TelaFornecedor(repositorioFornecedor, repositorioMedicamento);

        if (opcaoPrincipal == '2')
            return new TelaPaciente(repositorioPaciente);

        if (opcaoPrincipal == '3')
            return new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);

        if (opcaoPrincipal == '4')
           return new TelaFuncionario(repositorioFuncionario);

        if (opcaoPrincipal == '5')
            return new TelaRequisicaoEntrada(repositorioRequisicaoEntrada, repositorioMedicamento, repositorioFuncionario, repositorioFornecedor);

        if (opcaoPrincipal == '6')
            return new TelaPrescricaoMedica(repositorioMedicamento, repositorioPrescricaoMedica);

        if (opcaoPrincipal == '7')
            return new TelaRequisicaoSaida(repositorioPrescricaoMedica, repositorioMedicamento, repositorioPaciente, repositorioRequisicaoSaida);

        else
        {
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);
            ApresentarMenuPrincipal();
        }
        return null;
    }
}