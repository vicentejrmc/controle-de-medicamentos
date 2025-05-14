using ControleDeMedicamentos.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ModuloPaciente;

public class Paciente : EntidadeBase<Paciente>
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string CartaoSUS { get; set; }


    public Paciente() {}

    public Paciente(string nome, string telefone, string cartaoSUS)
    {
        Nome = nome;
        Telefone = telefone;
        CartaoSUS = cartaoSUS;
    }

    public override void AtualizarRegistro(Paciente resgitroEditado)
    {
        Nome = resgitroEditado.Nome;
        Telefone = resgitroEditado.Telefone;
        CartaoSUS = resgitroEditado.CartaoSUS;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Nome: {Nome}, Telefone: {Telefone}, CartaoSus: {CartaoSUS}";
    }

    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrEmpty(Nome))
            erros += "Erro! O campo Nome é obrigatório.\n";

        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "Erro! O campo Nome deve ter entre 3 e 100 caracteres.\n";

        if (string.IsNullOrEmpty(CartaoSUS))
            erros += "Erro! O campo Cartão do SUS é obrigatório.\n";

        else if (CartaoSUS.Length != 15)
            erros += "Erro! O campo Cartão do SUS deve ter exatamente 15 caracteres.\n";

        if (string.IsNullOrEmpty(Telefone))
            erros += "Erro! O campo Telefone é obrigatório.\n";

        else if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
            erros += "Erro! O campo Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.\n";

        return erros;
    }
}