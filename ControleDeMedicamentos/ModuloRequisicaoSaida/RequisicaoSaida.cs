using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;

namespace ControleDeMedicamentos.ModuloRequisicaoSaida;

public class RequisicaoSaida : EntidadeBase<RequisicaoSaida>
{
    public DateTime Data { get; set; }
    public int pacienteId { get; set; }
    public int prescricaoMedicaId { get; set; }
    public List<Medicamento> MedicamentosRequisitados { get; set; }  
    public List<string> medicamentosstring { get; set; }
    public List<int> QuantidadeDeMedicamentos{ get; set; }

    public RequisicaoSaida()
    {
        MedicamentosRequisitados = new List<Medicamento>();
        medicamentosstring = new List<string>();
        AdicionarNomeMedicamentos();
        QuantidadeDeMedicamentos = new List<int>();
    }
    public RequisicaoSaida(DateTime? Data, int pacienteId, int prescricaoMedicaId, List<int> QuantidadeDeMedicamentos, List<Medicamento> MedicamentosRequisitados) : this()
    {
        this.Data = (DateTime)Data!;
        this.pacienteId = pacienteId;
        this.prescricaoMedicaId = prescricaoMedicaId;
        this.QuantidadeDeMedicamentos = QuantidadeDeMedicamentos;
        this.MedicamentosRequisitados = MedicamentosRequisitados;
    }

    public override void AtualizarRegistro(RequisicaoSaida resgitroEditado)
    {
        throw new NotImplementedException();
    }

    public override string Validar()
    {
        string erros = "";
        return erros;
    }

    public void AdicionarNomeMedicamentos()
    {
        List<string> strings = new List<string>();
        foreach (var item in MedicamentosRequisitados)
        {
            medicamentosstring.Add(item.NomeMedicamento);
        }
    }
}