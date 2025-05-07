namespace ControleDeMedicamentos.Compartilhado;
public interface IRepositorio<T> where T : EntidadeBase<T>
{
    public void CadastrarRegistro(T novoRegistro);

    public bool EditarRegistro(int idRegistro, T registroEditado);

    public bool ExcluirRegistro(int idRegistro);

    public T SelecionarRegistroPorId(int idRegistro);
    public List<T> SelecionarTodos();
}