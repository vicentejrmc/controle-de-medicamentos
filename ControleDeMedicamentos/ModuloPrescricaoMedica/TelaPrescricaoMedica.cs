using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    public IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    public IRepositorioMedicamento repositorioMedicamento;

    public TelaPrescricaoMedica(IRepositorioMedicamento repositorioMedicamento, IRepositorioPrescricaoMedica repositorioPrescricaoMedica) : base("PrescricaoMedica", repositorioPrescricaoMedica)
    {
        this.repositorioMedicamento = repositorioMedicamento;
        this.repositorioPrescricaoMedica = repositorioPrescricaoMedica;
    }

    public override PrescricaoMedica ObterDados()
    {
        Console.Write("Digite o CRM do Medico: ");
        string crm = Console.ReadLine()! ?? string.Empty;

        Console.Write("Digite a data(dd/MM/yyyy): ");
        DateTime data = DateTime.Parse(Console.ReadLine()! ?? string.Empty);

        Console.WriteLine("Quantos MedicamentosDaPrescricao diferentes sao?");
        int quantidadeMedicamentos = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        if (quantidadeMedicamentos < medicamentos.Count)
        {
            Notificador.ExibirMensagem($"Erro! A quantidade de medicamentos é insuficiente para uma prescrição médcia de {quantidadeMedicamentos}.", ConsoleColor.Red);
            return null!;
        }

        for (int i = 0; i < quantidadeMedicamentos; i++)
        {
            VisualizarMedicamentos();

            Console.Write("Digite o id do Medicamento: ");
            int idMedicamento = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

            if (idMedicamento == 0)
            {
                Notificador.ExibirMensagem("Erro! O id do medicamento não pode ser 0.", ConsoleColor.Red);
                i--;
                continue;
            }

            Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idMedicamento);

            if (medicamento == null)
            {
                Notificador.ExibirMensagem("Medicamento não encontrado!", ConsoleColor.Red);
                i--;
                continue;
            }
        }

        if (medicamentos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum medicamento foi selecionado!", ConsoleColor.Red);
            return null!;
        }

        PrescricaoMedica prescricaoMedica = new PrescricaoMedica(data, medicamentos, crm);

        return prescricaoMedica;
    }
    public void VisualizarMedicamentos()
    {
        ExibirCabecalho();
        Console.WriteLine("Visualizando MedicamentosDaPrescricao");
        Console.WriteLine("\n--------------------------------------------");

        List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

        Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}",
            "ID", "Nome", "Quantidade", "Fornecedor", "Descrição");

        foreach (Medicamento med in medicamentos)
        {
            Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}",
               med.Id, med.NomeMedicamento, med.QtdEstoque, med.Fornecedor.Nome, med.Descrição);
        }

        Notificador.ExibirMensagem("Pressione qualquer tecla para continuar...", ConsoleColor.Yellow);
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        Console.WriteLine();

        Console.WriteLine("Visualizando Fornecedores...");
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
                p.Id, p.Data.ToString(), p.CRMMEdico
            );
            Console.Write("MedicamentosDaPrescricao: ");
            int i = 0;
            foreach (var m in p.MedicamentosDaPrescricao)
            {
                if (i == 0)
                {
                    Console.Write(m.NomeMedicamento + ", ");
                    i++;
                    continue;
                }
                Console.Write(m.NomeMedicamento + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine();

        Notificador.ExibirMensagem("Pressione ENTER para continuar...", ConsoleColor.DarkYellow);
    }
}