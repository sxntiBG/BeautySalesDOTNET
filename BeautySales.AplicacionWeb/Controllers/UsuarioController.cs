using Microsoft.AspNetCore.Mvc;

namespace BeautySales.AplicacionWeb.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
