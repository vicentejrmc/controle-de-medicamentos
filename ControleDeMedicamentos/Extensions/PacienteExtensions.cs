using ControleDeMedicamentos.Models;
using ControleDeMedicamentos.ModuloPaciente;
using iText.StyledXmlParser.Jsoup.Nodes;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace ControleDeMedicamentos.Extensions;

public static class PacienteExtensions
{
    // Método de Extensão, Pega um tipo especifico (this  formulario...)
    // permite a chamada de um método novo sem que seja preiso modificar classe desse tipo
    // Usando a palavra chave this, o Compliador do C# entende que quem vai chamar o método
    // é a instancia que está sendo especificada
    public static Paciente ParaEntidade(this FormularioPacienteViewModel formularioVM)
    {
        return new Paciente(formularioVM.Nome, formularioVM.Telefone, formularioVM.CartaoSus);
    }

    public static DetalhesPacienteViewModel ParaDetalhesVM(this Paciente paciente)
    {
        return new DetalhesPacienteViewModel(
            paciente.Id,
            paciente.Nome,
            paciente.Telefone,
            paciente.CartaoSus
        );
    }
}
