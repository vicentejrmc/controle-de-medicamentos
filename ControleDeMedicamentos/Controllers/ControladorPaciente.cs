using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloPaciente;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ControleDeMedicamentos.Controllers
{
    [Route("pacientes")]

    public class ControladorPaciente : Controller
    {
        [HttpGet("cadastrar")]
        public IActionResult ExibirFormularioCadastroPaciente()
        {
            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/cadastrar.html");

            return Content(conteudo, "text/html");
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarPaciente()
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            string nome = HttpContext.Request.Form["nome"].ToString();
            string telefone = HttpContext.Request.Form["telefone"].ToString();
            string cartaoSus = HttpContext.Request.Form["cartaoSus"].ToString();

            Paciente novoPaciente = new Paciente(nome, telefone, cartaoSus);

            repositorioPaciente.CadastrarRegistro(novoPaciente);

            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#mensagem#", $"O Registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!");

            string conteudoSting = sb.ToString();

            return Content(conteudoSting, "text/html");
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult ExibirFormularioEdicaoPaciente()
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            int id = Convert.ToInt32(HttpContext.GetRouteValue("id"));

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/Editar.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#id#", pacienteSelecionado.Id.ToString());
            sb.Replace("#nome#", pacienteSelecionado.Nome);
            sb.Replace("#telefone#", pacienteSelecionado.Telefone);
            sb.Replace("#cartaoSus#", pacienteSelecionado.CartaoSUS);

            string conteudoString = sb.ToString();

            return Content(conteudoString, "text/html");
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult EditarPaciente()
        {
            int id = Convert.ToInt32(HttpContext.GetRouteValue("id"));

            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            string nome = HttpContext.Request.Form["nome"].ToString();
            string telefone = HttpContext.Request.Form["telefone"].ToString();
            string cartaoSus = HttpContext.Request.Form["cartaoSus"].ToString();

            Paciente pacienteAtualizado = new Paciente(nome, telefone, cartaoSus);

            repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#mensagem#", $"O Registro \"{pacienteAtualizado.Nome}\" foi Editado com sucesso!");

            string conteudoSting = sb.ToString();

            return Content(conteudoSting, "text/html");
        }

        [HttpGet("excluir/{id:int}")]
        public IActionResult ExibirFormularioExclusaoPaciente()
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            int id = Convert.ToInt32(HttpContext.GetRouteValue("id"));

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/Excluir.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#id#", id.ToString());
            sb.Replace("#paciente#", pacienteSelecionado.Nome);

            string conteudoString = sb.ToString();

            return Content(conteudoString, "text/html");

        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirPaciente()
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            int id = Convert.ToInt32(HttpContext.GetRouteValue("id"));

            repositorioPaciente.ExcluirRegistro(id);

            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#mensagem#", $"O registro foi Excluido com sucesso!");

            string conteudoString = sb.ToString();

            return Content(conteudoString, "text/html");
        }

        [HttpGet("visualizar")]
        public IActionResult VisualizarPacientes()
        {
            ContextoDados contextoDados = new ContextoDados(true); // true indica que os dados serão carregados quando a função for chamada
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/Visualizar.html");

            StringBuilder stringBuilder = new StringBuilder(conteudo); // stringBuilder recebendo o conteudo 

            List<Paciente> paciente = repositorioPaciente.SelecionarTodos();

            foreach (Paciente p in paciente)
            {
                string intemLista = $"<li>{p.ToString()} <a href=\"/pacientes/editar/{p.Id}\">Editar</a> / <a href=\"/pacientes/excluir/{p.Id}\">Excluir</a> </li> #paciente#";

                stringBuilder.Replace("#paciente#", intemLista);
            }

            stringBuilder.Replace("#paciente#", "");

            string conteudoString = stringBuilder.ToString();

            return Content(conteudoString, "text/html");
        }

    }
}
