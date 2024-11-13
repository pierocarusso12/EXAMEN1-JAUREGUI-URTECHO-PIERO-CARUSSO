using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Models;

namespace EXAMEN1_JAUREGUI_URTECHO_PIERO_CARUSSO.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Orders");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}