using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeTitulo.Controllers
{
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Perfil";

            return View(DL.PerfilDL.GetPerfilList());
        }
    }
}