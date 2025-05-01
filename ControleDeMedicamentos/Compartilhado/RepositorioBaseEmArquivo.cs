namespace ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.Compartilhado
{
public abstract class RepositorioBasEmArquivo<T> where T : EntidadeBase<T>
{
    private List<T> registros = new List<T>();
    private int contadorIds = 0;

    protected ContextoDados contexto;

    protected RepositorioBasEmArquivo(ContextoDados contexto)
    {
        this.contexto = contexto;

        registros = ObterRegistros();

        int maiorId = 0;   // metodo para garantir que o Id virá corretamente ordenado quando recuperarmos os arquivos
        foreach (var registro in registros)
        {
            if (registro.Id > maiorId)
                maiorId = registro.Id;
        }

        contadorIds = maiorId;
    }

    protected abstract List<T> ObterRegistros();

    public void CadastrarRegistro(T novoRegistro)
    {
        novoRegistro.Id = ++contadorIds;

        registros.Add(novoRegistro);

        contexto.Salvar();

    }

    public bool EditarRegistro(int idRegistro, T registroEditado)
    {
        foreach (T entidade in registros)
        {
            if (entidade.Id == idRegistro)
            {
                entidade.AtualizarRegistro(registroEditado);

                contexto.Salvar();

                return true;
            }
        }

        return false;
    }

    public bool ExcluirRegistro(int idRegistro)
    {
        T registroSelecionado = SelecionarRegistroPorId(idRegistro);

        if (registroSelecionado != null)
        {
            registros.Remove(registroSelecionado);

            contexto.Salvar();

            return true;
        }

        return false;
    }

    public T SelecionarRegistroPorId(int idRegistro)
    {
        foreach (T entidade in registros)
        {
            if (entidade.Id == idRegistro)
                return entidade;
        }

        return null!;
    }

    public List<T> SelecionarTodos()
    {
        return registros;
    }
}
}
