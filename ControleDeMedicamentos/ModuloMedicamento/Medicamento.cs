using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;

namespace ControleDeMedicamentos.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string NomeMedicamento { get; set; }
        public string Descricao { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int Quantidade { get; set; }
        public bool EmFalta
        {
            get { return Quantidade < 20; }
        }

        public Medicamento()
        {
            Fornecedor = new Fornecedor();
        }
        public Medicamento(string nomeMedicamento, string descricao, Fornecedor fornecedor, int quantidade) : this()
        {
            NomeMedicamento = nomeMedicamento;
            Descricao = descricao;
            Quantidade = quantidade;
            Fornecedor = fornecedor;
        }

        public override void AtualizarRegistro(Medicamento resgitroEditado)
        {
            NomeMedicamento = resgitroEditado.NomeMedicamento;
            Descricao = resgitroEditado.Descricao;
        }

        public override string Validar()
        {
            string erros = "";

            //Validação de fornecedor implementada na entrada de cadastro de medicamento

            if (string.IsNullOrWhiteSpace(NomeMedicamento))
                erros += "Erro! O campo Nome do Medicamento é obrigatório\n";

            else if (NomeMedicamento.Length < 3 || NomeMedicamento.Length > 100)
                erros += "Erro! O campo Nome do Medicamento deve ter entre 3 e 100 caracteres\n";

            if (string.IsNullOrWhiteSpace(Descricao))
                erros += "Erro! O campo Descrição do Medicamento é obrigatório\n";

            else if (Descricao.Length < 5 || Descricao.Length > 255)
                erros += "Erro! O campo Descrição do Medicamento deve ter entre 3 e 100 caracteres\n";

            if (Quantidade < 0)
                erros += "Erro! O campo Qtd Estoque do Medicamento não pode ser < 0\n";

            if (Fornecedor == null)
                erros += "Erro! O campo Fornecedor do Medicamento é obrigatório\n";

            return erros;
        }

        public void AdicionarEstoque(int quantidade)
        {
                Quantidade += quantidade;
        }
        public void RemoverEstoque(int quantidade)
        {
                Quantidade -= quantidade;
        }
    }
}
