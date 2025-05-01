using ControleDeMedicamentos.Compatilhado;
using ControleDeMedicamentos.ModuloFuncionario;
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
        private ContextoDados contexto;
        private IRepositorioFuncionario repositorioFuncionario;

        public TelaPrincipal()
        {
            contexto = new ContextoDados(true);
            repositorioFuncionario = new RepositorioFuncionario(contexto);
        }

        public void ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|      Controle de Medicamentos        |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Não implementada ");
            Console.WriteLine("2 - Não implementada");
            Console.WriteLine("4 - Controle de Funcionarios");
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

            else if (opcaoPrincipal == '4')
               return new TelaFuncionario("Funcionario" , repositorioFuncionario);

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
