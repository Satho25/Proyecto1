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
    public class EstadoController : BaseController
    {
        // GET: Estado
        public ActionResult Index()
        {
            ViewBag.Title = "Mantenedor Estado";

            return View(DL.EstadoDL.GetEstadoList());
        }
        public ActionResult Create()
        {
            ViewBag.Title = "Mantenedor Estado";

            return View("Partial/_create", new Estado());
        }
        public ActionResult Edit(int key)
        {
            ViewBag.Title = "Mantenedor Estado";
            try
            { 
                if (key <=0)
                {
                    throw new Exception("Llave no proporcionada");
                }

                Estado usr = DL.EstadoDL.GetEstado(key);
                if (usr == null)
                {
                    throw new Exception("No se encontraron registros");
                }

                return View("Partial/_edit", usr);
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = ex.Message;
                return View("Index", DL.EstadoDL.GetEstadoList());
            }
        }
        [HttpPost]
        public ActionResult CreateEstado(Estado Estado)
        {
            ViewBag.Title = "Mantenedor Estado";
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al crear";
                    return View("Partial/_create", Estado);
                }
                //Registro
                DL.EstadoDL.RegistrarEstado(Estado);

                @ViewBag.Notification = "Estado creado correctamente";
                return View("Index", DL.EstadoDL.GetEstadoList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.EstadoDL.GetEstadoList());
            }
        }
        [HttpPost]
        public ActionResult UpdateEstado(Estado Estado)
        {
            try
            {
                //Validaciones y preparacion
                if (!ModelState.IsValid)
                {
                    @ViewBag.NotificationErr = "Error al editar";
                    return View("Partial/_edit", Estado);
                }
                //Registro
                DL.EstadoDL.ActualizarEstado(Estado);

                @ViewBag.Notification = "Estado actualizado correctamente";
                return View("Index", DL.EstadoDL.GetEstadoList());
            }
            catch (Exception ex)
            {
                @ViewBag.NotificationErr = "Error: " + ex.Message;
                log.Error(ex);
                return View("Index", DL.EstadoDL.GetEstadoList());
            }
        }
        [HttpPost]
        public ActionResult DeleteEstado(string key)
        {
            try
            {
                //Validaciones y preparacion
                //...
                //Registro
                if (!string.IsNullOrEmpty(key))
                {
                    DL.EstadoDL.EliminarEstado(key);
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