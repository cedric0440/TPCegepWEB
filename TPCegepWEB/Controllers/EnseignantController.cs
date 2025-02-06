using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Logics.DAOs;
using GestionCegepWeb.Logics.Modeles;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
namespace TPCegepWEB.Controllers
{
    public class EnseignantController : Controller
    {
        [Route("Enseignants")]
        [Route("Enseignants/Index")]
        [HttpGet]
        public IActionResult Index(string? nomCegep, string ?nomDepartement)
        {
            return View();
        }
    }
}
