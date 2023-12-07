using Microsoft.AspNetCore.Mvc;

namespace BeautySales.AplicacionWeb.Controllers
{
    public class NegocioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
