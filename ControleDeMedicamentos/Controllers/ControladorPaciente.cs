using ControleDeMedicamentos.Compartilhado;
using ControleDeMedicamentos.ModuloPaciente;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ControleDeMedicamentos.Controllers
{
    [Route("pacientes")]

    public class ControladorPaciente : Controller
    {
        [HttpGet("cadastrar")]
        public IActionResult ExibirFormularioCadastroPaciente()
        {
            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/cadastrar.html");

            return Content(conteudo, "text/html"); 
            // embora não seja obrigatório recomendado o uso de  ,"text/html"
            // para evitar conflitos e especificar que ao navegador o que será reenderizado
        }

        [HttpPost("cadastrar")]
        public IActionResult CadastrarPaciente(
            [FromForm]string nome,    // FromForm especifica par ao metodo de onde estão vindo as informações.
            [FromForm]string telefone,   //Elimina a necessidade de serem atribidas dentro do Método
            [FromForm]string cartaoSus)     //O proprio MVC entende e passa os atributos para o metodo
            // Note que o nome da variável precisa corresponder exatamente com nome passado pelo formulario
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente novoPaciente = new Paciente(nome, telefone, cartaoSus);

            repositorioPaciente.CadastrarRegistro(novoPaciente);

            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#mensagem#", $"O Registro \"{novoPaciente.Nome}\" foi cadastrado com sucesso!");

            string conteudoSting = sb.ToString();

            return Content(conteudoSting, "text/html");
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult ExibirFormularioEdicaoPaciente([FromRoute]int id) //Maperando parametro de Roda vindo do [HttpGet(.../{id:int})]
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/Editar.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#id#", pacienteSelecionado.Id.ToString());
            sb.Replace("#nome#", pacienteSelecionado.Nome);
            sb.Replace("#telefone#", pacienteSelecionado.Telefone);
            sb.Replace("#cartaoSus#", pacienteSelecionado.CartaoSUS);

            string conteudoString = sb.ToString();

            return Content(conteudoString, "text/html");
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult EditarPaciente(
            [FromRoute] int id,
            [FromForm]string nome,    
            [FromForm]string telefone,   
            [FromForm]string cartaoSus )
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteAtualizado = new Paciente(nome, telefone, cartaoSus);

            repositorioPaciente.EditarRegistro(id, pacienteAtualizado);

            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#mensagem#", $"O Registro \"{pacienteAtualizado.Nome}\" foi Editado com sucesso!");

            string conteudoSting = sb.ToString();

            return Content(conteudoSting, "text/html");
        }

        [HttpGet("excluir/{id:int}")]  // Sempre que tiver um parametro de roda, ele pode ser usado como argumento do método.
        public IActionResult ExibirFormularioExclusaoPaciente([FromRoute] int id)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            Paciente pacienteSelecionado = repositorioPaciente.SelecionarRegistroPorId(id);

            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/Excluir.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#id#", id.ToString());
            sb.Replace("#paciente#", pacienteSelecionado.Nome);

            string conteudoString = sb.ToString();

            return Content(conteudoString, "text/html");

        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirPaciente([FromRoute] int id)
        {
            ContextoDados contextoDados = new ContextoDados(true);
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            repositorioPaciente.ExcluirRegistro(id);

            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/Notificacao.html");

            StringBuilder sb = new StringBuilder(conteudo);

            sb.Replace("#mensagem#", $"O registro foi Excluido com sucesso!");

            string conteudoString = sb.ToString();

            return Content(conteudoString, "text/html");
        }

        [HttpGet("visualizar")]
        public IActionResult VisualizarPacientes()
        {
            ContextoDados contextoDados = new ContextoDados(true); // true indica que os dados serão carregados quando a função for chamada
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente(contextoDados);

            string conteudo = System.IO.File.ReadAllText("ModuloPaciente/Html/Visualizar.html");

            StringBuilder stringBuilder = new StringBuilder(conteudo); // stringBuilder recebendo o conteudo 

            List<Paciente> paciente = repositorioPaciente.SelecionarTodos();

            foreach (Paciente p in paciente)
            {
                string intemLista = $"<li>{p.ToString()} <a href=\"/pacientes/editar/{p.Id}\">Editar</a> / <a href=\"/pacientes/excluir/{p.Id}\">Excluir</a> </li> #paciente#";

                stringBuilder.Replace("#paciente#", intemLista);
            }

            stringBuilder.Replace("#paciente#", "");

            string conteudoString = stringBuilder.ToString();

            return Content(conteudoString, "text/html");
        }

    }
}
