using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.Controllers
{
    [Route("funcionarios")]

    public class ControladorFuncionario : Controller
    {
        [HttpGet("visualizar")]
        public IActionResult VisualizarFuncionarios()
        {
            ContextoDados contextoDados = new ContextoDados(true); // true indica que os dados serão carregados quando a função for chamada
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

            List<Funcionario> funcionario = repositorioFuncionario.SelecionarTodos();

            VisualizarFuncionarioViewModel visualizarVM = new VisualizarFuncionarioViewModel(funcionario);

            return View("Visualizar", visualizarVM); // fornece os dados passados como modelo na pagina html '@model'
        }
    }
}
