using ControleDeMedicamentos.Compatilhado;
using ControleDeMedicamentos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeMedicamentos.ModuloFuncionario
{
    public class TelaFuncionario : TelaBase<Funcionario>, ITelaCrud
    {
        public IRepositorioFuncionario repositorioFuncionario;

        public TelaFuncionario(string nomeEntidade, IRepositorioFuncionario repositorioFuncionario) : base(nomeEntidade, repositorioFuncionario)
        {
            this.repositorioFuncionario = repositorioFuncionario;
        }

        protected void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Controle de Funcionarios");
            Console.WriteLine("\n--------------------------------------------");
        }

        public override void InserirRegistro()
        {
            ExibirCabecalho();

            Funcionario novoFuncionario = ObterDados();

            string erros = novoFuncionario.Validar();
            if (erros.Length > 0)
            {
                Notificador.ExibirMensagem(erros, ConsoleColor.Red);
                return;
            }

            repositorioFuncionario.CadastrarRegistro(novoFuncionario);

            Notificador.ExibirMensagem("Funcionario cadastrado com sucesso!", ConsoleColor.Green);
        }

        public override Funcionario ObterDados()
        {
            Console.Write("Digite o nome do funcionario: ");
            string nome = Console.ReadLine()! ?? string.Empty;

            Console.Write("Digite o CPF do funcionario: ");
            string cpf = Console.ReadLine()! ?? string.Empty;

            Console.Write("Digite o telefone do funcionario: ");
            string telefone = Console.ReadLine()! ?? string.Empty;

            Funcionario funcionario = new Funcionario
            {
                Nome = nome,
                CPF = cpf,
                Telefone = telefone
            };

            return funcionario;
        }

        public override void VisualizarRegistros(bool exibirTitulo)
        {
            throw new NotImplementedException();
        }
    }
}
