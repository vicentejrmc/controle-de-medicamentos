using ControleDeMedicamentos.Compartilhado;

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