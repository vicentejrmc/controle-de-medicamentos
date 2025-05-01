using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Compatilhado
{
    public class ContextoDados
    {


        private string pastaArmazenamento = "C:\\ArquivosJson";
        private string arquivoArmazenamento = "dados.json";

        public ContextoDados()
        {

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
