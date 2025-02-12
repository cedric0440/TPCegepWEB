using GestionCegepWeb.Logics.Controleurs;
using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace TPCegepWEB.Controllers
{
    public class EnseignantController : Controller
    {
        [Route("Enseignants")]
        [Route("Enseignants/Index")]
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


        [Route("AjouterEnseignant")]
        [Route("/Enseignants/AjouterEnseignant")]
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
            return RedirectToAction("Index", "Enseignants", new { nomCegep ,nomDepartement});
        }
    }
}
