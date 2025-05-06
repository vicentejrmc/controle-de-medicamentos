using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

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

    private string pastaArmazenamento = "C:\\ArquivosJson";
    private string arquivoArmazenamento = "dados-controle-de-medicamentos.json";

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
            Carregar();
    }

    private void Carregar()
    {
        string caminho = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

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

    public void Salvar()
    {
        string caminho = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento)) 
            Directory.CreateDirectory(pastaArmazenamento); 
        File.WriteAllText(caminho, json);
    }

    // exportar arquivos para csv
    public void ExportarParaCsv()
    {
        string caminho = Path.Combine(pastaArmazenamento, "dados-controle-de-medicamentos.csv");

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        using StreamWriter exportar = new StreamWriter(caminho);
        exportar.WriteLine("Tipo,ID,Nome,Descrição,Qtd Estoque, CNPJ, Forncedor, Telefone Fornecedor");

        foreach (Medicamento med in Medicamentos)
        {
            string tipo = med.GetType().Name;
            string id = med.Id.ToString();
            string nome = med.NomeMedicamento;
            string descricao = med.Descricao;
            string qtdEstoque = med.Quantidade.ToString();
            string cnpj = med.Fornecedor.CNPJ;
            string fornecedor = med.Fornecedor.Nome;
            string telefoneFornecedor = med.Fornecedor.Telefone;
            exportar.WriteLine($"{tipo},{id},{nome},{descricao},{qtdEstoque},{cnpj},{fornecedor},{telefoneFornecedor}");
        }
    }
}

    public void ExportarParaPDF(List<Medicamento> medicamentos)
    {
        string nomeArquivo = $"medicamentos_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

        using (var fs = new FileStream(nomeArquivo, FileMode.Create, FileAccess.Write))
        {
            var writer = new PdfWriter(fs);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            // Título
            var titulo = new Paragraph($"Lista de Medicamentos - {DateTime.Now:dd/MM/yyyy}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(titulo);

            // Tabela
            var tabela = new Table(UnitValue.CreatePercentArray(new float[] { 1, 3, 4, 2, 3, 3, 3 }))
                .UseAllAvailableWidth();

            string[] cabecalhos = { "Id", "Nome", "Descrição", "Qtd. Estoque", "CNPJ Fornecedor", "Nome Fornecedor", "Telefone Fornecedor" };
            foreach (var cabecalho in cabecalhos)
                tabela.AddHeaderCell(cabecalho);

            foreach (var m in medicamentos)
            {
                var cor = m.Quantidade < 5 ? ColorConstants.RED : ColorConstants.BLACK;
                tabela.AddCell(new Paragraph(m.Id.ToString()).SetFontColor(cor));
                tabela.AddCell(new Paragraph(m.NomeMedicamento).SetFontColor(cor));
                tabela.AddCell(new Paragraph(m.Descricao).SetFontColor(cor));
                tabela.AddCell(new Paragraph(m.Quantidade.ToString()).SetFontColor(cor));
                tabela.AddCell(new Paragraph(m.Fornecedor.CNPJ).SetFontColor(cor));
                tabela.AddCell(new Paragraph(m.Fornecedor.Nome).SetFontColor(cor));
                tabela.AddCell(new Paragraph(m.Fornecedor.Telefone).SetFontColor(cor));
            }

            document.Add(tabela);

            // Rodapé
            var rodape = new Paragraph($"Gerado em: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Total: {medicamentos.Count}")
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10);
            document.Add(rodape);

            document.Close();
        }
    }
}