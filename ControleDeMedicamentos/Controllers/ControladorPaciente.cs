using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Extensions;
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

            Paciente novoPaciente = cadastrarVM.ParaEntidade();  // usando o método de estensão,
            // asseguramos de que os dados que forem passados virão da forma correta
            //sem o risco de que sejam passados em uma ordem errada. da qual deve ser instanciada no construtor da entidade

            repositorioPaciente.CadastrarRegistro(novoPaciente);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Paciente Cadastrado!",
            "O Paciente foi Cadastrado com sucesso!"
                );

            return View("Notificacao", notificacaoVM);
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
        public IActionResult EditarPaciente([FromRoute] int id, EditarPacienteViewModel editarVM)
        // O editarVM serve para transferir os dados vindos do formulario para a instância do obejto dentro do método
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteAtualizado = new Paciente(editarVM.Nome, editarVM.Telefone, editarVM.Telefone);

            repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Paciente Editado!",
            "O Paciente foi Editado com sucesso!"
                );

            return View("Notificacao", notificacaoVM);
        }

        [HttpGet("excluir/{id:int}")]  // Sempre que tiver um parametro de rota, ele pode ser usado como argumento do método.
        public IActionResult ExibirFormularioExclusaoPaciente([FromRoute] int id) // parametro de rota passada como argumento.
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

//Utilizando ViewModel, escondemos as informações desnecessarias da nossa Entidade. de forma que o usuario(navegador)
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

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Paciente Excluído!",
            "O Paciente foi excluído com sucesso!"
                );

            return View("Notificacao", notificacaoVM);
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
