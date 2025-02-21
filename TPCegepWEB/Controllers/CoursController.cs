using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TPCegepWEB.Controllers
{
    /// <summary>
    /// classe controller
    /// </summary>
    public class CoursController : Controller
    {
        /// <summary>
        /// index du cours
        /// retourne la vue du cours
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <returns></returns>
        [Route("Cours")]
        [Route("Cours/Index")]
        [HttpGet]
        public IActionResult Index([FromQuery] string nomCegep, [FromQuery] string nomDepartement)
        {
            try
            {
                //Si aucun Cégep/Département n'est préalablement sélectionné...
                if ((nomCegep is null) || (nomDepartement is null))
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                }

                //Préparation des données pour la vue...
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                ViewBag.Cegep = CegepControleur.Instance.ObtenirCegep(nomCegep);
                ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep);
                ViewBag.Departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement);
            }
            catch (Exception e)
            {
                //Si le Cégep est un bon Cégep, on utilise le premier département...
                if ((ViewBag.Cegep != null) && (e.Message == "Erreur lors de l'obtention d'un département par son nom et son cégep..."))
                {
                    try
                    {
                        if (CegepControleur.Instance.ObtenirListeDepartement(nomCegep).Count > 0)
                        {
                            nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                            ViewBag.Departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                            ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement);
                        }
                        else
                        {
                            nomDepartement = "";
                            ViewBag.Departement = new DepartementDTO();
                            ViewBag.ListeCours = new List<CoursDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.MessageErreur = ex.Message;
                    }
                }
                else
                {
                    nomCegep = CegepControleur.Instance.ObtenirListeCegep()[0].Nom;
                    nomDepartement = CegepControleur.Instance.ObtenirListeDepartement(nomCegep)[0].Nom;
                    //Préparation des données pour la vue...
                    ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                    ViewBag.Cegep = CegepControleur.Instance.ObtenirCegep(nomCegep);
                    ViewBag.ListeDepartements = CegepControleur.Instance.ObtenirListeDepartement(nomCegep);
                    ViewBag.Departement = CegepControleur.Instance.ObtenirDepartement(nomCegep, nomDepartement);
                    ViewBag.ListeCours = CegepControleur.Instance.ObtenirListeCours(nomCegep, nomDepartement);
                }
            }

            //Retour de la vue...
            return View();
        }

        /// <summary>
        /// Action AjouterCours.
        /// Permet de Ajouter un Cours.
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <param name="coursDTO"></param>
        /// <returns></returns>
        [Route("AjouterCours")]
        [Route("/Cours/AjouterCours")]
        [HttpPost]
        public IActionResult AjouterCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] CoursDTO coursDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterCours(nomCegep, nomDepartement, coursDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                //TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cours", new { nomCegep, nomDepartement });
        }


        /// <summary>
        /// Action SupprimerCours.
        /// Permet de supprimer un Cours.
        /// </summary>
        /// <param name="nomCegep">Le nom du Cégep.</param>
        /// <param name="nomDepartement">Le nom du Département.</param>
        /// <param name="nomCours">Le nom du Cours.</param>
        /// <returns>ActionResult</returns>

        [Route("SupprimerCours")]
        [Route("/Cours/SupprimerCours")]
        [HttpPost]
        public IActionResult SupprimerCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] string  nomCours)
        {
            try
            {
                CegepControleur.Instance.SupprimerCours(nomCegep, nomDepartement, nomCours);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Cours", new { nomCegep, nomDepartement });
        }

        /// <summary>
        /// Action ViderListeCours.
        /// Permet de Vider la liste d'un Cours.
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <returns></returns>
        [Route("/Cours")]
        [Route("/Cours/ViderListeCours")]
        [HttpPost]
        public IActionResult ViderListeCours([FromForm] string nomCegep, [FromForm] string nomDepartement)
        {
            try
            {
                CegepControleur.Instance.ViderListeCours(nomCegep, nomDepartement);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            return RedirectToAction("Index", "Enseignants");
        }

        /// <summary>
        /// Action ModifierCours.
        /// Permet de modifier un Cégep.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Departement.</param>
        /// <param name="coursDTO">Le cours a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Cours/ModifierEnseignant")]
        [HttpPost]
        public IActionResult ModifierCours([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] CoursDTO coursDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierCours(nomCegep, nomDepartement, coursDTO);
            }
            catch (Exception e)
            {
                //TempData["MessageErreur"] = e.Message;
                return RedirectToAction("FormulaireModifierCours", "Cours", new { nomCegep, nomDepartement, coursDTO });

            }
            //Lancement de l'action Index...
            return RedirectToAction("Index", "Cours", new { nomCegep, nomDepartement });
        }

        /// <summary>
        /// Action FormulaireModifierCours.
        /// Permet d'afficher le formulaire pour la modification d'un cours.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Département.</param>
        /// <param name="nomCours">Nom du cours.</param>
        /// <returns>IActionResult</returns>
        [Route("/Cours/FormulaireModifierCours")]
        [HttpGet]
        public IActionResult FormulaireModifierCours([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] string nomCours)
        {
            try
            {
                ViewBag.MessageErreur = TempData["MessageErreur"];
                CoursDTO cours = CegepControleur.Instance.ObtenirCours(nomCegep, nomDepartement, nomCours);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement = nomDepartement;
                ViewBag.nomCours = nomCours ;
                return View(cours);
            }
            catch
            {
                return RedirectToAction("Index", "Cours", new { nomCegep, nomDepartement });
            }

        }

    }
}
