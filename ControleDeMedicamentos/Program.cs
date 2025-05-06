using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using ControleDeMedicamentos.Util;

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

            if (telaSelecionada is TelaRequisicaoEntrada)
            {
                TelaRequisicaoEntrada telaRequisicaoEntrada = (TelaRequisicaoEntrada)telaSelecionada;
                telaRequisicaoEntrada.ApresentarMenuRequisicaoEntrada();
                continue;
            }
            if(telaSelecionada is TelaPrescricaoMedica)
            {
                TelaPrescricaoMedica telaPrescricaoMedica = (TelaPrescricaoMedica)telaSelecionada;
                telaPrescricaoMedica.ApresentarMenuPrescricaoMedica();
                continue;
            }
            if(telaSelecionada is TelaRequisicaoSaida)
            {
                TelaRequisicaoSaida telaRequisicaoSaida = (TelaRequisicaoSaida)telaSelecionada;
                telaRequisicaoSaida.ApresentarMenuSaida();
                continue;
            }
            if(telaSelecionada is TelaMedicamento)
            {
                TelaMedicamento telaMedicamento = (TelaMedicamento)telaSelecionada;
                telaMedicamento.ApresentarMenuMedicamentos();
                continue;
            }
            if (telaSelecionada is null)
                break;

            char opcaoEscolhida = telaSelecionada.ApresentarMenu();

            switch (opcaoEscolhida)
            {
                case '1': telaSelecionada.CadastrarRegistro(); break;
                case '2': telaSelecionada.EditarRegistro(); break;
                case '3': telaSelecionada.ExcluirRegistro(); break;
                case '4': telaSelecionada.VisualizarRegistros(true); break;

                default: break;
            }
        }
    }
}