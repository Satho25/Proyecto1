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
        public ActionResult Create()
        {
            ViewBag.Title = "Mantenedor Usuario";

            return View("Partial/_create", new Usuario());
        }
        public ActionResult Edit(string key)
        {
            ViewBag.Title = "Mantenedor Usuario";
            try
            {
                if (key == null)
                {
                    throw new Exception("Llave no proporcionada"); 
                }

                Usuario usr = DL.UserDL.GetUsuario(key);
                if (usr == null)
                {
                    throw new Exception("No se encontraron registros");
                }

                return View("Partial/_edit", usr);
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = ex.Message;
                return View("Index", DL.UserDL.GetUsuarioList());
            }
        }
        [HttpPost]
        public ActionResult CreateUser(Usuario usuario)
        {
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al crear";
                    return View("Partial/_create", usuario);
                }
                //Registro
                DL.UserDL.RegistrarUsuario(usuario);

                @ViewBag.Notification = "Usuario creado correctamente";
                return View("Index", DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr ="Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.UserDL.GetUsuarioList());
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(Usuario usuario)
        {
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al editar";
                    return View("Partial/_edit", usuario);
                }
                //Registro
                DL.UserDL.ActualizarUsuario(usuario);

                @ViewBag.Notification = "Usuario actualizado correctamente";
                return View("Index", DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.UserDL.GetUsuarioList());
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
                if (!string.IsNullOrEmpty(key))
                {
                    DL.UserDL.EliminarUsuario(key);
                }                    
                else
                {
                    @ViewBag.NotificationErr = "Llave no proporcionada";
                    return View("Index", DL.UserDL.GetUsuarioList());
                }

                @ViewBag.Notification = "Usuario eliminado correctamente";
                return View("Index", DL.UserDL.GetUsuarioList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.UserDL.GetUsuarioList());
            }
        }

        #region "Comentado"
        //[HttpGet]
        //public ActionResult RenderFormClient(bool create, string key)
        //{
        //    if (Request.IsAjaxRequest())
        //    {
        //        try
        //        {
        //            Usuario _usuario = new Usuario();
        //            if (!create)
        //            {
        //                _usuario = DL.UserDL.GetUsuario(key);
        //                if (_usuario == null)
        //                    throw new Exception("No se encontro usuario");
        //            }


        //            return Json(new
        //            {
        //                success = true,
        //                html = create ? this.RenderViewToString("~/Views/User/Partial/_create.cshtml", _usuario) :
        //                                this.RenderViewToString("~/Views/User/Partial/_edit.cshtml", _usuario)

        //            }, JsonRequestBehavior.AllowGet);

        //        }
        //        catch (Exception ex)
        //        {
        //            log.Error(ex);
        //            return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    return HttpNotFound("No encontrado.");
        //}
        #endregion
    }
}