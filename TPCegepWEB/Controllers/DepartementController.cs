using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Logics.DAOs;
using GestionCegepWeb.Logics.Modeles;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace TPCegepWEB.Controllers
{
    /// <summary>
    /// classe controller
    /// </summary>
    public class DepartementController : Controller
    {
        /// <summary>
        /// index du département
        /// retourne la vue du département
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <returns></returns>
        [Route("Departement")]
        [Route("Departement/Index")]
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

        /// <summary>
        /// / Action AjouterDepartement.
        /// Permet de Ajjouter un département.
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="departementDTO"></param>
        /// <returns></returns>
        [Route("AjouterDepartement")]
        [Route("/Departement/AjouterDepartement")]
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
            return RedirectToAction("Index", "Departement", new {nomCegep});
        }


        /// <summary>
        /// Action SupprimerDepartement.
        /// Permet de supprimer un département.
        /// </summary>
        /// <param name="nomCegep">Le nom du Cégep.</param>
        /// <param name="nomDepartement">Le nom du Département.</param>
        /// <returns>ActionResult</returns>

        [Route("SupprimerDepartement")]
        [Route("/Departement/SupprimerDepartement")]
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
            return RedirectToAction("Index", "Departement", new {nomCegep,nomDepartement});
        }

        /// <summary>
        /// Action ViderListeDepartement.
        /// Permet de vider la liste des Départements.
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <returns></returns>
        [Route("/Departement/ViderListeDepartement")]
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

            return RedirectToAction("Index", "Departement");
        }

        /// <summary>
        /// Action ModifierDepartement.
        /// Permet de modifier un Cégep.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="departementDTO">Le Département a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Departement/ModifierDepartement")]
        [HttpPost]
        public IActionResult ModifierDepartement([FromForm] string nomCegep,[FromForm] DepartementDTO departementDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierDepartement(nomCegep,departementDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
                return RedirectToAction("FormulaireModifierDepartement", "Departement", new { nomCegep,departementDTO});

            }
            //Lancement de l'action Index...
            return RedirectToAction("Index", "Departement", new{ nomCegep});
        }

        /// <summary>
        /// Action FormulaireModifierDepartement.
        /// Permet d'afficher le formulaire pour la modification d'un Département.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Département.</param>
        /// <returns>IActionResult</returns>
        [Route("/Departement/FormulaireModifierDepartement")]
        [HttpGet]
        public IActionResult FormulaireModifierDepartement([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            try
            {
                ViewBag.MessageErreur = TempData["MessageErreur"];
                DepartementDTO departement = CegepControleur.Instance.ObtenirDepartement(nomCegep,nomDepartement);
                ViewBag.nomCegep = nomCegep;
                return View(departement);
            }
            catch
            {
                return RedirectToAction("Index", "Departement", new{ nomCegep});
            }

        }
    }
}
