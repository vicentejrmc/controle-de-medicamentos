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
    public void ImportarCSV(IRepositorio<Medicamento> repositorioMedicamentos, IRepositorio<Fornecedor> repositorioFornecedor)
    {
        List<Medicamento> medicamentosRegistrados = repositorioMedicamentos.SelecionarTodos();
        List<Fornecedor> FornecedoresRegistrados = repositorioFornecedor.SelecionarTodos();

        Console.WriteLine("Digite o Caminho do seu aquivo CSV, Ex: (C:\\Users\\Public)");
        string caminho = Console.ReadLine()!.Trim('"');
        if (!File.Exists(caminho))
        {
            Notificador.ExibirMensagem("Arquivo não encontrado", ConsoleColor.Red);
            return;
        }


        using (var leitor = new StreamReader(caminho, Encoding.UTF8))
        {
            string linha;
            int contadorLinha = 0;

            while((linha = leitor.ReadLine()) != null)
            {
                contadorLinha++;
                if (contadorLinha == 1) continue;

                var dados = ConverterLinhaCSV(linha);

                string[] colunas = dados[0].Split(',');

                if(colunas.Length != 7)
                {
                    Notificador.ExibirMensagem("O arquivo deve ter 7 campos, sendo: \n Id, Nome, Descrição, Quantidade, CNPJ, Nome(fornecedor), Telefone(Fornecedor)", ConsoleColor.Red);
                    return;
                }
                if (!int.TryParse(colunas[0], out int id))
                {
                    Notificador.ExibirMensagem("Id(s) inválido(s), retornando", ConsoleColor.Red);
                    return;
                }
                string nome = colunas[1].Trim();
                string descricao = colunas[2].Trim();
                if (!int.TryParse(colunas[3], out int quantidade) || quantidade < 0)
                {
                    Notificador.ExibirMensagem("Quantidade em estoque inválida, retornando", ConsoleColor.Red);
                    return;
                }

                string CNPJ = colunas[4].Trim();
                string nomeFornecedor = colunas[5].Trim();
                string telefone = colunas[6].Trim();

                if ((nome.Length < 3 || nome.Length > 100) || (descricao.Length < 5 || descricao.Length > 255) || !Regex.IsMatch(CNPJ, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$") || !Regex.IsMatch(telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
                {
                    Notificador.ExibirMensagem("Dados Inválidos, retornando", ConsoleColor.Red);
                    return;
                }

                int idFornecedor = 0;
                if(FornecedoresRegistrados.Count == 0)
                {
                    Fornecedor fornecedor = new Fornecedor(nomeFornecedor, CNPJ, telefone);
                    repositorioFornecedor.CadastrarRegistro(fornecedor);
                    idFornecedor = fornecedor.Id;
                    
                } else
                {
                    foreach (var i in FornecedoresRegistrados)
                    {
                        if (CNPJ == i.CNPJ)
                        {
                            i.Nome = nomeFornecedor;
                            i.Telefone = telefone;
                            idFornecedor = i.Id;
                            break;
                        }
                        else
                        {
                            Fornecedor fornecedor = new Fornecedor(nomeFornecedor, CNPJ, telefone);
                            repositorioFornecedor.CadastrarRegistro(fornecedor);
                            idFornecedor = fornecedor.Id;
                            break;
                        }
                    }
                }
                    
                Fornecedor fornecedor1 = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);
                
                if(medicamentosRegistrados.Count == 0)
                {
                    Medicamento medicamento = new Medicamento(nome, descricao, fornecedor1, quantidade);
                    repositorioMedicamentos.CadastrarRegistro(medicamento);
                    medicamento.Id = id;
                } else
                {
                    foreach (var i in medicamentosRegistrados)
                    {
                        if (id == i.Id)
                        {
                            i.NomeMedicamento = nome;
                            i.Descricao = descricao;
                            i.Quantidade = quantidade;
                            break;
                        }
                        else if (nome == i.NomeMedicamento)
                        {
                            Console.WriteLine();
                            i.AdicionarEstoque(quantidade);
                            Notificador.ExibirCorDeFonte($"Já possuímos o remédio {nome}, sua quantidade de {quantidade} foi adicionada ao nosso estoque de {i.Quantidade - quantidade} \n" +
                                $"Agora o estoque é de {i.Quantidade}", ConsoleColor.DarkCyan);
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Medicamento medicamento = new Medicamento(nome, descricao, fornecedor1, quantidade);
                            repositorioMedicamentos.CadastrarRegistro(medicamento);
                            break;
                        }
                    }
                }
            }
            Notificador.ExibirMensagem("Arquivo importado com sucesso", ConsoleColor.Green);
        }
    }

    private static string[] ConverterLinhaCSV(string linha)
    {
        List<string> dados = new List<string>();
        var dadoAtual = new StringBuilder();
        bool entreAspas = false;

        for (int i = 0; i < linha.Length; i++)
        {
            char c = linha[i];

            if(c == '"' && (i == 0 || linha[i - 1] != '\\'))
            {
                entreAspas = true;

            } else if (c == ',' && entreAspas == true)
            {
                dados.Add(dadoAtual.ToString().Trim());
                dadoAtual.Clear();
            } else
            {
                dadoAtual.Append(c);
            }
        }

        dados.Add(dadoAtual.ToString().Trim());
        return dados.ToArray();
    }
}