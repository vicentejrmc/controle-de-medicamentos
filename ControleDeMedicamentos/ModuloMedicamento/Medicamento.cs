using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.ModuloRequisicaoEntrada;
using ControleDeMedicamentos.ModuloRequisicaoSaida;

namespace ControleDeMedicamentos.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public int Quantidade { get; set; }
        public List<RequisicaoEntrada> RequisicoesEntrada { get; set; }
        public List<RequisicaoSaida> RequisicoesSaida { get; set; }

        public bool EmFalta
        {
            get { return Quantidade < 20; }
        }

        public Medicamento()
        {
            RequisicoesEntrada = new List<RequisicaoEntrada>();
            RequisicoesSaida = new List<RequisicaoSaida>();
        }


        public Medicamento(string nome, string descricao, Fornecedor fornecedor, int quantidade) : this()
        {
            Nome = nome;
            Descricao = descricao;
            Quantidade = quantidade;
            Fornecedor = fornecedor;
        }

        public override void AtualizarRegistro(Medicamento resgitroEditado)
        {
            Nome = resgitroEditado.Nome;
            Descricao = resgitroEditado.Descricao;
        }

        public override string Validar()
        {
            string erros = "";

            //Validação de fornecedor implementada na entrada de cadastro de medicamento

            if (string.IsNullOrWhiteSpace(Nome))
                erros += "Erro! O campo Nome do Medicamento é obrigatório\n";

            else if (Nome.Length < 3 || Nome.Length > 100)
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

        public void AdicionarAoEstoque(RequisicaoEntrada requisicaoEntrada)
        {
            if (!RequisicoesEntrada.Contains(requisicaoEntrada))
                RequisicoesEntrada.Add(requisicaoEntrada);
        }

        public void RemoverDoEstoque(RequisicaoSaida requisicaoSaida)
        {
            if (!RequisicoesSaida.Contains(requisicaoSaida))
                RequisicoesSaida.Add(requisicaoSaida);
        }

    }
}
