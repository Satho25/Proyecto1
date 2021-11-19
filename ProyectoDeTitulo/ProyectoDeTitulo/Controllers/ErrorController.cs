using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoDeTitulo.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View("Error");
        }
        public ActionResult Error500(string err)
        {
            @ViewBag.error = err;
            return View("Error");
        }
    }
}