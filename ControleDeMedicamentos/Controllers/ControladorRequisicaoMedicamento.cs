using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.Extensions;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloFuncionario;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using Microsoft.AspNetCore.Mvc;


[Route("requisicoes-medicamentos")]
public class ControladorRequisicaoMedicamento : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioRequisicaoEntrada repositorioRequisicaoEntrada;
    private IRepositorioRequisicaoSaida repositorioRequisicaoSaida;
    private IRepositorioFuncionario repositorioFuncionario;
    private IRepositorioMedicamento repositorioMedicamento;
    private IRepositorioPaciente repositorioPaciente;

    public ControladorRequisicaoMedicamento()
    {
        contextoDados = new ContextoDados(true);
        repositorioRequisicaoEntrada = new RepositorioRequisicaoEntrada(contextoDados);
        repositorioFuncionario = new RepositorioFuncionario(contextoDados);
        repositorioMedicamento = new RepositorioMedicamento(contextoDados);
        repositorioPaciente = new RepositorioPaciente(contextoDados);
    }

    [HttpGet("entrada/{medicamentoId:int}")]
    public IActionResult CadastrarEntrada([FromRoute] int medicamentoId)
    {
        var funcionarios = repositorioFuncionario.SelecionarTodos();

        var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(medicamentoId);

        var cadastrarVM = new CadastrarRequisicaoEntradaViewModel(medicamentoId, funcionarios);

        ViewBag.NomeMedicamento = medicamentoSelecionado.NomeMedicamento;

        return View("CadastrarEntrada", cadastrarVM);
    }

    [HttpPost("entrada/{medicamentoId:int}")]
    public IActionResult CadastrarEntrada([FromRoute] int medicamentoId, CadastrarRequisicaoEntradaViewModel cadastrarVM)
    {
        var funcionarios = repositorioFuncionario.SelecionarTodos();
        var medicamentos = repositorioMedicamento.SelecionarTodos();

        var registro = cadastrarVM.ParaEntidade(funcionarios, medicamentos);

        var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(medicamentoId);

        medicamentoSelecionado.AdicionarAoEstoque(registro);

        repositorioRequisicaoEntrada.CadastrarRequisicaoEntrada(registro);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Requisição de Entrada Cadastrada!",
            $"O estoque do medicamento foi atualizado!"
        );

        return View("Notificacao", notificacaoVM);
    }

    [HttpGet("saida/{medicamentoId:int}")]
    public IActionResult CadastrarSaida([FromRoute] int medicamentoId)
    {
        var pacientes = repositorioPaciente.SelecionarTodos();
        var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(medicamentoId);

        var cadastrarVM = new CadastrarRequisicaoSaidaViewModel(medicamentoId, pacientes);

        ViewBag.NomeMedicamento = medicamentoSelecionado.NomeMedicamento;

        return View(cadastrarVM);
    }

    [HttpPost("saida/{medicamentoId:int}")]
    public IActionResult CadastrarSaida([FromRoute] int medicamentoId, CadastrarRequisicaoSaidaViewModel cadastrarVM)
    {
        var pacientes = repositorioPaciente.SelecionarTodos();
        var medicamentos = repositorioMedicamento.SelecionarTodos();

        var registro = cadastrarVM.ParaEntidade(pacientes, medicamentos);

        var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(medicamentoId);

        medicamentoSelecionado.RemoverDoEstoque(registro);

        repositorioRequisicaoSaida.CadastrarRequisicaoSaida(registro);

        NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
            "Requisição de Saída Cadastrada!",
            $"O estoque do medicamento foi atualizado!"
        );

        return View("Notificacao", notificacaoVM);
    }
}