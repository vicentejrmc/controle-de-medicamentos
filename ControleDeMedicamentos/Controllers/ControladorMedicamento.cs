using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloMedicamento;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.Controllers
{
    [Route("medicamentos")]
    public class ControladorMedicamento : Controller
    {

        [HttpGet("visualizar")]
        public IActionResult Visualizar()
        {
            // utilizando o 'var' neste contexto, o Compilador do C# já entende que estamos passado uma variável do tipo
            // determinado na instãncia ' = new ContextoDados'
            var contextoDados = new ContextoDados(true);
            var repositorioMedicamento = new RepositorioMedicamento(contextoDados);

            var medicamentos = repositorioMedicamento.SelecionarTodos(); 
            // nesse contexto o Compliador também entende que se trata de uma lista do objeto que está sendo retornado

            VisualizarMedicamentosViewModel visualizarVM = new VisualizarMedicamentosViewModel(medicamentos);

            return View(visualizarVM);
        }
    }
}
