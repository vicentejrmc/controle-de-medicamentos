using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleDeMedicamentos.Compatilhado;
namespace ControleDeMedicamentos.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CartaoSUS { get; set; }

        public Paciente(string nome, string telefone, string cartaoSUS)
        {
            Nome = nome;
            Telefone = telefone;
            CartaoSUS = cartaoSUS;
        }





        public override void AtualizarRegistro(Paciente resgitroEditado)
        {
            Nome = resgitroEditado.Nome;
            Telefone = resgitroEditado.Telefone;
            CartaoSUS = resgitroEditado.CartaoSUS;
        }

        public override string Validar()
        {
            string erros = "";
            return erros;
        }
    }
}
