using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Logics.DAOs;
using GestionCegepWeb.Logics.Modeles;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace TPCegepWEB.Controllers
{
    public class DepartementController : Controller
    {
      
        public IActionResult Index([FromQuery] string nomCegep)
        {
            // Récupération de la liste des cégeps pour affichage
            ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();

            if (!string.IsNullOrEmpty(nomCegep))
            {
                // Récupération des départements du cégep sélectionné
                ViewBag.Departements = DepartementRepository.Instance.ObtenirListeDepartement(nomCegep);

                // Récupération du cégep sélectionné
                var cegepDTO = CegepRepository.Instance.ObtenirCegep(nomCegep);
                ViewBag.CegepSelectionne = cegepDTO != null ? cegepDTO.Nom : "Inconnu";
            }
            else
            {
                // Affichage de tous les départements si aucun cégep n'est sélectionné
                ViewBag.Departements = DepartementRepository.Instance.ObtenirListeDepartement(null);
                ViewBag.CegepSelectionne = "Tous les cégeps";
            }

            return View();
        }
    }
}
