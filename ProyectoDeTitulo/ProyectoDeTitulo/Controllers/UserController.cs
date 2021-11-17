using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoDeTitulo.DL;
using ProyectoDeTitulo.DBModels;
using ProyectoDeTitulo.Controllers.Extensions;

namespace ProyectoDeTitulo.Controllers
{
    public class UserController : BaseController
    {
        //GET: User
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Usuario";
            
            return View(DL.UserDL.GetUsuarioList());
        }
        [HttpGet]
        public ActionResult RenderFormClient(bool create, string key)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    Usuario _usuario = new Usuario();
                    if (!create)
                    {
                        _usuario = DL.UserDL.GetUsuario(key);
                        if (_usuario == null)
                            throw new Exception("No se encontro usuario");
                    }
                        

                    return Json(new
                    {
                        success = true,
                        html = create ? this.RenderViewToString("~/Views/User/Partial/_create.cshtml", _usuario) :
                                        this.RenderViewToString("~/Views/User/Partial/_edit.cshtml", _usuario)

                    }, JsonRequestBehavior.AllowGet);
                  
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            return HttpNotFound("No encontrado.");
        }
        [HttpPost]
        public ActionResult CreateUser(Usuario usuario)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                DL.UserDL.RegistrarUsuario(usuario);

                return View("Index", DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("Index", new List<Usuario>());
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(Usuario usuario)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                DL.UserDL.ActualizarUsuario(usuario);

                return View("Index", DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("Index", new List<Usuario>());
            }
        }
        [HttpPost]
        public ActionResult DeleteUser(string key)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                if(!string.IsNullOrEmpty(key))
                    DL.UserDL.EliminarUsuario(key);

                return View("Index", DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("Index", new List<Usuario>());
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
                log.Error(ex);
                return View(new List<Usuario>());
            }
        }

    }
}