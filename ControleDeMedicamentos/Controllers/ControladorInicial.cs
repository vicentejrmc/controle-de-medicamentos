using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.Controllers
{
    [Route("/")] /*Atributo de rota,  rota inicial*/

    public class ControladorInicial : Controller
    {
        public IActionResult PaginaInicial()
        {
            string conteudo = System.IO.File.ReadAllText("Compartilhado/Html/PaginaInicial.html"); // O ReadAllText lê o testo e passa para uma string

            return Content(conteudo, "text/html");   // 1° Retorna conteudo 2° determina o tipo de arquivo a ser reenderizado, evitando conflitos
        }
    }
}
