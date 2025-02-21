using Microsoft.AspNetCore.Mvc;

namespace UnphuVacantesAPI.Controllers
{
    public class HomeController : Controller
    {
        // Acción que devuelve la vista inicial
        public IActionResult Index()
        {
            return View(); // Retorna la vista correspondiente
        }
    }
}
