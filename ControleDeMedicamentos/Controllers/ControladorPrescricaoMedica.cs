using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ConsoleApp.Extensions;
using ControleDeMedicamentos.ConsoleApp.Model;
using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ControleDeMedicamentos.ConsoleApp.Controllers;

[Route("prescricoes-medicas")]
public class ControladorPrescricaoMedica : Controller
{
    private ContextoDados contexto;
    private IRepositorioPrescricaoMedica repositorioPrescricaoMedica;
    private IRepositorioPaciente repositorioPaciente;
    private IRepositorioMedicamento repositorioMedicamento;

    public ControladorPrescricaoMedica()
    {
        contexto = new ContextoDados(true);
        repositorioPrescricaoMedica = new RepositorioPrescricaoMedica(contexto);
        repositorioPaciente = new RepositorioPaciente(contexto);
        repositorioMedicamento = new RepositorioMedicamento(contexto);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var pacientes = repositorioPaciente.SelecionarTodos();
        var medicamentos = repositorioMedicamento.SelecionarTodos();

        CadastrarPrescricaoViewModel cadastrarVM;

        var prescricaoArmazenada = TempData.Peek("Prescricao");

        if (prescricaoArmazenada is null && prescricaoArmazenada is string jsonString)
        {
            cadastrarVM = JsonSerializer.Deserialize<CadastrarPrescricaoViewModel>(jsonString)!;

            cadastrarVM.AdicionarPacientes(pacientes);
            cadastrarVM.AdicionarMedicamentos(medicamentos);

            return View(cadastrarVM);
        }

        cadastrarVM = new CadastrarPrescricaoViewModel(pacientes, medicamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarPrescricaoViewModel cadastrarVM, string acaoSubmit)
    {
        var pacientes = repositorioPaciente.SelecionarTodos();
        var medicamentos = repositorioMedicamento.SelecionarTodos();

        if (TempData.TryGetValue("Prescricao", out var valor) && valor is string jsonString)
        {
            var vmAnterior = JsonSerializer.Deserialize<CadastrarPrescricaoViewModel>(jsonString)!;

            vmAnterior.CrmMedico = cadastrarVM.CrmMedico;
            vmAnterior.PacienteId = cadastrarVM.PacienteId;
            vmAnterior.MedicamentoId = cadastrarVM.MedicamentoId;
            vmAnterior.DosagemMedicamento = cadastrarVM.DosagemMedicamento;
            vmAnterior.PeriodoMedicamento = cadastrarVM.PeriodoMedicamento;
            vmAnterior.QuantidadeMedicamento = cadastrarVM.QuantidadeMedicamento;

            cadastrarVM = vmAnterior;
        }

        if (acaoSubmit == "adicionarMedicamento")
        {
            var medicamentoSelecionado = repositorioMedicamento.SelecionarRegistroPorId(cadastrarVM.MedicamentoId);

            var detalhesMedicamentoPrescritoVM = new DetalhesMedicamentoPrescritoViewModel(
                cadastrarVM.MedicamentoId,
                medicamentoSelecionado.NomeMedicamento,
                cadastrarVM.DosagemMedicamento,
                cadastrarVM.PeriodoMedicamento,
                cadastrarVM.QuantidadeMedicamento
            );

            cadastrarVM.MedicamentosPrescritos.Add(detalhesMedicamentoPrescritoVM);

            TempData["Prescricao"] = JsonSerializer.Serialize(cadastrarVM);

            return RedirectToAction("Cadastrar");
        }

        else if (acaoSubmit == "limpar")
        {
            TempData.Remove("Prescricao");

            return RedirectToAction("Cadastrar");
        }

        else
        {
            var novoRegistro = cadastrarVM.ParaEntidade(pacientes, medicamentos);

            repositorioPrescricaoMedica.CadastrarRegistro(novoRegistro);

            NotificacaoViewModel notificacaoVM = new NotificacaoViewModel(
                "Prescrição Cadastrada!",
                "A prescrição foi criada com sucesso!"
            );

            TempData.Remove("Prescricao");

            return View("Notificacao", notificacaoVM);
        }
    }

    [HttpGet("visualizar")]
    public IActionResult Visualizar()
    {
        var registros = repositorioPrescricaoMedica.SelecionarRegistros();

        var visualizarVM = new VisualizarPrescricoesViewModel(registros);

        return View(visualizarVM);
    }
}
