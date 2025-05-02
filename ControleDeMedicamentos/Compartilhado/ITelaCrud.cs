namespace ControleDeMedicamentos.Compartilhado; 
public interface ITelaCrud
{
    void CadastrarRegistro();
    void EditarRegistro();
    void ExcluirRegistro();
    char ApresentarMenu();
    void VisualizarRegistros(bool v);
}

