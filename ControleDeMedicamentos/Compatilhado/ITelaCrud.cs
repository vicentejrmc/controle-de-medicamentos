namespace ControleDeMedicamentos.Compatilhado;

public interface ITelaCrud
{
    void InserirRegistro();
    void VisualizarRegistros(bool titulo);
    void EditarRegistro();
    void ExcluirRegistro();
    char ApresentarMenu();
}