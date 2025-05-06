using ControleDeMedicamentos.Compartilhado;
using System.Text.RegularExpressions;

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
        string erros = "";

        if (string.IsNullOrEmpty(Nome))
            erros += "Erro! O campo Nome é obrigatório.\n";

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "Erro! O campo Nome deve ter entre 3 e 100 caracteres.\n";

        if (string.IsNullOrEmpty(CNPJ))
            erros += "Erro! O campo CNPJ é obrigatório.\n";

        else if (!Regex.IsMatch(CNPJ, @"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$"))
            erros += "Erro! O campo CNPJ deve estar no formato XX.XXX.XXX/XXXX-XX.\n";

        if (string.IsNullOrEmpty(Telefone))
            erros += "Erro! O campo Telefone é obrigatório.\n";

        else if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros += "Erro! O campo Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.\n";

        return erros;
    }

    public override void AtualizarRegistro(Fornecedor resgitroEditado)
    {
        Nome = resgitroEditado.Nome;
        CNPJ = resgitroEditado.CNPJ;
        Telefone = resgitroEditado.Telefone;
    }
}