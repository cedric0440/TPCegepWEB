using System.Diagnostics;
using System.Reflection;
using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Xunit;

namespace TPCegepWEB.Controllers
{
    public class CegepController : Controller
    {
        [Route("")]
        [Route("Cegeps")]
        [Route("Cegeps/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                return View(new CegepDTO());
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();

            }
        }

        [Route("AjouterCegep")]
        [Route("/Cegep/AjouterCegep")]
        [HttpPost]
        public IActionResult AjouterCegep([FromForm] CegepDTO cegepDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterCegep(cegepDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cegep");
        }










    }
}
