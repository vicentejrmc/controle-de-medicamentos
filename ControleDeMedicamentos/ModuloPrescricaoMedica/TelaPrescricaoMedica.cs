using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    public IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    public IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioFornecedor repositorioFornecedor;

    public TelaPrescricaoMedica(IRepositorioMedicamento repositorioMedicamento, IRepositorioPrescricaoMedica repositorioPrescricaoMedica) : base("Prescrição Médica", repositorioPrescricaoMedica)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPrescricaoMedica = repositorioPrescricaoMedica;
    }

    public void ApresentarMenuPrescricaoMedica()
    {
        ExibirCabecalho();

        Console.WriteLine("[1] Cadastrar Prescrição Médica.");
        Console.WriteLine("[2] Visualizar Prescrições Médicas.");
        Console.WriteLine("[S] Voltar");

        Console.Write("\nEscolha uma das opções: ");
        string opcao = Console.ReadLine() ?? string.Empty;
        if (opcao.Length > 0)
        {
            char operacaoEscolhida = Convert.ToChar(opcao[0]);

            if (operacaoEscolhida == '1')
                CadastrarRegistro();

            else if (operacaoEscolhida == '2')
                VisualizarRegistros(false);         
        }
        else
        {
            Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);
            ApresentarMenuPrescricaoMedica();
        }
        if(opcao != "1" || opcao != "2" || opcao.ToUpper() != "S")
        {
            Notificador.ExibirMensagem("Opção inválida! Tente novamente.", ConsoleColor.Red);
            ApresentarMenuPrescricaoMedica();
        }
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.Write("Digite o CRM do Médico: ");
        string crm = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite a data(dd/MM/yyyy): ");
        string datastring = Console.ReadLine()!;
        DateTime? data = Convertor.ConverterStringParaDate(datastring);
        if (data == null) return null;

        Console.Write("Quantos Medicamentos da prescriçõo diferentes teram? ");
        int quantidadeMedicamentos = Convertor.ConverterStringParaInt();
        if (quantidadeMedicamentos == 0) return null;

        List<Medicamento> medicamentosSelecionados = new List<Medicamento>();
        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        if (quantidadeMedicamentos > medicamentos.Count)
        {
            Notificador.ExibirMensagem($"Erro! A quantidade de medicamentos é insuficiente para uma prescrição médica de {quantidadeMedicamentos}.", ConsoleColor.Red);
            return null!;
        }
        if(quantidadeMedicamentos > 5)
        {
            Notificador.ExibirMensagem($"Erro! A quantidade de medicamentos é maior que 5, o máximo permitido.", ConsoleColor.Red);
            return null!;
        }
        if(quantidadeMedicamentos < 1)
        {
            Notificador.ExibirMensagem($"Erro! A quantidade de medicamentos é menor que 1, o mínimo permitido.", ConsoleColor.Red);
            return null!;
        }

        for (int i = 0; i < quantidadeMedicamentos; i++)
        {
            TelaMedicamento telaMedicamento = new TelaMedicamento(repositorioMedicamento, repositorioFornecedor);
            telaMedicamento.VisualizarRegistros(false);

            Console.Write("Digite o id do Medicamento: ");
            int idMedicamento = Convertor.ConverterStringParaInt();
            if (idMedicamento == 0) return null;

            Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

            if (medicamento == null)
            {
                Notificador.ExibirMensagem("Medicamento não encontrado!", ConsoleColor.Red);
                i--;
                continue;
            }
            if (medicamento.Quantidade == 0)
            {
                Notificador.ExibirMensagem("Medicamento sem estoque!", ConsoleColor.Red);
                i--;
                continue;
            }
                medicamentosSelecionados.Add(medicamento);
        }

        PrescricaoMedica prescricaoMedica = new PrescricaoMedica(data, medicamentosSelecionados, crm);

        return prescricaoMedica;
    }
    public override void VisualizarRegistros(bool exibirTitulo)
    {
        Console.WriteLine();

        Console.WriteLine("Visualizando Prescrições Médicas...");
        Console.WriteLine("--------------------------------------------");

        Console.WriteLine();

        Console.WriteLine(
            "{0, -10} | {1, -20} | {2, -20}",
            "Id", "Data", "CRM do Medico"
        );

        List<PrescricaoMedica> registros = repositorioPrescricaoMedica.SelecionarTodos();
        foreach (var p in registros)
        {
            Console.WriteLine(
                "{0, -10} | {1, -20} | {2, -20}",
                p.Id, p.Data.ToString("dd/MM/yyyy"), p.CRMMedico
            );
            Console.Write("Medicamentos da prescrição: {");
            int i = 0;
            foreach (var m in p.MedicamentosDaPrescricao)
            {
                i++;
                if (i < p.MedicamentosDaPrescricao.Count && p.MedicamentosDaPrescricao.Count > 1)
                {
                    Console.Write(m.NomeMedicamento + ", ");
                    continue;
                }
                Console.Write(m.NomeMedicamento);
            }
            Console.WriteLine("}\n");
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }
}