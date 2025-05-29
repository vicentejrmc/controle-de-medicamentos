using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.Controllers;
[Route("fornecedores")]

public class ControladorFornecedor : Controller
{
    [HttpGet("cadastrar")]
    public IActionResult ExibirFormCadastrarFornecedor()
    {
        CadastrarFornecedorViewModel cadastrarVM = new CadastrarFornecedorViewModel();

        return View("cadastrar", cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult CadastrarFornecedor(CadastrarFornecedorViewModel cadastrarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFuncionario = new RepositorioFornecedor(contextoDados);

        Fornecedor novoFornecedor = cadastrarVM.ParaEntidade();

        repositorioFuncionario.CadastrarRegistro(novoFornecedor);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
      "Fornecedor Cadastrado!",
      "O Fornecedor foi cadastrado com sucesso!"
             );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormEditarFornecedor([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        EditarFornecedorViewModel editarVM = new EditarFornecedorViewModel(
              fornecedorSelecionado.Id,
              fornecedorSelecionado.Nome,
              fornecedorSelecionado.CNPJ,
              fornecedorSelecionado.Telefone
            );

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult EditarFornecedor([FromRoute] int id, EditarFornecedorViewModel editarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor fornecedorEditado = new Fornecedor(editarVM.Nome, editarVM.CNPJ, editarVM.Telefone);

        repositorioFornecedor.EditarRegistro(id, fornecedorEditado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
        "Fornecedor Editado!",
        "O Fornecedor foi Editado com sucesso!"
            );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormExcluirFornecedor([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        Fornecedor fornecedorSelecionado = repositorioFornecedor.SelecionarRegistroPorId(id);

        ExcluirFornecedorViewModel excluirVM = new ExcluirFornecedorViewModel(
            fornecedorSelecionado.Id,
            fornecedorSelecionado.Nome
            );

        return View("excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirFornecedor([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        repositorioFornecedor.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
        "Fornecedor Excluido!",
        "O Fornecedor foi Excluido com sucesso!"
            );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("visualizar")]
    public IActionResult VisualizarFornecedor()
    {
        ContextoDados contextoDados = new ContextoDados(true); // true indica que os dados serão carregados quando a função for chamada
        IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor(contextoDados);

        List<Fornecedor> fornecedor = repositorioFornecedor.SelecionarTodos();

        VisualizarFornecedorViewModel visualizarVM = new VisualizarFornecedorViewModel(fornecedor);

        return View("Visualizar", visualizarVM); // fornece os dados passados como modelo na pagina html '@model'
    }
}
