using ControleDeMedicamentos.ConsoleApp.Model;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;


namespace ControleDeMedicamentos.ConsoleApp.Extensions;

public static class PrescricaoExtensions
{
    public static PrescricaoMedica ParaEntidade(
      this CadastrarPrescricaoViewModel cadastrarVM,
      List<Paciente> pacientesDisponiveis,
      List<Medicamento> medicamentosDisponiveis
  )
    {
        Paciente pacienteSelecionado = null;

        foreach (var p in pacientesDisponiveis)
        {
            if (p.Id == cadastrarVM.PacienteId)
                pacienteSelecionado = p;
        }

        var registrosSelecionados = new List<MedicamentoPrescrito>();

        foreach (var selecionarMedicamentoVM in cadastrarVM.MedicamentosPrescritos)
        {
            foreach (var medicamentoCadastrado in medicamentosDisponiveis)
            {
                if (selecionarMedicamentoVM.MedicamentoId == medicamentoCadastrado.Id)
                {
                    var registroSelecionado = new MedicamentoPrescrito(
                        medicamentoCadastrado,
                        selecionarMedicamentoVM.Dosagem,
                        selecionarMedicamentoVM.Periodo,
                        selecionarMedicamentoVM.Quantidade
                    );

                    registrosSelecionados.Add(registroSelecionado);
                }
            }
        }

        return new PrescricaoMedica(cadastrarVM.PacienteId ,cadastrarVM.CrmMedico, pacienteSelecionado, registrosSelecionados);
    }

    public static DetalhesPrescricaoViewModel ParaDetalhesVM(this PrescricaoMedica prescricao)
    {
        return new DetalhesPrescricaoViewModel(
            prescricao.Id,
            prescricao.CrmMedico,
            prescricao.Paciente.Nome,
            prescricao.DataEmissao
        );
    }
}
