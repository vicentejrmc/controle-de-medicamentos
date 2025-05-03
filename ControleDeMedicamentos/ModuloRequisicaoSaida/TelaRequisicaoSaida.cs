using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.Util;
using System.Security.Cryptography.X509Certificates;

namespace ControleDeMedicamentos.ModuloRequisicaoSaida;

public class TelaRequisicaoSaida : TelaBase<RequisicaoSaida>, ITelaCrud
{
    public IRepositorioRequisicaoSaida repositorioRequisicaoSaida;
    public IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    public IRepositorioMedicamento repositorioMedicamento;
    public IRepositorioPaciente repositorioPaciente;

    public TelaRequisicaoSaida(IRepositorioPrescricaoMedica repositorioPrescricaoMedica, IRepositorioMedicamento repositorioMedicamento, IRepositorioPaciente repositorioPaciente, IRepositorioRequisicaoSaida repositorioRequisicaoSaida) : base("Requisição de Saida", repositorioRequisicaoSaida)
    {
        this.repositorioPrescricaoMedica = repositorioPrescricaoMedica;
        this.repositorioPaciente = repositorioPaciente;
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioRequisicaoSaida = repositorioRequisicaoSaida;
    }

    public void ApresentarMenuSaida()
    {
        ExibirCabecalho();

        Console.WriteLine();

        Console.WriteLine($"1 - Cadastrar requisição de saída");
        Console.WriteLine($"2 - Visualizar requisições de saída");
        Console.WriteLine($"3 - Visualizar requisições por paciente");

        Console.WriteLine("S - Voltar");

        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char operacaoEscolhida = Console.ReadLine()!.ToUpper()[0];
        Opcoes(operacaoEscolhida);


    }

    public void Opcoes(char opcao)
    {
        if (opcao == '1') CadastrarRegistro();
        else if (opcao == '2') VisualizarRegistros(true, repositorioRequisicaoSaida.SelecionarTodos());
        else if (opcao == '3') VisualizarRegistrosPorPaciente();

    }
    public override RequisicaoSaida ObterDados()
    {
        Console.Write("Digite a data da Solicitação(dd/mm/aaaa): ");
        DateTime data = DateTime.Parse(Console.ReadLine()!);
        Console.WriteLine();
        TelaPaciente telaPaciente = new TelaPaciente(repositorioPaciente);
        telaPaciente.VisualizarRegistros(false);
        Console.WriteLine("Digite o Id do Paciente que deseja fazer uma requisição: ");
        int idPaciente = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);
        Console.WriteLine();

        Paciente paciente = repositorioPaciente.SelecionarRegistroPorId(idPaciente);

        if (paciente == null)
        {
            Notificador.ExibirMensagem("paciente não encontrado!", ConsoleColor.Red);
            return null;
        }
        TelaPrescricaoMedica telaPrescricaoMedica = new TelaPrescricaoMedica(repositorioMedicamento, repositorioPrescricaoMedica);
        telaPrescricaoMedica.VisualizarRegistros(false);
        Console.WriteLine("Digite o Id da Prescrição Médica: ");
        int idPrescricao = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);
        Console.WriteLine();
        PrescricaoMedica prescricao = repositorioPrescricaoMedica.SelecionarRegistroPorId(idPrescricao);

        if (prescricao == null)
        {
            Notificador.ExibirMensagem("Prescrição médica não encontrada!", ConsoleColor.Red);
            return null;
        }
        List<int> quantidadeDeMedicamentos = new List<int>();
        for (int i = 0; i < prescricao.MedicamentosDaPrescricao.Count; i++)
        {
            Console.WriteLine();
            Medicamento m = prescricao.MedicamentosDaPrescricao[i];
            Console.WriteLine($"A quantidade em Estoque de {m.NomeMedicamento} é de {m.Quantidade}");
            Console.Write($"Digite a quantidade de medicamentos que você deseja: ");
            int quantidade = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);
            if (quantidade > m.Quantidade)
            {
                Notificador.ExibirMensagem("Não há medicamentos suficientes desse remédio", ConsoleColor.Red);
                i--;
                continue;
            }
            else
            {
                quantidadeDeMedicamentos.Add(quantidade);
                m.RemoverEstoque(quantidade);
            }
        }        
        RequisicaoSaida requisicaoSaida = new RequisicaoSaida(data, idPaciente, idPrescricao, quantidadeDeMedicamentos, prescricao.MedicamentosDaPrescricao);
        return requisicaoSaida;

    }
    public void VisualizarRegistros(bool exibirTitulo, List<RequisicaoSaida> repositorio)
    {
        if (exibirTitulo) ExibirCabecalho();

        Console.WriteLine("Visualizando Requisições de Saída...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -6} | {1, -10} | {2, -20} | {3, -20}",
            "Id", "Data", "Paciente", "Quant Remédios Selecionados"
        );

        foreach (var r in repositorio)
        {
            Console.WriteLine(
            "{0, -6} | {1, -10} | {2, -20} | {3, -20}",
            r.Id, r.Data, repositorioPaciente.SelecionarRegistroPorId(r.pacienteId).Nome, r.MedicamentosRequisitados.Count
            );
            Console.WriteLine();
            Console.WriteLine("Deseja ver essa requisição em detalhes(s/n)? ");
            string opcao = Console.ReadLine()!.ToUpper();
            Console.WriteLine();
            if (opcao == "S")
            {
                    Console.WriteLine(
                "{0, -10} | {1, -10}",
                "Medicamento", "Quantidade" 
                );
                int posicao = 0;
                foreach (var d in r.MedicamentosRequisitados)
                {
                    
                    Console.WriteLine(
                    "{0, -10} | {1, -10}",
                    r.medicamentosstring[posicao], r.QuantidadeDeMedicamentos[posicao]
                    );
                    posicao += 1;
                }
            }
            Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
        }

        
    }

    public void VisualizarRegistrosPorPaciente()
    {
        ExibirCabecalho();

        Console.WriteLine();
        TelaPaciente telaPaciente = new TelaPaciente(repositorioPaciente);
        telaPaciente.VisualizarRegistros(false);
        Console.Write("Digite o Id do Paciente: ");
        int idPaciente = int.Parse(Console.ReadLine()!);

        Paciente paciente = repositorioPaciente.SelecionarRegistroPorId(idPaciente);
        if (paciente == null) return;
        Console.WriteLine();
        List<RequisicaoSaida> requisicaoSaidas = new List<RequisicaoSaida>();
        foreach (var i in repositorioRequisicaoSaida.SelecionarTodos())
        {
            if (!(i.pacienteId == paciente.Id))
            {
                Notificador.ExibirMensagem("Esse paciente não tem nenhuma requisição de saída", ConsoleColor.Red);
                return;
            }
            requisicaoSaidas.Add(i);
        }

        VisualizarRegistros(false, requisicaoSaidas);
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        throw new NotImplementedException();
    }
}