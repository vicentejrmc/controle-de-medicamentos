using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloPrescricaoMedica;

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

    public TelaPrincipal()
    {
        contexto = new ContextoDados(true);
        repositorioFuncionario = new RepositorioFuncionario(contexto);
        repositorioPaciente = new RepositorioPaciente(contexto);
        repositorioFornecedor = new RepositorioFornecedor(contexto);
        repositorioMedicamento = new RepositorioMedicamento(contexto);
        repositorioRequisicaoEntrada = new RepositorioRequisicaoEntrada(contexto);
        repositorioPrescricaoMedica = new RepositorioPrescricaoMedica(contexto);
    }
    
    public void ApresentarMenuPrincipal()
    {
        Console.Clear();

        Console.WriteLine("----------------------------------------");
        Console.WriteLine("|      Controle de MedicamentosDaPrescricao        |");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine();

        Console.WriteLine("1 - Gestão de Fornecedor ");
        Console.WriteLine("2 - Gestão de Paciente ");
        Console.WriteLine("3 - Gestao de MedicamentosDaPrescricao");
        Console.WriteLine("4 - Gestão de Funcionarios");
        Console.WriteLine("5 - Requisição de Entrada");
        Console.WriteLine("6 - Gestão de Prescrições Médicas");
        Console.WriteLine();
        Console.WriteLine("S - Sair");

        Console.WriteLine();

        EscolherOpcao();
    }

    public ITelaCrud ObterTela()
    {
        if (opcaoPrincipal == 'S')
        {
            Console.WriteLine("\n---------------------");
            Console.WriteLine("Saindo do Sistema....");
            Console.WriteLine("---------------------");
            Thread.Sleep(1500); ;
            Environment.Exit(0);
        }

        if (opcaoPrincipal == '1')
            return new TelaFornecedor(repositorioFornecedor);

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

        else
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);

        return null!;
    }

    private void EscolherOpcao()
    {
        Console.Write("Escolha uma das opções: ");

        opcaoPrincipal = Convert.ToChar(Console.ReadLine()!.ToUpper());
    }
}