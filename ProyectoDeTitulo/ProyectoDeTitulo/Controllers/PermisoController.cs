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
    public class PermisoController : BaseController
    {
        // GET: Permiso
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Permisos";

            return View(DL.PermisoDL.GetPermisosList());
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Mantenedor Permisos";

            return View("Partial/_create", new Permisos());
        }
        public ActionResult Edit(int key)
        {
            ViewBag.Title = "Mantenedor Permisos";
            try
            {
                if (key <= 0)
                {
                    throw new Exception("Llave no proporcionada");
                }

                Permisos usr = DL.PermisoDL.GetPermisos(key);
                if (usr == null)
                {
                    throw new Exception("No se encontraron registros");
                }

                return View("Partial/_edit", usr);
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = ex.Message;
                return View("Index", DL.PermisoDL.GetPermisosList());
            }
        }
        [HttpPost]
        public ActionResult CreatePermisos(Permisos Permisos)
        {
            ViewBag.Title = "Mantenedor Permisos";
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al crear";
                    return View("Partial/_create", Permisos);
                }
                //Registro
                DL.PermisoDL.RegistrarPermisos(Permisos);

                @ViewBag.Notification = "Permisos creado correctamente";
                return View("Index", DL.PermisoDL.GetPermisosList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.PermisoDL.GetPermisosList());
            }
        }
        [HttpPost]
        public ActionResult UpdatePermisos(Permisos Permisos)
        {
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al editar";
                    return View("Partial/_edit", Permisos);
                }
                //Registro
                DL.PermisoDL.ActualizarPermisos(Permisos);

                @ViewBag.Notification = "Permisos actualizado correctamente";
                return View("Index", DL.PermisoDL.GetPermisosList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.PermisoDL.GetPermisosList());
            }
        }
        [HttpPost]
        public ActionResult DeletePermisos(string key)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                if (!string.IsNullOrEmpty(key))
                {
                    DL.PermisoDL.EliminarPermisos(key);
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
    }
}