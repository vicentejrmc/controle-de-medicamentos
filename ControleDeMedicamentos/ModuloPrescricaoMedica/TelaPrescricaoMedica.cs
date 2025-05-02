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
        Console.Clear();
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("Controle de Prescrições Médicas");
        Console.WriteLine("--------------------------------------------\n");

        Console.WriteLine("1 - Cadastrar Prescrição Médica");
        Console.WriteLine("2 - Visualizar Prescrições Médicas");
        Console.WriteLine();
        Console.WriteLine("S - Sair");
        Console.WriteLine();

        Console.Write("Escolha uma das opções: ");
        char operacaoEscolhida = Convert.ToChar(Console.ReadLine()!.ToUpper());
        if (operacaoEscolhida == '1')
            CadastrarRegistro();
        else if (operacaoEscolhida == '2')
            VisualizarRegistros(false);
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.Write("Digite o CRM do Médico: ");
        string crm = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite a data(dd/MM/yyyy): ");
        DateTime data = DateTime.Parse(Console.ReadLine()! ?? string.Empty);

        Console.Write("Quantos Medicamentos da prescriçõo diferentes teram? ");
        int quantidadeMedicamentos = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

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
            int idMedicamento = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

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

        Console.WriteLine("Visualizando Prescrição Médicas...");
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