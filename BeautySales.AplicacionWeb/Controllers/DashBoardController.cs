using Microsoft.AspNetCore.Mvc;

namespace BeautySales.AplicacionWeb.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
