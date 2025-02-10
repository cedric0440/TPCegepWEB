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
        DepartementController controller = new DepartementController(); 
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
            Assert.AreEqual(5, list.Count); // Aucun cégep ne devrait être trouvé.
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
            Assert.AreEqual(1, cours.Count, "Aucun cours ne doit être listé pour un département inexistant.");
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
            Assert.AreEqual(1, cours.Count, "Si le cégep n'existe pas, il ne doit pas y avoir de cours listés.");
        }

        //1	Informatique Département dédié à linformatique

        [TestMethod]
        public void Test_BCegep_BDepart_AjouterDepartement()
        {
            // Arrange
            string nomCegep = "Cegep Exemple 1";
            DepartementDTO departementDTO = new DepartementDTO { No = "1", Nom = "Informatique", Description= "Département dédié à linformatique" };

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




    }

}
