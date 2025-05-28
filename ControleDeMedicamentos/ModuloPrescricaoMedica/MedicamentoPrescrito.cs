using ControleDeMedicamentos.ModuloMedicamento;
using System.Diagnostics.CodeAnalysis;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica
{
    public class MedicamentoPrescrito
    {
        public Guid Id { get; set; }
        public Medicamento Medicamento { get; set; }
        public string Dosagem { get; set; }
        public string Periodo { get; set; }
        public int Quantidade { get; set; }

        [ExcludeFromCodeCoverage]
        public MedicamentoPrescrito() { }

        public MedicamentoPrescrito(Medicamento medicamento, string dosagem, string periodo, int quantidade)
        {
            Id = Guid.NewGuid();
            Medicamento = medicamento;
            Dosagem = dosagem;
            Periodo = periodo;
            Quantidade = quantidade;
        }
    }
}
