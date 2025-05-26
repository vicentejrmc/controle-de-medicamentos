using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using ControleDeMedicamentos.Util;
using System.Text;
using ControleDeMedicamentos.Util;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.Compartilhado;

public class ContextoDados
{
    public List<Fornecedor> Fornecedores { get; set; }
    public List<Paciente> Pacientes { get; set; }
    public List<Funcionario> Funcionarios { get; set; }
    public List<Medicamento> Medicamentos { get; set; }
    public List<RequisicaoEntrada> RequisicaoEntradas { get; set; }
    public List<PrescricaoMedica> PrescricoesMedicas { get; set; }
    public List<RequisicaoSaida> RequisicaoSaidas { get; set; }

    private string pastaArmazenamentoJson = "C:\\ArquivosJson";
    private string arquivoArmazenamentoJson = "dados-controle-de-medicamentos.json";

    public ContextoDados()
    {
        Fornecedores = new List<Fornecedor>();
        Pacientes = new List<Paciente>();
        Funcionarios = new List<Funcionario>();
        Medicamentos = new List<Medicamento>();
        RequisicaoEntradas = new List<RequisicaoEntrada>();
        PrescricoesMedicas = new List<PrescricaoMedica>();
        RequisicaoSaidas = new List<RequisicaoSaida>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            CarregarJson();
    }

    private void CarregarJson()
    {
        string caminho = Path.Combine(pastaArmazenamentoJson, arquivoArmazenamentoJson);

        if (!File.Exists(caminho)) return;

        string json = File.ReadAllText(caminho);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        this.Fornecedores = contextoArmazenado.Fornecedores;
        this.Pacientes = contextoArmazenado.Pacientes;
        this.Funcionarios = contextoArmazenado.Funcionarios;
        this.Medicamentos = contextoArmazenado.Medicamentos;
        this.RequisicaoEntradas = contextoArmazenado.RequisicaoEntradas;
        this.PrescricoesMedicas = contextoArmazenado.PrescricoesMedicas;
        this.RequisicaoSaidas = contextoArmazenado.RequisicaoSaidas;
    }

    public void SalvarJson()
    {
        string caminho = Path.Combine(pastaArmazenamentoJson, arquivoArmazenamentoJson);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamentoJson))
            Directory.CreateDirectory(pastaArmazenamentoJson);
        File.WriteAllText(caminho, json);
    }

    public void ExportarParaCsv(IRepositorio<Medicamento> repositorioMedicamentos)
    {
        string caminho = Path.Combine(pastaArmazenamentoJson, "dados-controle-de-medicamentos.csv");
        if (!Directory.Exists(pastaArmazenamentoJson))
            Directory.CreateDirectory(pastaArmazenamentoJson);
        using StreamWriter exportar = new StreamWriter(caminho);
        exportar.WriteLine("ID,Nome,Descrição,Qtd Estoque,CNPJ,Forncedor,Telefone Fornecedor");
        foreach (var med in Medicamentos)
        {
            string id = med.Id.ToString();
            string nome = med.NomeMedicamento.ToString();
            string descricao = med.Descricao;
            string qtdEstoque = med.Quantidade.ToString();
            string cnpj = med.Fornecedor.CNPJ.ToString();
            string fornecedor = med.Fornecedor.Nome.ToString();
            string telefoneFornecedor = med.Fornecedor.Telefone.ToString();
            exportar.WriteLine($"{id},{nome},{descricao},{qtdEstoque},{cnpj},{fornecedor},{telefoneFornecedor}");
        }
        Notificador.ExibirCorDeFonte("Arquivo exportado para: C://ArquivosJson/dados-controle-de-medicamentos.csv", ConsoleColor.Yellow);
        Notificador.ExibirMensagem("Arquivo exportado com sucesso", ConsoleColor.Green);

    }
    public void ExportarParaPDF()
    {
        string caminho = Path.Combine(pastaArmazenamentoJson, "dados-controle-de-medicamentos.pdf");

        // Garante que o diretório existe
        if (!Directory.Exists(pastaArmazenamentoJson))
            Directory.CreateDirectory(pastaArmazenamentoJson);
        using FileStream fs = new FileStream(caminho, FileMode.Create, FileAccess.Write);
        using PdfWriter writer = new PdfWriter(fs);
        using PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        Paragraph titulo = new Paragraph($"Lista de Medicamentos - {DateTime.Now:dd/MM/yyyy}")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(16);
        document.Add(titulo);

        Table tabela = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 2, 3, 3, 3 }))
            .UseAllAvailableWidth();

        string[] cabecalhos = { "Id", "Nome", "Descrição", "Qtd. Estoque", "CNPJ Fornecedor", "Nome Fornecedor", "Telefone Fornecedor" };
        foreach (string cabecalho in cabecalhos)
            tabela.AddHeaderCell(cabecalho);

        foreach (Medicamento med in Medicamentos)
        {
            var cor = med.Quantidade < 20 ? ColorConstants.RED : ColorConstants.BLACK;
            tabela.AddCell(new Paragraph(med.Id.ToString()).SetFontColor(cor));
            tabela.AddCell(new Paragraph(med.NomeMedicamento).SetFontColor(cor));
            tabela.AddCell(new Paragraph(med.Descricao).SetFontColor(cor));
            tabela.AddCell(new Paragraph(med.Quantidade.ToString()).SetFontColor(cor));
            tabela.AddCell(new Paragraph(med.Fornecedor.CNPJ).SetFontColor(cor));
            tabela.AddCell(new Paragraph(med.Fornecedor.Nome).SetFontColor(cor));
            tabela.AddCell(new Paragraph(med.Fornecedor.Telefone).SetFontColor(cor));
        }

        document.Add(tabela);

        Paragraph rodape = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Total: {Medicamentos.Count}")
            .SetTextAlignment(TextAlignment.RIGHT)
            .SetFontSize(10);
        document.Add(rodape);

        document.Close();

        Notificador.ExibirMensagem("Arquivo exportado com sucesso!", ConsoleColor.Green);
    }
}