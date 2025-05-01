using ControleDeMedicamentos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ModuloMedicamento
{
    public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
    {
        public IRepositorioMedicamento repositorioMedicamento;

        public TelaMedicamento(IRepositorioMedicamento repositorioMedicamento) : base("Medicamento", repositorioMedicamento)
        {
            this.repositorioMedicamento = repositorioMedicamento;
        }

        public override Medicamento ObterDados()
        {
            throw new NotImplementedException();
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            throw new NotImplementedException();
        }
    }
}
