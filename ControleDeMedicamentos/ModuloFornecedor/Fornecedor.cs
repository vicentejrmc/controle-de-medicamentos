using ControleDeMedicamentos.Compatilhado;

namespace ControleDeMedicamentos.ModuloFornecedor;

public class Fornecedor : EntidadeBase<Fornecedor>
{
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public string Telefone { get; set; }

    public Fornecedor()
    {

    }
    public Fornecedor(string nome, string cnpj, string telefone) : this()
    {
        Nome = nome;
        CNPJ = cnpj;
        Telefone = telefone;
    }

    public override string Validar()
    {
        string erro = "";
        return erro;
    }

    public override void AtualizarRegistro(Fornecedor resgitroEditado)
    {
        Nome = resgitroEditado.Nome;
        CNPJ = resgitroEditado.CNPJ;
        Telefone = resgitroEditado.Telefone;
    }
}