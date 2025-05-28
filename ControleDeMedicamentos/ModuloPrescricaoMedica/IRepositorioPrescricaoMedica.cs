using ControleDeMedicamentos.Compartilhado;

namespace ControleDeMedicamentos.ModuloPrescricaoMedica;

public interface IRepositorioPrescricaoMedica
{
    public void CadastrarRegistro(PrescricaoMedica novaPrescricao);
    public List<PrescricaoMedica> SelecionarRegistros();
}



 