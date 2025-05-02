using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloFuncionario;

public class RepositorioFuncionario : RepositorioBasEmArquivo<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionario(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Funcionario> ObterRegistros()
    {
        return contexto.Funcionarios;
    }
}