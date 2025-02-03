using System.Diagnostics;
using GestionCegepWeb.Logics.Controleurs;
using Microsoft.AspNetCore.Mvc;

namespace TPCegepWEB.Controllers
{
    public class CegepController : Controller
    {
        [Route("")]
        [Route("Cegeps")]
        [Route("Cegeps/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                ViewBag.ListeCegeps = CegepControleur.Instance.ObtenirListeCegep();
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();

            }
        }

        }
    }
