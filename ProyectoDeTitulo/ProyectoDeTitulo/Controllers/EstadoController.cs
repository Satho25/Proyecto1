using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeTitulo.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Estado";

            return View(DL.EstadoDL.GetEstadoList());
        }
    }
}