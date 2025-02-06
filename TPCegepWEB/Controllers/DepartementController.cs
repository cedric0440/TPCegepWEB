using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Logics.DAOs;
using GestionCegepWeb.Logics.Modeles;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace TPCegepWEB.Controllers
{
    public class DepartementController : Controller
    {
        [Route("Departements")]
        [Route("Departements/Index")]
        [HttpGet]
        public IActionResult Index(string ? cegepId)
        {
            try
            {
                var listeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                ViewBag.ListeCegeps = listeCegeps;

                if (listeCegeps.Count > 0)
                {
                    // Utilise le cégep sélectionné ou le premier par défaut
                    string nomCegep = cegepId ?? listeCegeps[0].Nom;

                    ViewBag.CegepSelectionne = nomCegep;
                    ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep);
                    return View();
                }
                else
                {
                    ViewBag.ErrorMessage = "Aucun cégep n'est disponible.";
                    ViewBag.ListeDepartements = new List<DepartementDTO>();
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                ViewBag.ListeDepartements = new List<DepartementDTO>();
                return View();
            }
        }
    }
}
