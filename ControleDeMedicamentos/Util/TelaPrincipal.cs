using ControleDeMedicamentos.Compatilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloPaciente;

namespace ControleDeMedicamentos.Util
{
    public class TelaPrincipal
    {
        private char opcaoPrincipal;
        private ContextoDados contexto;
        private IRepositorioFornecedor repositorioFornecedor;
        private IRepositorioPaciente repositorioPaciente;

        public TelaPrincipal()
        {
            this.contexto = new ContextoDados(true);
            this.repositorioPaciente = new RepositorioPaciente(contexto);
        }
        
        public void ApresentarMenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|      Controle de Medicamentos        |");
            Console.WriteLine("----------------------------------------");

            Console.WriteLine();

            Console.WriteLine("1 - Gestão Fornecedor ");
            Console.WriteLine("2 - Gestão Paciente ");
            Console.WriteLine("3 - Não implementada");
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

            if (opcaoPrincipal == '1')
                return new TelaFornecedor(repositorioFornecedor);

            if (opcaoPrincipal == '2')
                return new TelaPaciente(repositorioPaciente);

            else
                Notificador.ExibirMensagem("Entrada Invalida! vefirique a opção digitada e tente novamente.", ConsoleColor.Red);

            return null!;
        }

        private void EscolherOpcao()
        {
            Console.Write("Escolha uma das opções: ");

            opcaoPrincipal = Convert.ToChar(Console.ReadLine()!.ToUpper());
        }
    }
}