using GestionCegepWeb.Models;
using Microsoft.AspNetCore.Mvc;
using TPCegepWEB.Controllers;

namespace TPCegepWEB
{

    [TestClass]
    public sealed class Test1
    {

        [TestMethod]
        public void TestMethod1()
        {
            CegepController controleur = new CegepController();
            ViewResult viewResult = (ViewResult)controleur.Index();

            List<CegepDTO> list = (List<CegepDTO>)viewResult.ViewData["ListeCegeps"];
            Assert.IsNotNull(list);
            Assert.AreEqual(6, list.Count);
            Assert.AreEqual("Cegep Exemple 1", list[0].Nom);
        }
        [TestMethod]
        public void Test_Cegep_Null()
        {
            // Arrange
            DepartementController controleur = new DepartementController();

            // Act
            ViewResult viewResult = (ViewResult)controleur.Index(null);

            // Assert
            List<CegepDTO> list = (List<CegepDTO>)viewResult.ViewData["ListeCegeps"];
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count > 0); // Il doit y avoir au moins un cégep disponible.
        }

        [TestMethod]
        public void Test_Cegep_Inexistant()
        {
            // Arrange
            DepartementController controleur = new DepartementController();

            // Act
            ViewResult viewResult = (ViewResult)controleur.Index("Cégep Inconnu");

            // Assert
            List<CegepDTO> list = (List<CegepDTO>)viewResult.ViewData["ListeCegeps"];
            Assert.IsNotNull(list);
            Assert.AreEqual(6, list.Count); // Aucun cégep ne devrait être trouvé.
        }

        [TestMethod]
        public void Test_BonCegep_BonDepart()
        {
            // Arrange
            string cegepNom = "Cegep Exemple 1";
            string departementNom = "Informatique";
            DepartementController controller = new DepartementController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom);
            List<DepartementDTO> departements = (List<DepartementDTO>)viewResult.ViewData["ListeDepartements"];

            // Assert
            Assert.IsNotNull(departements, "La liste des départements ne doit pas être null.");
            Assert.IsTrue(departements.Exists(d => d.Nom == departementNom), "Le département 'Informatique' doit exister pour ce cégep.");
        }

        [TestMethod]
        public void Test_BonCegep_MauvaisDepart()
        {
            // Arrange
            string cegepNom = "Cegep Exemple 1";
            string departementNom = "Astronomie"; // Ce département n'existe pas
            DepartementController controller = new DepartementController();


            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom);
            List<DepartementDTO> departements = (List<DepartementDTO>)viewResult.ViewData["ListeDepartements"];

            // Assert
            Assert.IsNotNull(departements, "La liste des départements ne doit pas être null.");
            Assert.IsFalse(departements.Exists(d => d.Nom == departementNom), "Le département Astronomie ne devrait pas exister dans ce cégep.");
        }

        [TestMethod]
        public void Test_MauvaisCegep_MauvaisDepart()
        {
            // Arrange
            string cegepNom = "Cégep Inexistant";
            string departementNom = "Mathématiques";
            DepartementController controller = new DepartementController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom);
            List<DepartementDTO> departements = (List<DepartementDTO>)viewResult.ViewData["ListeDepartements"];

            // Assert
            Assert.IsNotNull(departements, "La liste des départements ne doit pas être null.");
            Assert.AreEqual(0, departements.Count, "Si le cégep n'existe pas, il ne doit pas y avoir de départements listés.");
        }


        [TestMethod]
        public void Test_BonCegep_BonDepart_Enseignants()
        {
            // Arrange
            string cegepNom = "Cegep Exemple 1";
            string departementNom = "Informatique";
            EnseignantController controller = new EnseignantController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom, departementNom);
            List<EnseignantDTO> enseignants = ((List<EnseignantDTO>)viewResult.ViewData["ListeEnseignants"]);

            // Assert
            Assert.IsNotNull(enseignants, "La liste des enseignants ne doit pas être null.");
            Assert.IsTrue(enseignants.Count > 0, "Il doit y avoir au moins un enseignant dans le département 'Informatique' pour ce cégep.");
        }

        [TestMethod]
        public void Test_BonCegep_MauvaisDepart_Enseignants()
        {
            // Arrange
            string cegepNom = "Cegep Exemple 1";
            string departementNom = "Astronomie"; // Ce département n'existe pas
            EnseignantController controller = new EnseignantController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom, departementNom);
            List<EnseignantDTO> enseignants = ((List<EnseignantDTO>)viewResult.ViewData["ListeEnseignants"]);

            // Assert
            Assert.IsNotNull(enseignants, "La liste des enseignants ne doit pas être null.");
            Assert.AreEqual(3, enseignants.Count, "Aucun enseignant ne doit être listé pour un département inexistant.");
        }

        [TestMethod]
        public void Test_MauvaisCegep_MauvaisDepart_Enseignants()
        {
            // Arrange
            string cegepNom = "Cégep Inexistant";
            string departementNom = "Mathématiques";
            EnseignantController controller = new EnseignantController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom, departementNom);
            List<EnseignantDTO> enseignants = ((List<EnseignantDTO>)viewResult.ViewData["ListeEnseignants"]);

            // Assert
            Assert.IsNotNull(enseignants, "La liste des enseignants ne doit pas être null.");
            Assert.AreEqual(3, enseignants.Count, "Si le cégep n'existe pas, il ne doit pas y avoir d'enseignants listés.");
        }


        [TestMethod]
        public void Test_BonCegep_BonDepart_Cours()
        {
            // Arrange
            string cegepNom = "Cegep Exemple 1";
            string departementNom = "Informatique";
            CoursController controller = new CoursController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom, departementNom);
            List<CoursDTO> cours = ((List<CoursDTO>)viewResult.ViewData["ListeCours"]);

            // Assert
            Assert.IsNotNull(cours, "La liste des cours ne doit pas être null.");
            Assert.IsTrue(cours.Count > 0, "Il doit y avoir au moins un cours dans le département 'Informatique' pour ce cégep.");
        }

        [TestMethod]
        public void Test_BonCegep_MauvaisDepart_Cours()
        {
            // Arrange
            string cegepNom = "Cegep Exemple 1";
            string departementNom = "Astronomie"; // Ce département n'existe pas
            CoursController controller = new CoursController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom, departementNom);
            List<CoursDTO> cours = ((List<CoursDTO>)viewResult.ViewData["ListeCours"]);

            // Assert
            Assert.IsNotNull(cours, "La liste des cours ne doit pas être null.");
            Assert.AreEqual(2, cours.Count, "Aucun cours ne doit être listé pour un département inexistant.");
        }

        [TestMethod]
        public void Test_MauvaisCegep_MauvaisDepart_Cours()
        {
            // Arrange
            string cegepNom = "Cégep Inexistant";
            string departementNom = "Mathématiques";
            CoursController controller = new CoursController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom, departementNom);
            List<CoursDTO> cours = ((List<CoursDTO>)viewResult.ViewData["ListeCours"]);

            // Assert
            Assert.IsNotNull(cours, "La liste des cours ne doit pas être null.");
            Assert.AreEqual(2, cours.Count, "Si le cégep n'existe pas, il ne doit pas y avoir de cours listés.");
        }


        [TestMethod]
        public void Test_BCegep_BDepart_AjouterDepartement()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 1";
            DepartementDTO departementDTO = new DepartementDTO { No = "1", Nom = "Informatique", Description = "Département dédié à linformatique" };

            DepartementController controller = new DepartementController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterDepartement(nomCegep, departementDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep);
            List<DepartementDTO> departements = (List<DepartementDTO>)indexResult.ViewData["ListeDepartements"];

            // Assert
            Assert.IsNotNull(departements, "La liste des départements ne doit pas être null.");
            Assert.IsTrue(departements.Exists(d => d.Nom == departementDTO.Nom), "Le département doit exister pour ce cégep.");
        }

        [TestMethod]
        public void Test_MOCegep_BDepart_AjouterDepartement()
        {
            // Arrange
            string nomCegep = "Cegep Exemple Y";
            DepartementDTO departementDTO = new DepartementDTO { No = "1", Nom = "Informatique", Description = "Département dédié à linformatique" };

            DepartementController controller = new DepartementController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterDepartement(nomCegep, departementDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep);
            List<DepartementDTO> departements = (List<DepartementDTO>)indexResult.ViewData["ListeDepartements"];

            // Assert
            Assert.IsNotNull(departements, "La liste des départements ne doit pas être null.");
            Assert.AreEqual(0, departements.Count, "Aucun département ne doit être ajouté à un cégep inexistant.");
        }

        [TestMethod]
        public void Test_BCegep_MODepart_AjouterDepartement()
        {
            // Arrange
            string nomCegep = "Cegep Exemple Y";
            DepartementDTO departementDTO = new DepartementDTO { No = "87", Nom = "Astronomioe", Description = "Spartiale" };

            DepartementController controller = new DepartementController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterDepartement(nomCegep, departementDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep);
            List<DepartementDTO> departements = (List<DepartementDTO>)indexResult.ViewData["ListeDepartements"];

            // Assert
            Assert.IsNotNull(departements, "La liste des départements ne doit pas être null.");
            Assert.IsFalse(departements.Exists(d => d.Nom == departementDTO.Nom), "Le département Astronomie ne devrait pas exister dans ce cégep.");


        }

        [TestMethod]
        public void Test_BCegep_BDepar_BEnseignant_AjouterEnseignant()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 3";
            string nomDepartement = "Loisir";
            EnseignantDTO enseignantDTO = new EnseignantDTO { NoEmploye = 645, Nom = "bb", Prenom = "bb",Adresse="bb",Ville="bb",Province="bb",CodePostal="bb",Telephone="bb",Courriel="bb" };

            EnseignantController controller = new EnseignantController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterEnseignant(nomCegep, nomDepartement,enseignantDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep,nomDepartement);
            List<EnseignantDTO> enseignants = (List<EnseignantDTO>)indexResult.ViewData["ListeEnseignants"];

            // Assert
            Assert.IsNotNull(enseignants, "La liste des enseignants ne doit pas être null.");
            Assert.IsTrue(enseignants.Exists(e => e.NoEmploye== enseignantDTO.NoEmploye), "L'enseignant doit exister pour ce cégep.");
        }

        [TestMethod]
        public void Test_BCegep_MODepar_BEnseignant_AjouterEnseignant()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 3";
            string nomDepartement = "Sports";
            EnseignantDTO enseignantDTO = new EnseignantDTO { NoEmploye = 645, Nom = "bb", Prenom = "bb", Adresse = "bb", Ville = "bb", Province = "bb", CodePostal = "bb", Telephone = "bb", Courriel = "bb" };

            EnseignantController controller = new EnseignantController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterEnseignant(nomCegep, nomDepartement, enseignantDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep, nomDepartement);
            List<EnseignantDTO> enseignants = (List<EnseignantDTO>)indexResult.ViewData["ListeEnseignants"];

            // Assert
            Assert.IsNotNull(enseignants, "La liste des enseignants ne doit pas être null.");
            Assert.IsTrue(enseignants.Exists(e => e.NoEmploye == enseignantDTO.NoEmploye), "L'enseignant NE doit PAS exister pour ce département.");
        }

        [TestMethod]
        public void Test_BCegep_BDepar_MoEnseignant_AjouterEnseignant()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 3";
            string nomDepartement = "Loisir";
            EnseignantDTO enseignantDTO = new EnseignantDTO { NoEmploye = 258, Nom = "cc", Prenom = "cc", Adresse = "cc", Ville = "cc", Province = "cc", CodePostal = "cc", Telephone = "cc", Courriel = "cc" };

            EnseignantController controller = new EnseignantController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterEnseignant(nomCegep, nomDepartement, enseignantDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep, nomDepartement);
            List<EnseignantDTO> enseignants = (List<EnseignantDTO>)indexResult.ViewData["ListeEnseignants"];

            // Assert
            Assert.IsNotNull(enseignants, "La liste des enseignants ne doit pas être null.");
            Assert.IsTrue(enseignants.Exists(e => e.NoEmploye == enseignantDTO.NoEmploye), "L'enseignant NE doit  PASexister pour ce cégep.");
        }





        public void Test_BCegep_BDepar_BCours_AjouterCours()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 3";
            string nomDepartement = "Loisir";
            CoursDTO coursDTO = new CoursDTO { No = "150", Nom = "Animation", Description = "Party" };

            CoursController controller = new CoursController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterCours(nomCegep, nomDepartement, coursDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep, nomDepartement);
            List<CoursDTO> cours = (List<CoursDTO>)indexResult.ViewData["ListeCours"];

            // Assert
            Assert.IsNotNull(cours, "La liste des cours ne doit pas être null.");
            Assert.IsTrue(cours.Exists(c => c.No == coursDTO.No), "Le cours doit exister pour ce cégep.");
        }

        [TestMethod]
        public void Test_BCegep_MODepar_BCours_AjouterCours()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 3";
            string nomDepartement = "Sports";
            CoursDTO coursDTO = new CoursDTO { No = "150", Nom = "Animation", Description = "Party" };

            CoursController controller = new CoursController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterCours(nomCegep, nomDepartement, coursDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep, nomDepartement);
            List<CoursDTO> cours = (List<CoursDTO>)indexResult.ViewData["ListeCours"];

            // Assert
            Assert.IsNotNull(cours, "La liste des cours ne doit pas être null.");
            Assert.IsTrue(cours.Exists(c => c.No == coursDTO.No), "Le cours NE doit PAS exister pour ce département.");
        }

        [TestMethod]
        public void Test_BCegep_BDepar_MoCours_AjouterCours()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 3";
            string nomDepartement = "Loisir";
            CoursDTO coursDTO = new CoursDTO { No = "1", Nom = "Informatique", Description = "informatique" };

            CoursController controller = new CoursController();

            // Act
            RedirectToActionResult ajoutResult = (RedirectToActionResult)controller.AjouterCours(nomCegep, nomDepartement, coursDTO);
            ViewResult indexResult = (ViewResult)controller.Index(nomCegep, nomDepartement);
            List<CoursDTO> cours = (List<CoursDTO>)indexResult.ViewData["ListeCours"];

            // Assert
            Assert.IsNotNull(cours, "La liste des cours ne doit pas être null.");
            Assert.IsTrue(cours.Exists(c => c.No == coursDTO.No), "Le cours NE doit  PAS exister pour ce cégep.");
        }

    }

}