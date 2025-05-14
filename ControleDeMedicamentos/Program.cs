using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloMedicamento;
using ControleDeMedicamentos.ModuloPaciente;
using ControleDeMedicamentos.ModuloPrescricaoMedica;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloRequisicaoSaida;
using ControleDeMedicamentos.Util;
using System.Text;

namespace ControleDeMedicamentos;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args); // argumentos do programa podem ser passados no CreateBuilder ((args) rede de strings)

        builder.Services.AddControllersWithViews(); // adiciona supoerte ao nosso servidor para execultar Views()

        WebApplication app  = builder.Build();

        app.UseRouting(); // adiciona supoerte ao nosso servidor para execultar Views()

        app.MapControllers();
       
        app.Run(); // Run() funciona como um loop infinito do servidor, enquando o navegador estiver aberto ele funcionara
    }
}