using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Compatilhado
{
    public abstract class EntidadeBase<T>
    {
        public int Id { get; set; }

        public abstract string Validar();

        public abstract void AtualizarRegistro(T resgitroEditado);
    }
}
