using ControleDeMedicamentos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ModuloMedicamento;

public class RepositorioMedicamento : RepositorioBasEmArquivo<Medicamento>, IRepositorioMedicamento
{
    public RepositorioMedicamento(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Medicamento> ObterRegistros()
    {
        return contexto.Medicamentos;
    }
}
