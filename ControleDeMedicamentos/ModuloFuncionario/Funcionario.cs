using ControleDeMedicamentos.Compartilhado;
using System.Text.RegularExpressions;

namespace ControleDeMedicamentos.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }

        public Funcionario()
        {
        }

        public Funcionario(string? nome, string? cPF, string? telefone)
        {
            Nome = nome;
            CPF = cPF;
            Telefone = telefone;
        }

        public override void AtualizarRegistro(Funcionario resgitroEditado)
        {
            Nome = resgitroEditado.Nome;
            CPF = resgitroEditado.CPF;
            Telefone = resgitroEditado.Telefone;
        }

        public override string Validar()
        {
            string erros = "";

            if (string.IsNullOrEmpty(Nome))
                erros += "Erro! O campo Nome é obrigatório.\n";

            else if (Nome!.Length < 3 || Nome.Length > 100)
                erros += "Erro! O campo Nome deve ter entre 3 e 100 caracteres.\n";

            if (string.IsNullOrEmpty(CPF))
                erros += "Erro! O campo CPF é obrigatório.\n";

            else if (!Regex.IsMatch(CPF!.ToString(), @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
                erros += "Erro! O campo CPF deve estar no formato XXX.XXX.XXX-XX.\n";

            if (string.IsNullOrEmpty(Telefone))
                erros += "Erro! O campo Telefone é obrigatório.\n";

            else if (!Regex.IsMatch(Telefone, @"^\(\d{2}\) \d{4,5}-\d{4}$"))
                erros += "Erro! O campo Telefone deve estar no formato (XX) XXXX-XXXX ou (XX) XXXXX-XXXX.\n";

            return erros;
        }
    }
}