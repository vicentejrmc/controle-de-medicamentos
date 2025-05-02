using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class PrescricaoMedica : EntidadeBase<PrescricaoMedica>
{
    public DateTime Data { get; set; }
    public List<Medicamento> MedicamentosDaPrescricao { get; set; }
    public string CRMMEdico { get; set; }
    public PrescricaoMedica()
    {
        MedicamentosDaPrescricao = new List<Medicamento>();
    }
    public PrescricaoMedica(DateTime data, List<Medicamento> medicamentosDaPrescricao, string crmmMedico) : this()
    {
        Data = data;
        MedicamentosDaPrescricao = medicamentosDaPrescricao;
        CRMMEdico = crmmMedico;
    }
    public override void AtualizarRegistro(PrescricaoMedica resgitroEditado)
    {
        Data = resgitroEditado.Data;
        MedicamentosDaPrescricao = resgitroEditado.MedicamentosDaPrescricao;
        CRMMEdico = resgitroEditado.CRMMEdico;
    }

    public override string Validar()
    {
        string erros = "";

        //if (string.IsNullOrEmpty(CRMMedico))
        //    erros += "Erro! O campo CRM do Medico é obrigatório.\n";

        //else if (CRMMedico.Length != 6)
        //    erros += "Erro! O campo CRM do Medico deve ter exatamente 6 caracteres.\n";

        return erros;
    }
}