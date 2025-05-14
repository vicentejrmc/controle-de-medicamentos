using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Models;
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
            CadastrarPacienteViewModel cadastrarVM = new CadastrarPacienteViewModel();

            // A instância acima 'cadastrarVM' está sendo passada sem nenhuma informação pois são os dados passados para o usuário

            return View("Cadastrar", cadastrarVM);
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarPaciente(CadastrarPacienteViewModel cadastrarVM)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente novoPaciente = new Paciente(
             cadastrarVM.Nome,
             cadastrarVM.Telefone,
             cadastrarVM.CartaoSus
            );

            repositorioPaciente.CadastrarRegistro(novoPaciente);

            ViewBag.Mensagem = $"O Registro \"{cadastrarVM.Nome}\" foi cadastrado com sucesso!";

            return View("Notificacao");
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult ExibirFormularioEdicaoPaciente([FromRoute]int id) //Maperando parametro de Roda vindo do [HttpGet(.../{id:int})]
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

//Utilizando ViewModel, escondemos as informações desnecessarias da nossa Entidade. de form que o usuario(navegador)
//não tenha acesso direto aos dados internos do sistema, melhorando a proteção dos dados da entidade por não expor os nosso Dominio.
            EditarPacienteViewModel editarVM = new EditarPacienteViewModel(
                pacienteSelecionado.Id,
                pacienteSelecionado.Nome,
                pacienteSelecionado.Telefone,
                pacienteSelecionado.CartaoSus
                );

            return View("Editar", editarVM);
        }

        [HttpPost("editar/{id:int}")]
        // O ...VM apenas serve para transferir os dados do formulario para a instancia do obejto dentro do método
        public IActionResult EditarPaciente([FromRoute] int id, EditarPacienteViewModel editarVM) 
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteAtualizado = new Paciente(editarVM.Nome, editarVM.Telefone, editarVM.Telefone);

            repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

            ViewBag.Mensagem =  $"O Registro \"{editarVM.Nome}\" foi Editado com sucesso!";

            return View("Notificacao");
        }

        [HttpGet("excluir/{id:int}")]  // Sempre que tiver um parametro de roda, ele pode ser usado como argumento do método.
        public IActionResult ExibirFormularioExclusaoPaciente([FromRoute] int id) // parametro de rota passada como argumento.
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

//Utilizando ViewModel, escondemos as informações desnecessarias da nossa Entidade. de form que o usuario(navegador)
//não tenha acesso direto aos dados internos do sistema, melhorando a proteção dos dados da entidade por não expor os nosso Dominio.
            ExcluirPacienteViewModel excluirVM = new ExcluirPacienteViewModel(
                pacienteSelecionado.Id, 
                pacienteSelecionado.Nome
            );

            return View("Excluir", excluirVM);

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

            List<Paciente> paciente = repositorioPaciente.SelecionarTodos();

            VisualizarPacientesViewModel visualizarVM =  new VisualizarPacientesViewModel(paciente);

            return View("Visualizar", visualizarVM); // fornece os dados passados como modelo na pagina html '@model'
        }

    }
}
