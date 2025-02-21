using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TPCegepWEB.Controllers
{
    /// <summary>
    /// classe Controller
    /// </summary>
    public class EnseignantController : Controller
    {
        /// <summary>
        /// index de l'enseignant(e)
        /// retourne la vue de l'enseignant(e)
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <returns></returns>
        [Route("Enseignant")]
        [Route("Enseignant/Index")]
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
                ViewBag.ListeEnseignants = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement);
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
                            ViewBag.ListeEnseignants = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement);
                        }
                        else
                        {
                            nomDepartement = "";
                            ViewBag.Departement = new DepartementDTO();
                            ViewBag.ListeEnseignants = new List<EnseignantDTO>();
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
                    ViewBag.ListeEnseignants = CegepControleur.Instance.ObtenirListeEnseignant(nomCegep, nomDepartement);
                }
            }

            //Retour de la vue...
            return View();
        }


        /// <summary>
        /// Action AjouterEnseignant.
        /// Permet d.Ajouter un enseignant(e).
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <param name="enseignantDTO"></param>
        /// <returns></returns>
        [Route("AjouterEnseignant")]
        [Route("/Enseignant/AjouterEnseignant")]
        [HttpPost]
        public IActionResult AjouterEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] EnseignantDTO enseignantDTO)
        {
            try
            {
                CegepControleur.Instance.AjouterEnseignant(nomCegep, nomDepartement, enseignantDTO);
            }
            catch (Exception e)
            {
                //Mettre cette ligne en commentaire avant de lancer les tests fonctionnels
                //TempData["MessageErreur"] = e.Message;
            }

            //Lancement de l'action Index...
            return RedirectToAction("Index", "Enseignant", new { nomCegep ,nomDepartement});
        }

        /// <summary>
        /// Action SupprimerEnseignant.
        /// Permet de supprimer un Enseignant(e).
        /// </summary>
        /// <param name="nomCegep">Le nom du Cégep.</param>
        /// <param name="nomDepartement">Le nom du Département.</param>
        /// <param name="noEnseignant">Le numéro employé d'un enseignant(e).</param>
        /// <returns>ActionResult</returns>

        [Route("SupprimerEnseignant")]
        [Route("/Enseignant/SupprimerEnseignant")]
        [HttpPost]
        public IActionResult SupprimerEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement, [FromForm] int noEnseignant)
        {
            try
            {
                CegepControleur.Instance.SupprimerEnseignant(nomCegep, nomDepartement,noEnseignant);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }
            return RedirectToAction("Index", "Enseignant", new { nomCegep, nomDepartement});
        }

        /// <summary>
        ///  /// Action ViderListeEnseignant.
        /// Permet de vider la liste des Enseignant(e)
        /// </summary>
        /// <param name="nomCegep"></param>
        /// <param name="nomDepartement"></param>
        /// <returns></returns>
        [Route("/Enseignant")]
        [Route("/Enseignant/ViderListeEnseignant")]
        [HttpPost]
        public IActionResult ViderListeEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement)
        {
            try
            {
                CegepControleur.Instance.ViderListeEnseignant(nomCegep,nomDepartement);
            }
            catch (Exception e)
            {
                ViewBag.MessageErreur = e.Message;
            }

            return RedirectToAction("Index", "Enseignant");
        }


        /// <summary>
        /// Action ModifierDepartement.
        /// Permet de modifier un Cégep.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Departement.</param>
        /// <param name="enseignantDTO">Le enseignant a modifier.</param>
        /// <returns>ActionResult</returns>
        [Route("/Enseignant/ModifierEnseignant")]
        [HttpPost]
        public IActionResult ModifierEnseignant([FromForm] string nomCegep, [FromForm] string nomDepartement,[FromForm] EnseignantDTO enseignantDTO)
        {
            try
            {
                CegepControleur.Instance.ModifierEnseignant(nomCegep, nomDepartement,enseignantDTO);
            }
            catch (Exception e)
            {
                TempData["MessageErreur"] = e.Message;
                return RedirectToAction("FormulaireModifierEnseignant", "Enseignant", new { nomCegep, nomDepartement,enseignantDTO });

            }
            //Lancement de l'action Index...
            return RedirectToAction("Index", "Enseignant", new { nomCegep,nomDepartement });
        }

        /// <summary>
        /// Action FormulaireModifierDepartement.
        /// Permet d'afficher le formulaire pour la modification d'un Enseignant.
        /// </summary>
        /// <param name="nomCegep">Nom du Cégep.</param>
        /// <param name="nomDepartement">Nom du Département.</param>
        /// <param name="noEnseignant">No de l'enseignant(e).</param>
        /// <returns>IActionResult</returns>
        [Route("/Enseignant/FormulaireModifierEnseignant")]
        [HttpGet]
        public IActionResult FormulaireModifierEnseignant([FromQuery] string nomCegep, [FromQuery] string nomDepartement, [FromQuery] int noEnseignant)
        {
            try
            {
                ViewBag.MessageErreur = TempData["MessageErreur"];
                EnseignantDTO enseignant = CegepControleur.Instance.ObtenirEnseignant(nomCegep, nomDepartement,noEnseignant);
                ViewBag.nomCegep = nomCegep;
                ViewBag.nomDepartement=nomDepartement;
                ViewBag.noEnseignant = noEnseignant;
                return View(enseignant);
            }
            catch
            {
                return RedirectToAction("Index", "Enseignant", new { nomCegep ,nomDepartement});
            }

        }
    }
}
