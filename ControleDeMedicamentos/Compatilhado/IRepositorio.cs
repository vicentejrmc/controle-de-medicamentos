using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Compatilhado
{
    public interface IRepositorio<T> where T : EntidadeBase<T>
    {
        public void CadastrarRegistro(T novoRegistro);

        public bool EditarRegistro(int idRegistro, T registroEditado);

        public bool ExcluirRegistro(int idRegistro);

        public T SelecionarRegistroPorId(int idRegistro);

        public List<T> SelecionarTodos();
    }
}
