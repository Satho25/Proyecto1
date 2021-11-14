using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoDeTitulo.DL;
using ProyectoDeTitulo.DBModels;

namespace ProyectoDeTitulo.Controllers
{
    public class UserController : Controller
    {
        //GET: User
        public ActionResult Index()
        {
            return View(new List<Usuario>());
        }

        [HttpPost]
        public ActionResult RegisterUser(Usuario usuario)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                DL.UserDL.RegistrarUsuario(usuario);

                return View();
            }
            catch (Exception ex) {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetUser(string key)
        {
            try
            {
                //Validaciones y preparacion
                //...
                
                DL.UserDL.GetUsuario(key);

                return View();
            }
            catch (Exception ex) {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetUserList()
        {
            try
            {
                //Validaciones y preparacion
                //...

                //DL.UserDL.GetUsuarioList();

                return View(DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}