using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using ControleDeMedicamentos.ModuloFornecedor;

namespace ControleDeMedicamentos.Compatilhado
{
    public class ContextoDados
    {
        public List<Fornecedor> Fornecedores { get; set; }

        private string pastaArmazenamento = "C:\\ArquivosJson";
        private string arquivoArmazenamento = "dados-controle-de-medicamentos.json";

        public ContextoDados()
        {
            Fornecedores = new List<Fornecedor>();
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
}
