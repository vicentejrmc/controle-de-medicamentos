using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloFornecedor;
using ControleDeMedicamentos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ModuloMedicamento
{
    public class TelaMedicamento : TelaBase<Medicamento>, ITelaCrud
    {
        public IRepositorioMedicamento repositorioMedicamento;
        public IRepositorioFornecedor repositorioFornecedor;

        public TelaMedicamento(IRepositorioMedicamento repositorioMedicamento, IRepositorioFornecedor repositorioFornecedor) : base("Medicamento", repositorioMedicamento)
        {
            this.repositorioMedicamento = repositorioMedicamento;
            this.repositorioFornecedor = repositorioFornecedor;
        }

        protected void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Controle de Medicamentos");
            Console.WriteLine("\n--------------------------------------------");
        }

        public override void CadastrarRegistro()
        {
            ExibirCabecalho();

            if (repositorioFornecedor.SelecionarTodos().Count == 0)
            {
                Notificador.ExibirMensagem("Erro! Você precisa cadastrar um 'Fornecedor' antes de cadastrar um 'Medicamento'.", ConsoleColor.Red);
                return;
            }

            Medicamento novoMedicamento = ObterDados();

            string ehValido = novoMedicamento.Validar();

            if (ehValido.Length > 0)
            {
                Notificador.ExibirMensagem(ehValido, ConsoleColor.Red);

                return;
            }

            repositorioMedicamento.CadastrarRegistro(novoMedicamento);

            Notificador.ExibirMensagem("Medicamento cadastrado com sucesso!", ConsoleColor.Green);
        }

        public override void EditarRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Editando Medicamentos");
            Console.WriteLine("\n--------------------------------------------");

            VisualizarRegistros(false);

            Console.Write("Digite o id do Medicamento que deseja editar: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

            Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idSelecionado);

            if (medicamento == null)
            {
                Notificador.ExibirMensagem("Medicamento não encontrado!", ConsoleColor.Red);
                return;
            }

            medicamento = ObterDados();

            string erros = medicamento.Validar();
            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }

            repositorioMedicamento.EditarRegistro(idSelecionado, medicamento);

            Notificador.ExibirMensagem("Medicamento editado com sucesso!", ConsoleColor.Green);
        }

        public override void ExcluirRegistro()
        {
            ExibirCabecalho();

            Console.WriteLine("Excluindo Medicamentos");
            Console.WriteLine("\n--------------------------------------------");

            VisualizarRegistros(false);

            Console.Write("Digite o id do Medicamento que deseja excluir: ");
            int idSelecionado = Convert.ToInt32(Console.ReadLine()! ?? string.Empty);

            Medicamento medicamento = repositorioMedicamento.SelecionarRegistroPorId(idSelecionado);

            if (medicamento == null)
            {
                Notificador.ExibirMensagem("Medicamento não encontrado!", ConsoleColor.Red);
                return;
            }
            else
            {
                Console.WriteLine("Você tem certeza que deseja excluir o Medicamento? (S/N)");
                string resposta = Console.ReadLine()! ?? string.Empty;
                
                if (resposta.ToUpper() != "S")
                {
                    Notificador.ExibirMensagem("Exclusão cancelada!", ConsoleColor.Yellow);
                    return;
                }
            }

            repositorioMedicamento.ExcluirRegistro(idSelecionado);

            Notificador.ExibirMensagem("Medicamento excluído com sucesso!", ConsoleColor.Green);
        }

        public override Medicamento ObterDados()
        {
            Console.Write("Digite o Nome do Medicamento: ");
            string nome = Console.ReadLine()! ?? string.Empty;

            Console.Write("Digite a Descrição Medicamento: ");
            string descricao = Console.ReadLine()! ?? string.Empty;

            TelaFornecedor telaFornecedor = new TelaFornecedor(repositorioFornecedor);
            telaFornecedor.VisualizarRegistros(false);

            Console.Write("Selecione o Id do Fornecedor: ");
            int idFornecedor = Convert.ToInt32(Console.ReadLine()!);

            repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

            Fornecedor fornecedor = repositorioFornecedor.SelecionarRegistroPorId(idFornecedor);

            Console.Write("Digite a Quantidade de Medicamento: ");
            int quantidade = Convert.ToInt32(Console.ReadLine()!);

            Medicamento novoMedicamento = new Medicamento(nome, descricao, fornecedor, quantidade);

            return novoMedicamento;

        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            ExibirCabecalho();
            Console.WriteLine("Visualizando Medicamentos");
            Console.WriteLine("\n--------------------------------------------");

            List<Medicamento> medicamentos = repositorioMedicamento.SelecionarTodos();

            Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}", 
                "ID", "Nome", "Quantidade", "Fornecedor", "Descrição");

            foreach (Medicamento med in medicamentos)
            {
                Console.WriteLine("{0, -10} | {1, -20} | {2, -10} | {3, -20} | {4, -30}",
                   med.Id, med.NomeMedicamento, med.QtdEstoque, med.Fornecedor.Nome, med.Descrição);
            }

            Notificador.ExibirMensagem("Pressione qualquer tecla para continuar...", ConsoleColor.Yellow);

        }
    }
}
