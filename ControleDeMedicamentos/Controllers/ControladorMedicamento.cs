using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.Util;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.Controllers
{
    [Route("medicamentos")]
    public class ControladorMedicamento : Controller
    {
// cadastrando objeto com Dependencias. Note as diferenças no ViewModelMedicamentos para os outros sem dependencias
        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var contextoDados = new ContextoDados(true);
            var repositorioFornecedor = new RepositorioFornecedor(contextoDados);

            var fornecedores = repositorioFornecedor.SelecionarTodos();

            var cadastrarVM = new CadastrarMedicamentoViewModel(fornecedores);

            return View(cadastrarVM);
        }

// Note que os dois métodos para cadastro tem o mesmo nome.
// Mebora o Compilador C# não aceite metodos com o mesmo nome, ele permite isso quando os métdos passam
// parametros de entrada diferentes, dessa forma ele consegue diferencia-los
        [HttpPost("cadastrar")]       // direnetça entre métdos(dados vindos do formulario 'cadastrarVM')
        public IActionResult Cadastrar(CadastrarMedicamentoViewModel cadastrarVM) 
        {
            var contextoDados = new ContextoDados(true);
            var repositorioMedicamento = new RepositorioMedicamento(contextoDados);
            var repositorioFornecedor = new RepositorioFornecedor(contextoDados);

            var fornecedor = repositorioFornecedor.SelecionarTodos();

            Medicamento novoMedicamento = cadastrarVM.ParaEntidade(fornecedor);

            repositorioMedicamento.CadastrarRegistro(novoMedicamento);

            ViewBag.Mensagem = $"O Medicamento {novoMedicamento.NomeMedicamento} foi cadastraro com sucesso!";

            return View("Notificacao");
        }




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
