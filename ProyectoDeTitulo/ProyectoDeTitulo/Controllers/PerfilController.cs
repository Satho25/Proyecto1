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
    public class PerfilController : BaseController
    {
        // GET: Perfil
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Perfil";

            return View(DL.PerfilDL.GetPerfilList());
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Mantenedor Perfil";

            return View("Partial/_create", new Perfil());
        }
        public ActionResult Edit(int key)
        {
            ViewBag.Title = "Mantenedor Perfil";
            try
            {
                if (key <= 0)
                {
                    throw new Exception("Llave no proporcionada");
                }

                Perfil usr = DL.PerfilDL.GetPerfil(key);
                if (usr == null)
                {
                    throw new Exception("No se encontraron registros");
                }

                return View("Partial/_edit", usr);
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = ex.Message;
                return View("Index", DL.PerfilDL.GetPerfilList());
            }
        }
        [HttpPost]
        public ActionResult CreatePerfil(Perfil Perfil)
        {
            ViewBag.Title = "Mantenedor Perfil";
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al crear";
                    return View("Partial/_create", Perfil);
                }
                //Registro
                DL.PerfilDL.RegistrarPerfil(Perfil);

                @ViewBag.Notification = "Perfil creado correctamente";
                return View("Index", DL.PerfilDL.GetPerfilList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.PerfilDL.GetPerfilList());
            }
        }
        [HttpPost]
        public ActionResult UpdatePerfil(Perfil Perfil)
        {
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al editar";
                    return View("Partial/_edit", Perfil);
                }
                //Registro
                DL.PerfilDL.ActualizarPerfil(Perfil);

                @ViewBag.Notification = "Perfil actualizado correctamente";
                return View("Index", DL.PerfilDL.GetPerfilList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.PerfilDL.GetPerfilList());
            }
        }
        [HttpPost]
        public ActionResult DeletePerfil(string key)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                if (!string.IsNullOrEmpty(key))
                {
                    DL.PerfilDL.EliminarPerfil(key);
                }
                else
                {
                    return Json(new { success = false, responseText = "Llave no proporcionada" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { success = true, responseText = "Registro eliminado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return Json(new { success = false, responseText = "Error no controlado" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult RenderFormDetail(int key)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {

                    return Json(new
                    {
                        success = true,
                        html = this.RenderViewToString("~/Views/Perfil/Partial/_detail.cshtml", DL.PermisoDL.GetPermisosByPerfilList(key))

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
    }
}