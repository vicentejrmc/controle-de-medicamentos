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

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
             "Medicamento Cadastrado!",
             "O Medicamento foi Cadastrado com sucesso!"
                 );

            return View("Notificacao", notificacaoVM);
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id)
        {
            var contextoDados = new ContextoDados(true);
            var repositorioMedicamento = new RepositorioMedicamento(contextoDados);
            var repositorioFornecedor = new RepositorioFornecedor(contextoDados);

            var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(id);

            var fornecedores = repositorioFornecedor.SelecionarTodos();

            var editarVM = new EditarMedicamentoViewModel(
                id,
                medicamentoSelecionado.NomeMedicamento,
                medicamentoSelecionado.Descricao,
                medicamentoSelecionado.Fornecedor.Id,
                fornecedores,
                medicamentoSelecionado.Quantidade
             );

            return View(editarVM);
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id, EditarMedicamentoViewModel editarVM)
        {
            var contextoDados = new ContextoDados(true);
            var repositorioMedicamento = new RepositorioMedicamento(contextoDados);
            var repositorioFornecedor = new RepositorioFornecedor(contextoDados);

            var fornecedores = repositorioFornecedor.SelecionarTodos();

            var medicamentoEditado = editarVM.ParaEntidade(fornecedores);

            var medicamentoOriginal = repositorioMedicamento.SelecionarRegistroPorId(id);

            if(medicamentoEditado.Fornecedor != medicamentoOriginal.Fornecedor)
            {
                medicamentoOriginal.Fornecedor.RemoverMedicamento(medicamentoOriginal);

                medicamentoOriginal.Fornecedor = medicamentoEditado.Fornecedor;
            }

            repositorioMedicamento.EditarRegistro(id, medicamentoEditado);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Medicamento Editado!",
            "O Medicamento foi Editado com sucesso!"
                );

            return View("Notificacao", notificacaoVM);
        }

        [HttpGet("excluir/{id:int}")]
        public IActionResult Excluir([FromRoute] int id)
        {
            var contextoDados = new ContextoDados(true);
            var repositorioMedicamento = new RepositorioMedicamento(contextoDados);
            var repositorioFornecedor = new RepositorioFornecedor(contextoDados);

            var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(id);

            var excluirVM = new ExcluirMedicamentoViewModel(id, medicamentoSelecionado.NomeMedicamento); 

            return View("Excluir", excluirVM);
        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirConfirmado([FromRoute]int id)
        {
            var contextoDados = new ContextoDados(true);
            var repositorioMedicamento = new RepositorioMedicamento(contextoDados);

            repositorioMedicamento.ExcluirRegistro(id);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
             "Medicamento Excluído!",
             "O Medicamento foi excluído com sucesso!"
                 );

            return View("Notificacao", notificacaoVM);
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

            var visualizarVM = new VisualizarMedicamentosViewModel(medicamentos);

            return View(visualizarVM);
        }
    }
}
