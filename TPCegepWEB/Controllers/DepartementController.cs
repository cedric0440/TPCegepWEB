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
        public IActionResult Index(string ? nomCegep)
        {
            try
            {
                var listeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                ViewBag.ListeCegeps = listeCegeps;

                if (listeCegeps.Count > 0)
                {
                    // Utilise le cégep sélectionné ou le premier par défaut
                    ViewBag.CegepSelectionne = nomCegep ?? listeCegeps[0].Nom;

                   
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

        [Route("AjouterDepartement")]
        [Route("/Departements/AjouterDepartement")]
        [HttpPost]
        public IActionResult AjouterDepartement([FromForm] string nomCegep,[FromForm] DepartementDTO departementDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterDepartement(nomCegep,departementDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
               // TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Departements", new {nomCegep});
        }


        /// <summary>
        /// Action SupprimerDepartement.
        /// Permet de supprimer un Cégep.
        /// </summary>
        /// <param name="nomCegep">Le nom du Cégep.</param>
        /// <param name="nomDepartement">Le nom du Département.</param>
        /// <returns>ActionResult</returns>

        [Route("SupprimerDepartement")]
        [Route("/Departements/SupprimerDepartement")]
        [HttpPost]
        public IActionResult SupprimerDepartement([FromForm] string nomCegep, [FromForm] string nomDepartement)
        {
            try
            {
                CegepControleur.Instance.SupprimerDepartement(nomCegep,nomDepartement);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Departements", new {nomCegep,nomDepartement});
        }

        /// <summary>
        /// Action ViderListeDepartement.
        /// Permet de vider la liste des Départements.
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("/Departements/ViderListeDepartement")]
        [HttpPost]
        public IActionResult ViderListeDepartement([FromForm] string nomCegep)
        {
            try
            {
                CegepControleur.Instance.ViderListeDepartement(nomCegep);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            return RedirectToAction("Index", "Departements");
        }

    }
}
