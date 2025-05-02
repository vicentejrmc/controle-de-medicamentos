using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    public IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    public IRepositorioMedicamento repositorioMedicamento;

    public TelaPrescricaoMedica(IRepositorioMedicamento repositorioMedicamento, IRepositorioPrescricaoMedica repositorioPrescricaoMedica) : base("Prescrição Médica", repositorioPrescricaoMedica)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPrescricaoMedica = repositorioPrescricaoMedica;
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

        for (int i = 0; i < quantidadeMedicamentos; i++)
        {
            VisualizarMedicamentos();

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
    public void VisualizarMedicamentos()
    {
        ExibirCabecalho();
        Console.WriteLine("Visualizando Medicamentos da prescriçõo");
        Console.WriteLine("\n--------------------------------------------");

        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}",
            "ID", "Nome", "Quantidade", "Fornecedor", "Descrição");

        foreach (Medicamento med in medicamentos)
        {
            Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}",
               med.Id, med.NomeMedicamento, med.Quantidade, med.Fornecedor.Nome, med.Descrição);
        }

        Notificador.ExibirMensagem("Pressione qualquer tecla para continuar...", ConsoleColor.Yellow);
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

            foreach (var m in p.MedicamentosDaPrescricao)
            {
                if (m.Id < p.MedicamentosDaPrescricao.Count && p.MedicamentosDaPrescricao.Count > 1)
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