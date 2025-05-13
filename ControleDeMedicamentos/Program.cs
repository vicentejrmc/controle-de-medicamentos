using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using ControleDeMedicamentos.Util;

namespace ControleDeMedicamentos;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // argumentos do programa podem ser passados no CreateBuilder ((args) rede de strings)

        WebApplication app  = builder.Build(); 

        app.MapGet("/", PaginaInicial); // dado uma rota ele execulta uma ação (' "/" rota incial ', "MetodoCriado")

        app.MapGet("/pacientes/visualizar", VisualizarFabricantes);

        app.Run(); // Run() funciona como um loop infinito do servidor, enquando o navegador estiver aberto ele funcionara
    }

    static Task VisualizarFabricantes(HttpContext context)
    {
        throw new NotImplementedException();
    }

    static Task PaginaInicial(HttpContext context)
    {
        string conteudo = File.ReadAllText("Html/PaginaInicial.html"); // O ReadAllText lê o testo e passa para uma string

        return context.Response.WriteAsync(conteudo);   // Retorna a resposta
    }
}