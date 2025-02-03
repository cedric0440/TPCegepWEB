using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TPCegepWEB.Controllers
{
    public class CegepController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }

      
    }
}
