using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;

namespace ControleDeMedicamentos.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string NomeMedicamento { get; set; }
        public string Descrição { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int QtdEstoque { get; set; }

        public Medicamento(string nomeMedicamento, string descricao, Fornecedor fornecedor, int quantidade)
        {
            NomeMedicamento = nomeMedicamento;
            Descrição = descricao;
            Fornecedor = fornecedor;
            QtdEstoque = quantidade;
        }

        public override void AtualizarRegistro(Medicamento resgitroEditado)
        {
            NomeMedicamento = resgitroEditado.NomeMedicamento;
            Descrição = resgitroEditado.Descrição;
        }

        public override string Validar()
        {
            string ehValido = "";

            //if (string.IsNullOrWhiteSpace(NomeMedicamento))
            //    ehValido += "Erro! O campo Nome do Medicamento é obrigatório\n";

            //if(NomeMedicamento.Length < 3 || NomeMedicamento.Length > 100)
            //    ehValido += "Erro! O campo Nome do Medicamento deve ter entre 3 e 100 caracteres\n";

            //if (string.IsNullOrWhiteSpace(Descrição))
            //    ehValido += "Erro! O campo Descrição do Medicamento é obrigatório\n";

            //if (Descrição.Length < 5 || Descrição.Length > 255)
            //    ehValido += "Erro! O campo Descrição do Medicamento deve ter entre 3 e 100 caracteres\n";

            //if (QtdEstoque < 0)
            //    ehValido += "Erro! O campo Qtd Estoque do Medicamento não pode ser < 0\n";

            return ehValido;
        }

        public void AdicionarEstoque(int quantidade)
        {
            if (quantidade > 0)
                QtdEstoque += quantidade;
        }
    }
}
