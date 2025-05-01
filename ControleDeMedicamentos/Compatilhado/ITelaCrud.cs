using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Compatilhado
{
    public interface ITelaCrud
    {
        void InserirRegistro();
        void VisualizarRegistros();
        void EditarRegistro();
        void ExcluirRegistro();
        char ApresentarMenu();
    }
}
