using Microsoft.AspNetCore.Mvc;

namespace BeautySales.AplicacionWeb.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
