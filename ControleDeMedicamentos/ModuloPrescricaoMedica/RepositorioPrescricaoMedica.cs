using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public class RepositorioPrescricaoMedica : IRepositorioPrescricaoMedica
{
    private ContextoDados contexto;
    private List<PrescricaoMedica> registros = new List<PrescricaoMedica>();

    public RepositorioPrescricaoMedica(ContextoDados contexto)
    {
        this.contexto = contexto;
        registros = contexto.PrescricoesMedicas;
    }

    public void CadastrarRegistro(PrescricaoMedica novaPrescricao)
    {
        registros.Add(novaPrescricao);

        contexto.SalvarJson();
    }

    public List<PrescricaoMedica> SelecionarRegistros()
    {
        return registros;
    }

}