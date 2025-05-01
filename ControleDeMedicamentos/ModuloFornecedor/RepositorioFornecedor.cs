using ControleDeMedicamentos.Compatilhado;

namespace ControleDeMedicamentos.ModuloFornecedor;

public class RepositorioFornecedor : RepositorioBasEmArquivo<Fornecedor>, IRepositorioFornecedor
{
    public RepositorioFornecedor(ContextoDados contexto) : base(contexto)
    {
    }

    protected override List<Fornecedor> ObterRegistros()
    {
        return contexto.Fornecedores;
    }
}