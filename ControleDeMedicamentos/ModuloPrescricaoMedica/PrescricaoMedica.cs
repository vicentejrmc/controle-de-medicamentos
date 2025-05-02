using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
{
    public DateTime Data { get; set; }
    public List<Medicamento> MedicamentosDaPrescricao { get; set; }
    public string CRMMedico { get; set; }
    public PrescricaoMedica()
    {
        MedicamentosDaPrescricao = new List<Medicamento>();
    }
    public PrescricaoMedica(DateTime data, List<Medicamento> medicamentosDaPrescricao, string crmmMedico) : this()
    {
        Data = data;
        MedicamentosDaPrescricao = medicamentosDaPrescricao;
        CRMMedico = crmmMedico;
    }
    public override void AtualizarRegistro(PrescricaoMedica resgitroEditado)
    {
        Data = resgitroEditado.Data;
        MedicamentosDaPrescricao = resgitroEditado.MedicamentosDaPrescricao;
        CRMMedico = resgitroEditado.CRMMedico;
    }

    public override string Validar()
    {
        string erros = "";

        //if (string.IsNullOrEmpty(CRMMedico))
        //    erros += "Erro! O campo CRM do Medico é obrigatório.\n";

        //else if (CRMMedico.Length != 6)
        //    erros += "Erro! O campo CRM do Medico deve ter exatamente 6 caracteres.\n";

        //if (Data == null)
        //    erros += "Erro! O campo Data é obrigatório.\n";

        //else if (!Regex.IsMatch(Data.ToString(), @"^\d{2}/\.\d{2}/\.\d{4}$"))
        //    erros += "Erro! O campo Data deve estar no formato dd/MM/yyyy.\n";

        //if (MedicamentosDaPrescricao == null || MedicamentosDaPrescricao.Count == 0)
        //    erros += "Erro! O campo Medicamentos da Prescrição é obrigatório.\n";

        return erros;
    }
}