namespace ControleDeMedicamentos.Compatilhado;

namespace ControleDeMedicamentos.Compatilhado
{
    public interface ITelaCrud
    {
        void InserirRegistro();
        void VisualizarRegistros(bool exibirTitulo);
        void EditarRegistro();
        void ExcluirRegistro();
        char ApresentarMenu();
    }
}
