using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentos.Controllers
{
    [Route("/")] /*Atributo de rota,  rota inicial*/

    public class ControladorInicial : Controller
    {
        public IActionResult PaginaInicial()
        {
            return View("PaginaInicial");
        }
    }
}
