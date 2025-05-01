using ControleDeMedicamentos.Compatilhado;

namespace ControleDeMedicamentos;

public class Program
{
    public static void Main(string[] args)
    {
        TelaPrincipal telaPrincipal = new TelaPrincipal();


        while (true)
        {
            telaPrincipal.ApresentarMenuPrincipal();

            ITelaCrud telaSelecionada = telaPrincipal.ObterTela();

            if(telaSelecionada == null) return; // Temporario Caso "Opção invalida / null" sistema irá fechar.

            char opcaoEscolhida = telaSelecionada.ApresentarMenu();

            switch (opcaoEscolhida)
            {
                case '1': telaSelecionada.InserirRegistro(); break;
                case '2': telaSelecionada.EditarRegistro(); break;
                case '3': telaSelecionada.ExcluirRegistro(); break;
                case '4': telaSelecionada.VisualizarRegistros(); break;

                default: break;
            }
        }
    }
}