using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoDeTitulo.CustomAuthentication;
using ProyectoDeTitulo.Models;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Base
        public ActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("LogIn", "Account");


            return View();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}