using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFuncionario;
using Microsoft.AspNetCore.Mvc;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ControleDeMedicamentos.Controllers;

[Route("funcionarios")]
public class ControladorFuncionario : Controller
{
    [HttpGet("cadastrar")]
    public IActionResult ExibirFormCadastrarFuncionario()
    {
        CadastrarFuncionarioViewModel cadastrarVM = new CadastrarFuncionarioViewModel();

        return View("cadastrar", cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult CadastrarFuncionario(CadastrarFuncionarioViewModel cadastrarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario novoFuncionario = cadastrarVM.ParaEntidade();

        repositorioFuncionario.CadastrarRegistro(novoFuncionario);

       NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
        "Funcionario Cadastrado!",
        "O Funcionário foi Cadastrado com sucesso!"
            );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("editar/{id:int}")]
    public IActionResult ExibirFormEditarFuncionario([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        EditarFuncionarioViewModel editarVM = new EditarFuncionarioViewModel(
            funcionarioSelecionado.Id,
            funcionarioSelecionado.Nome!,
            funcionarioSelecionado.CPF!,
            funcionarioSelecionado.Telefone!
            );

        return View("Editar", editarVM);
    }

    [HttpPost("editar/{id:int}")]
    public IActionResult EditarFuncionario([FromRoute] int id, EditarFuncionarioViewModel editarVM)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioEditado = new Funcionario(editarVM.Nome, editarVM.CPF, editarVM.Telefone);

        repositorioFuncionario.EditarRegistro(id, funcionarioEditado);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
        "Funcionario Editado!",
        "O Funcionário foi Editado com sucesso!"
            );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("excluir/{id:int}")]
    public IActionResult ExibirFormExcluirFuncionario([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistroPorId(id);

        ExcluirFuncionarioViewModel excluirVM = new ExcluirFuncionarioViewModel(
            funcionarioSelecionado.Id,
            funcionarioSelecionado.Nome
            );

        return View("excluir", excluirVM);
    }

    [HttpPost("excluir/{id:int}")]
    public IActionResult ExcluirFuncionario([FromRoute] int id)
    {
        ContextoDados contextoDados = new ContextoDados(true);
        IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario(contextoDados);

        repositorioFuncionario.ExcluirRegistro(id);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
        "Funcionario Excluído!",
        "O Funcionário foi Excluído com sucesso!"
            );

        return View("Notificacao", notificacaoVM);
    }

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
