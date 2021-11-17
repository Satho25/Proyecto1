using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeTitulo.Controllers
{
    public class PermisoController : Controller
    {
        // GET: Permiso
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Permisos";

            return View(DL.PermisoDL.GetPermisosList());
        }
    }
}