using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class TelaPrescricaoMedica : TelaBase<PrescricaoMedica>, ITelaCrud
{
    public IRepositorioPrescricaoMedica repositorioPrescricaoMedica;

    public TelaPrescricaoMedica(string nomeEntidade, IRepositorio<PrescricaoMedica> repositorio) : base(nomeEntidade, repositorio)
    {
    }

    public override PrescricaoMedica ObterDados()
    {
        throw new NotImplementedException();
    }

    public override void VisualizarRegistros(bool exibirTitulo)
    {
        throw new NotImplementedException();
    }
}