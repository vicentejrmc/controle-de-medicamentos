using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class PrescricaoMedica
{
    public Guid Id { get; set; }

    public string CrmMedico { get; set; }
    public DateTime DataEmissao { get; set; }
    public Paciente Paciente { get; set; }

    public List<MedicamentoPrescrito> MedicamentoPrescritos { get; set; }

    [ExcludeFromCodeCoverage]
    public PrescricaoMedica() { }

    public PrescricaoMedica(string crmMedico, Paciente paciente, List<MedicamentoPrescrito> medicamentoPrescritos)
    {
        Id = Guid.NewGuid();
        DataEmissao = DateTime.Now;
        CrmMedico = crmMedico;

        Paciente = paciente;
        MedicamentoPrescritos = medicamentoPrescritos;
    }

    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrEmpty(CrmMedico))
            erros += "Erro! O campo CRM do Medico é obrigatório.\n";

        else if (CrmMedico.Length != 6)
            erros += "Erro! O campo CRM do Medico deve ter exatamente 6 caracteres.\n";

        if (MedicamentoPrescritos == null || MedicamentoPrescritos.Count == 0)
            erros += "Erro! O campo Medicamentos da Prescrição é obrigatório.\n";

        return erros;
    }
}