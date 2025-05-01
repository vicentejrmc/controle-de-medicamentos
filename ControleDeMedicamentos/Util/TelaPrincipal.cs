using ControleDeMedicamentos.Compatilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.Util
{
    public class TelaPrincipal
    {
        public char opcaoPrincipal;

        public void ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|            Titulo Projeto            |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Não implementada ");
            Console.WriteLine("2 - Não implementada");
            Console.WriteLine("S - Sair");

            Console.WriteLine();

            EscolherOpcao();
        }

        public ITelaCrud ObterTela()
        {
            if (opcaoPrincipal == 'S')
            {
                Console.WriteLine("\n---------------------");
                Console.WriteLine("Saindo do Sistema....");
                Console.WriteLine("---------------------");
                Thread.Sleep(1500); ;
                Environment.Exit(0);
            }

            //if (opcaoPrincipal == '1')
            //    return Tela;

            //else if (opcaoPrincipal == '2')
            //    return Tela;

            //else if (opcaoPrincipal == '3')
            //    return Tela;

            else
                Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);

            return null;
        }

        private void EscolherOpcao()
        {
            Console.Write("Escolha uma das opções: ");

            opcaoPrincipal = Convert.ToChar(Console.ReadLine().ToUpper());
        }
    }
}
