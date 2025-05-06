using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
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

    public void ImportarCSV(IRepositorio<Medicamento> repositorioMedicamentos, IRepositorio<Fornecedor> repositorioFornecedor)
    {
        List<Medicamento> medicamentosRegistrados = repositorioMedicamentos.SelecionarTodos();
        List<Fornecedor> FornecedoresRegistrados = repositorioFornecedor.SelecionarTodos();

        Console.WriteLine("Digite o Caminho do seu aquivo CSV, Ex: (C:\\Users\\Public)");
        string caminho = Console.ReadLine()!;
        if (!File.Exists(caminho))
        {
            Console.WriteLine("Arquivo não encontrado.");
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

                if(dados.Length != 7)
                {
                    Notificador.ExibirMensagem("O arquivo deve ter 7 campos, sendo: \n Id, Nome, Descrição, Quantidade, CNPJ, Nome(fornecedor), Telefone(Fornecedor)", ConsoleColor.Red);
                    return;
                }
                if (!int.TryParse(dados[0], out int id))
                {
                    Notificador.ExibirMensagem("Id(s) inválido(s), retornando", ConsoleColor.Red);
                    return;
                }
                string nome = dados[1];
                string descricao = dados[2];
                if (!int.TryParse(dados[3], out int quantidade) || quantidade < 0)
                {
                    Notificador.ExibirMensagem("Quantidade em estoque inválida, retornando", ConsoleColor.Red);
                    return;
                }

                string CNPJ = dados[4];
                string nomeFornecedor = dados[5];
                string telefone = dados[6];

                if ((nome.Length < 3 || nome.Length > 100) || (descricao.Length < 5 || nome.Length > 255) || !Regex.IsMatch(CNPJ, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$") || !Regex.IsMatch(telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
                {
                    Notificador.ExibirMensagem("Dados Inválidos, retornando", ConsoleColor.Red);
                    return;
                }

                int idFornecedor = 0;
                foreach (var i in FornecedoresRegistrados)
                {
                    if(CNPJ == i.CNPJ)
                    {
                        i.Nome = nome;
                        i.Telefone = telefone;
                        idFornecedor = i.Id;
                        break;
                    } else
                    {
                        Fornecedor fornecedor = new Fornecedor(nome, CNPJ, telefone);
                        repositorioFornecedor.CadastrarRegistro(fornecedor);
                        idFornecedor = fornecedor.Id;
                        break;
                    }
                }
                Fornecedor fornecedor1 = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);
                               
                foreach (var i in medicamentosRegistrados)
                {
                    if(id == i.Id)
                    {
                        i.NomeMedicamento = nome;
                        i.Descricao = descricao;
                        i.Quantidade = quantidade;
                        break;
                    } else if (nome == i.NomeMedicamento)
                    {
                        i.AdicionarEstoque(quantidade);
                        Console.WriteLine($"Já possuímos o remédio {nome}, sua quantidade de {quantidade} foi adicionada ao nosso estoque de {i.Quantidade - quantidade} \n" +
                            $"Agora o estoque é de {i.Quantidade}");
                        break;
                    } else
                    {
                        Medicamento medicamento = new Medicamento(nome, descricao, fornecedor1, quantidade);
                        repositorioMedicamentos.CadastrarRegistro(medicamento);
                        break;
                    }
                }
            }
            

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

