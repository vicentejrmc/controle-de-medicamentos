using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using ControleDeMedicamentos.Util;
using System.Text;

namespace ControleDeMedicamentos;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // argumentos do programa podem ser passados no CreateBuilder ((args) rede de strings)

        WebApplication app  = builder.Build();

        app.MapGet("/", PaginaInicial); // dado uma rota ele execulta uma ação (' "/" rota incial ', "MetodoCriado")

        app.MapGet("/pacientes/cadastrar", ExibirFormularioCadastroPaciente);
        app.MapPost("/pacientes/cadastrar", CadastrarFabricante);

        app.MapGet("/pacientes/editar/{id:int}", ExibirFormularioEdicaoPaciente);
        app.MapPost("/pacientes/editar/{id:int}", EditarPaciente);

        app.MapGet("/pacientes/visualizar", VisualizarFabricantes);
       

        app.Run(); // Run() funciona como um loop infinito do servidor, enquando o navegador estiver aberto ele funcionara
    }

    static Task ExibirFormularioCadastroPaciente(HttpContext context)
    {
        string conteudo = File.ReadAllText("ModuloPaciente/Html/Cadastrar.html");

        return context.Response.WriteAsync(conteudo);
    }

    static Task CadastrarFabricante(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cartaoSus = context.Request.Form["cartaoSus"].ToString();

        Paciente novoPaciente = new Paciente(nome, telefone, cartaoSus);

        repositorioPaciente.CadastrarRegistro(novoPaciente);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O Registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!");

        string conteudoSting = sb.ToString();

        return context.Response.WriteAsync(conteudoSting);
    }

    static Task ExibirFormularioEdicaoPaciente(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        int id = Convert.ToInt32(context.GetRouteValue("id"));

        Paciente pacienteSelecionado  = repositorioPaciente.SelecionarRegistroPorId(id);

        string conteudo = File.ReadAllText("ModuloPaciente/Html/Editar.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#id#", pacienteSelecionado.Id.ToString());
        sb.Replace("#nome#", pacienteSelecionado.Nome);
        sb.Replace("#telefone#", pacienteSelecionado.Telefone);
        sb.Replace("#cartaoSus#", pacienteSelecionado.CartaoSUS);

        string conteudoString = sb.ToString();

        return context.Response.WriteAsync(conteudoString);
    }

    static Task EditarPaciente(HttpContext context)
    {
        int id = Convert.ToInt32(context.GetRouteValue("id"));

        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        string nome = context.Request.Form["nome"].ToString();
        string telefone = context.Request.Form["telefone"].ToString();
        string cartaoSus = context.Request.Form["cartaoSus"].ToString();

        Paciente pacienteAtualizado = new Paciente(nome, telefone, cartaoSus);

        repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

        string conteudo = File.ReadAllText("Compartilhado/Html/Notificacao.html");

        StringBuilder sb = new StringBuilder(conteudo);

        sb.Replace("#mensagem#", $"O Registro \"{pacienteAtualizado.Nome}\" foi Editado com sucesso!");

        string conteudoSting = sb.ToString();

        return context.Response.WriteAsync(conteudoSting);
    }

    static Task PaginaInicial(HttpContext context)
    {
        string conteudo = File.ReadAllText("Compartilhado/Html/PaginaInicial.html"); // O ReadAllText lê o testo e passa para uma string

        return context.Response.WriteAsync(conteudo);   // Retorna a resposta
    }

    static Task VisualizarFabricantes(HttpContext context)
    {
        ContextoDados contextoDados = new ContextoDados(true); // true indica que os dados serão carregados quando a função for chamada
        IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

        string conteudo = File.ReadAllText("ModuloPaciente/Html/Visualizar.html");

        StringBuilder stringBuilder = new StringBuilder(conteudo); // stringBuilder recebendo o conteudo 

        List<Paciente> paciente = repositorioPaciente.SelecionarTodos();

        foreach (Paciente p in paciente)
        {
            string intemLista = $"<li>{p.ToString()} <a href=\"/pacientes/editar/{p.Id}\">Editar</a> / <a href=\"/pacientes/excluir/{p.Id}\">Excluir</a> </li> #paciente#";

            stringBuilder.Replace("#paciente#", intemLista);
        }

        stringBuilder.Replace("#paciente#", "");

        string conteudoString = stringBuilder.ToString();

        return context.Response.WriteAsync(conteudoString);
    }
}