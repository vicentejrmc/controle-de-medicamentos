namespace ControleDeMedicamentos.Compartilhado;

public interface ITelaCrud
{
    void InserirRegistro();
    void VisualizarRegistros(bool exibirTitulo);
    void EditarRegistro();
    void ExcluirRegistro();
    char ApresentarMenu();
}