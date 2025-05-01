namespace ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.Compartilhado
{ 
public interface ITelaCrud
{
    void CadastrarRegistro();
    void VisualizarRegistros(bool exibirTitulo);
    void EditarRegistro();
    void ExcluirRegistro();
    char ApresentarMenu();
}
}
