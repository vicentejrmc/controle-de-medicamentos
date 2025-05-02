using System.Text.Json.Serialization;
using System.Text.Json;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;

namespace ControleDeMedicamentos.Compartilhado;

public class ContextoDados
{
    public List<Fornecedor> Fornecedores { get; set; }
    public List<Paciente> Pacientes { get; set; }
    public List<Funcionario> Funcionario { get; set; }
    public List<Medicamento> Medicamentos { get; set; }

    private string pastaArmazenamento = "C:\\ArquivosJson";
    private string arquivoArmazenamento = "dados-controle-de-medicamentos.json";

    public ContextoDados()
    {
        Fornecedores = new List<Fornecedor>();
        Pacientes = new List<Paciente>();
        Funcionario = new List<Funcionario>();
        Medicamentos = new List<Medicamento>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    private void Carregar()
    {
        string caminho = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminho)) return;  // Verificação de existencia de arquivo

        string json = File.ReadAllText(caminho);

        if (string.IsNullOrWhiteSpace(json)) return; // Verificação de arquivo

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        this.Fornecedores = contextoArmazenado.Fornecedores;
        this.Pacientes = contextoArmazenado.Pacientes;
        this.Funcionario = contextoArmazenado.Funcionario;
        this.Medicamentos = contextoArmazenado.Medicamentos;
    }

    public void Salvar()
    {
        string caminho = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        // configuração
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))  // verificação de arquivo
            Directory.CreateDirectory(pastaArmazenamento); // criação caso não exista

        File.WriteAllText(caminho, json); // Escrita.
    }
}

