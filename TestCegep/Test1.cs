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
        Assert.AreEqual(5, list.Count);
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
            EnseignantController controller = new EnseignantController();

            // Act
            ViewResult viewResult = (ViewResult)controller.Index(cegepNom,departementNom);
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



    }

}
