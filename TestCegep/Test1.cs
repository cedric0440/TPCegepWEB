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
    }

}
