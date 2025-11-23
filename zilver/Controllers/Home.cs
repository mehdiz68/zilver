using Microsoft.AspNetCore.Mvc;

namespace zilver.Controllers
{
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
