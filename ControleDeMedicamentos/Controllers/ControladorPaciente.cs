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
            return View("Cadastrar");
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarPaciente(Paciente novoPaciente)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            repositorioPaciente.CadastrarRegistro(novoPaciente);

            ViewBag.Mensagem = $"O Registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!";

            return View("Notificacao");
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult ExibirFormularioEdicaoPaciente([FromRoute]int id) //Maperando parametro de Roda vindo do [HttpGet(.../{id:int})]
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

            return View("Editar", pacienteSelecionado);
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult EditarPaciente([FromRoute] int id, Paciente pacienteAtualizado)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

            ViewBag.Mensagem =  $"O Registro \"{pacienteAtualizado.Nome}\" foi Editado com sucesso!";

            return View("Notificacao");
        }

        [HttpGet("excluir/{id:int}")]  // Sempre que tiver um parametro de roda, ele pode ser usado como argumento do método.
        public IActionResult ExibirFormularioExclusaoPaciente([FromRoute] int id)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

            return View("Excluir", pacienteSelecionado);

        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirPaciente([FromRoute] int id)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            repositorioPaciente.ExcluirRegistro(id);

            ViewBag.Mensagem = $"O registro foi Excluido com sucesso!";

            return View("Notificacao");
        }

        [HttpGet("visualizar")]
        public IActionResult VisualizarPacientes()
        {
            ContextoDados contextoDados = new ContextoDados(true); // true indica que os dados serão carregados quando a função for chamada
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            List<Paciente> pacientes = repositorioPaciente.SelecionarTodos();

            return View("Visualizar", pacientes); // fornece os dados passados como modelo na pagina html '@model'
        }

    }
}
